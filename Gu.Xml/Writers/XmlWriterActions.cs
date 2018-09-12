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

        internal bool TryGetSimple<TMember>(TMember value, out Action<TextWriter, TMember> writer)
        {
            if (value == null)
            {
                writer = null;
                return false;
            }

            var type = value.GetType();
            if (TryGet(type, out writer))
            {
                return writer != null;
            }

            var memberType = typeof(TMember);
            if (memberType.IsNullable())
            {
                return TryGet(memberType, out writer) && writer != null;
            }

            if (type.IsEnum)
            {
                // ReSharper disable once PossibleNullReferenceException
                _ = typeof(XmlWriterActions).GetMethod(nameof(this.RegisterEnum), BindingFlags.Instance | BindingFlags.NonPublic)
                                            .MakeGenericMethod(type)
                                            .Invoke(this, null);
                return TryGet(typeof(TMember), out writer);
            }

            writer = null;
            return false;

            bool TryGet(Type current, out Action<TextWriter, TMember> result)
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
        }

        internal bool TryGetCollection<T>(T value, out Action<XmlWriter, T> writer)
        {
            if (value is IEnumerable &&
                this.actions.GetOrAdd(value.GetType(), x => CollectionWriter.Create(x)) is CastAction<XmlWriter> castAction)
            {
                return castAction.TryGet(out writer);
            }

            writer = null;
            return false;
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
                this.actions[typeof(T?)] = CastAction<TextWriter>.Create(new Action<TextWriter, T?>((writer, value) => action(writer, value.Value)));
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