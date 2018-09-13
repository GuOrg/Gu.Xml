namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Reflection;

    internal class XmlWriterActions
    {
        // Using <Type, object> here as an optimization. Hopefully we can refactor to something nicer.
        private readonly ConcurrentDictionary<Type, object> actions = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// If the type is sealed and has a simple action it can be cached to avoid lookups for example when writing collections.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="castAction"></param>
        /// <returns></returns>
        internal bool TryGetSimpleCached(Type type, out CastAction<TextWriter> castAction)
        {
            castAction = null;
            if (type.IsSealed)
            {
                if (this.actions.TryGetValue(type, out var action) && action is CastAction<TextWriter> match)
                {
                    castAction = match;
                    return true;
                }

                return this.TryRegisterEnum(type, out castAction);
            }

            return false;
        }

        internal bool TryGetSimple<TMember>(TMember value, out Action<TextWriter, TMember> writer)
        {
            if (TryGetType(out var type))
            {
                if (TryGetWriter(type, out writer))
                {
                    return writer != null;
                }

                return this.TryRegisterEnum(type, out var castAction) &&
                       castAction.TryGet(out writer);
            }

            writer = null;
            return false;

            bool TryGetWriter(Type current, out Action<TextWriter, TMember> result)
            {
                result = null;
                if (this.actions.TryGetValue(current, out var match))
                {
                    if (match is CastAction<TextWriter> castAction)
                    {
                        return castAction.TryGet(out result);
                    }

                    return true;
                }

                return false;
            }

            bool TryGetType(out Type result)
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
        }

        internal bool TryGetCollection<T>(T value, out Action<XmlWriter, T> writer)
        {
            if (value is IEnumerable &&
                this.actions.GetOrAdd(value.GetType(), x => Create(x)) is CastAction<XmlWriter> castAction)
            {
                return castAction.TryGet(out writer);
            }

            writer = null;
            return false;

            CastAction<XmlWriter> Create(Type x)
            {
                return CollectionItemWriter.Create(x, this);
            }
        }

        internal bool TryGetWriteMap<T>(T value, out WriteMap map)
        {
            if (value?.GetType() is Type type &&
                this.actions.GetOrAdd(type, x => Create(x)) is WriteMap match)
            {
                map = match;
                return true;
            }

            map = null;
            return false;

            WriteMap Create(Type x) => WriteMap.Create(x, this);
        }

        /// <summary>
        /// If the type is sealed and has a simple action it can be cached to avoid lookups for example when writing collections.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        internal bool TryGetWriteMapCached(Type type, out WriteMap map)
        {
            map = null;
            if (type.IsSealed &&
                this.actions.GetOrAdd(type, x => Create(x)) is WriteMap match)
            {
                map = match;
                return true;
            }

            return false;

            WriteMap Create(Type x) => WriteMap.Create(x, this);
        }

        internal XmlWriterActions RegisterSimple<T>(Action<TextWriter, T> action)
        {
            this.actions[typeof(T)] = CastAction<TextWriter>.Create(action);
            if (typeof(T).IsValueType &&
                !typeof(T).IsNullable())
            {
                var nullableType = typeof(Nullable<>).MakeGenericType(typeof(T));
                if (!this.actions.ContainsKey(nullableType))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    this.actions[nullableType] = typeof(CastAction<TextWriter>)
                                                 .GetMethod(nameof(CastAction<TextWriter>.CreateNullable), BindingFlags.Static | BindingFlags.NonPublic)
                                                 .MakeGenericMethod(typeof(T))
                                                 .Invoke(null, new[] { action });
                }
            }

            return this;
        }

        private bool TryRegisterEnum(Type type, out CastAction<TextWriter> castAction)
        {
            if (type.IsEnum)
            {
                // ReSharper disable once PossibleNullReferenceException
                _ = typeof(XmlWriterActions).GetMethod(nameof(this.RegisterEnum), BindingFlags.Instance | BindingFlags.NonPublic)
                                            .MakeGenericMethod(type)
                                            .Invoke(this, null);
                return this.TryGetSimpleCached(type, out castAction);
            }

            castAction = null;
            return false;
        }

        private void RegisterEnum<T>()
            where T : struct, Enum
        {
            this.RegisterSimple<T>((writer, value) => EnumWriter<T>.Default.Write(writer, value));
        }
    }
}