namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Reflection;

    internal class XmlWriterActions
    {
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
            if (!type.IsSealed)
            {
                if (this.actions.TryGetValue(type, out var action) && action is CastAction<TextWriter> match)
                {
                    castAction = match;
                    return true;
                }

                if (type.IsEnum)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    _ = typeof(XmlWriterActions).GetMethod(nameof(this.RegisterEnum), BindingFlags.Instance | BindingFlags.NonPublic)
                                                .MakeGenericMethod(type)
                                                .Invoke(this, null);
                    return this.TryGetSimpleCached(type, out castAction);
                }
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

                if (type.IsEnum)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    _ = typeof(XmlWriterActions).GetMethod(nameof(this.RegisterEnum), BindingFlags.Instance | BindingFlags.NonPublic)
                                                .MakeGenericMethod(type)
                                                .Invoke(this, null);
                    return TryGetWriter(typeof(TMember), out writer);
                }
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

            object Create(Type x)
            {
                return CollectionWriter.Create(x, this);
            }
        }

        internal bool TryGetWriteMap<T>(T value, out WriteMap map)
        {
            if (value?.GetType() is Type type &&
                this.actions.GetOrAdd(type, x => WriteMap.Create(x)) is WriteMap match)
            {
                map = match;
                return true;
            }

            map = null;
            return false;
        }

        internal XmlWriterActions SimpleClass<T>(Action<TextWriter, T> action)
            where T : class
        {
            this.actions[typeof(T)] = CastAction<TextWriter>.Create(action);
            return this;
        }

        internal XmlWriterActions SimpleStruct<T>(Action<TextWriter, T> action)
            where T : struct
        {
            this.actions[typeof(T)] = CastAction<TextWriter>.Create(action);
            if (!this.actions.ContainsKey(typeof(T?)))
            {
                this.actions[typeof(T?)] = CastAction<TextWriter>.Create(new Action<TextWriter, T?>((writer, value) =>
                {
                    if (value.HasValue)
                    {
                        // Using GetValueOrDefault() here as it is a little faster.
                        action(writer, value.GetValueOrDefault());
                    }
                }));
            }

            return this;
        }

        private void RegisterEnum<T>()
            where T : struct, Enum
        {
            this.SimpleStruct<T>((writer, value) => EnumWriter<T>.Default.Write(writer, value));
        }
    }
}