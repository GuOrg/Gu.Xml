namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Reflection;

    internal class WriteMaps
    {
        // Using <Type, object> here as an optimization. Hopefully we can refactor to something nicer.
        private readonly ConcurrentDictionary<Type, WriteMap> maps = new ConcurrentDictionary<Type, WriteMap>();

        /// <summary>
        /// If the type is sealed and has a simple action it can be cached to avoid lookups for example when writing collections.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        internal bool TryGetSimpleCached(Type type, out SimpleWriteMap map)
        {
            if (type.IsSealed)
            {
                if (this.maps.TryGetValue(type, out var match) &&
                    match is SimpleWriteMap temp)
                {
                    map = temp;
                    return true;
                }

                return this.TryRegisterEnum(type, out map);
            }

            map = null;
            return false;
        }

        internal bool TryGetSimple<TMember>(TMember value, out SimpleWriteMap map)
        {
            if (TryGetType(value, out var type))
            {
                if (this.maps.TryGetValue(type, out var match) &&
                    match is SimpleWriteMap temp)
                {
                    map = temp;
                    return true;
                }

                return this.TryRegisterEnum(type, out map);
            }

            map = null;
            return false;
        }

        /// <summary>
        /// If the type is sealed and has a simple action it can be cached to avoid lookups for example when writing collections.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        internal bool TryGetComplexCached(Type type, out ComplexWriteMap map)
        {
            map = null;
            if (!typeof(IEnumerable).IsAssignableFrom(type) &&
                !type.IsEnum &&
                type.IsSealed &&
                this.maps.GetOrAdd(type, x => Create(x)) is ComplexWriteMap match)
            {
                map = match;
                return true;
            }

            return false;

            ComplexWriteMap Create(Type x) => ComplexWriteMap.Create(x, this);
        }

        internal bool TryGet<T>(T value, out WriteMap map)
        {
            if (TryGetType(value, out var type))
            {
                if (this.maps.TryGetValue(type, out map))
                {
                    return true;
                }

                if (this.TryGetItems(value, out var items))
                {
                    map = items;
                    return true;
                }

                if (this.TryGetComplex(value, out var complex))
                {
                    map = complex;
                    return true;
                }
            }

            map = null;
            return false;
        }

        internal WriteMaps RegisterSimple<T>(Action<TextWriter, T> action)
        {
            this.maps[typeof(T)] = new SimpleWriteMap(CastAction<TextWriter>.Create(action));
            if (typeof(T).IsValueType)
            {
                if (Nullable.GetUnderlyingType(typeof(T)) is Type underlying)
                {
                    if (!this.maps.ContainsKey(underlying))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        this.maps[underlying] = new SimpleWriteMap(
                            (CastAction<TextWriter>)typeof(CastAction<TextWriter>)
                                                    .GetMethod(nameof(CastAction<TextWriter>.CreateUnderlying), BindingFlags.Static | BindingFlags.NonPublic)
                                                    .MakeGenericMethod(underlying)
                                                    .Invoke(null, new object[] { action }));
                    }
                }
                else
                {
                    var nullableType = typeof(Nullable<>).MakeGenericType(typeof(T));
                    if (!this.maps.ContainsKey(nullableType))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        this.maps[nullableType] = new SimpleWriteMap(
                            (CastAction<TextWriter>)typeof(CastAction<TextWriter>)
                                        .GetMethod(nameof(CastAction<TextWriter>.CreateNullable), BindingFlags.Static | BindingFlags.NonPublic)
                                        .MakeGenericMethod(typeof(T))
                                        .Invoke(null, new object[] { action }));
                    }
                }
            }

            return this;
        }

        private static bool TryGetType<TMember>(TMember value, out Type result)
        {
            var candidate = typeof(TMember);
            if (candidate.IsSealed)
            {
                result = candidate;
                return true;
            }

            result = value?.GetType();
            return result != null;
        }

        private bool TryGetItems<T>(T value, out ItemsWriteMap map)
        {
            if (value is IEnumerable &&
                this.maps.GetOrAdd(value.GetType(), x => Create(x)) is ItemsWriteMap match)
            {
                map = match;
                return true;
            }

            map = null;
            return false;

            ItemsWriteMap Create(Type x)
            {
                return ItemsWriteMap.Create(x, this, null);
            }
        }

        private bool TryGetComplex<T>(T value, out ComplexWriteMap map)
        {
            if (TryGetType(value, out var type) &&
                this.maps.GetOrAdd(type, x => Create(x)) is ComplexWriteMap match)
            {
                map = match;
                return true;
            }

            map = null;
            return false;

            ComplexWriteMap Create(Type x) => ComplexWriteMap.Create(x, this);
        }

        private bool TryRegisterEnum(Type type, out SimpleWriteMap map)
        {
            if (type.IsEnum)
            {
                // ReSharper disable once PossibleNullReferenceException
                // ReSharper disable once AssignmentIsFullyDiscarded
                _ = typeof(WriteMaps)
                    .GetMethod(nameof(this.RegisterEnum), BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(type)
                    .Invoke(this, null);
                return this.TryGetSimpleCached(type, out map);
            }

            map = null;
            return false;
        }

        private void RegisterEnum<T>()
            where T : struct, Enum
        {
            this.RegisterSimple<T>((writer, value) => EnumFormatter<T>.Default.Write(writer, value));
        }
    }
}