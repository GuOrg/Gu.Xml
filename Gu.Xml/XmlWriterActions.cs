namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Reflection;

    public class XmlWriterActions
    {
        private readonly ConcurrentDictionary<Type, CastAction<TextWriter>> simpleActions = new ConcurrentDictionary<Type, CastAction<TextWriter>>();
        private readonly ConcurrentDictionary<Type, CastAction<XmlWriter>> collectionActions = new ConcurrentDictionary<Type, CastAction<XmlWriter>>();
        private readonly ConcurrentDictionary<Type, WriteMap> writeMaps = new ConcurrentDictionary<Type, WriteMap>();

        public bool TryGetSimple<TMember>(TMember value, out Action<TextWriter, TMember> writer)
        {
            if (value == null)
            {
                writer = null;
                return false;
            }

            return this.TryGetSimple(value.GetType(), out writer);
        }

        public bool TryGetCollection<T>(T value, out Action<XmlWriter, T> writer)
        {
            if (value is IEnumerable &&
                this.collectionActions.GetOrAdd(value.GetType(), x => CollectionWriter.Create(x)) is CastAction<XmlWriter> castAction)
            {
                return castAction.TryGet(out writer);
            }

            writer = null;
            return false;
        }

        public bool TryGetWriteMap<T>(T value, out WriteMap map)
        {
            if (value?.GetType() is Type type)
            {
                map = this.writeMaps.GetOrAdd(type, x => WriteMap.Create(x));
                return true;
            }

            map = null;
            return false;
        }

        public XmlWriterActions SimpleClass<T>(Action<TextWriter, T> action)
            where T : class
        {
            this.simpleActions[typeof(T)] = CastAction<TextWriter>.Create(action);
            return this;
        }

        public XmlWriterActions SimpleStruct<T>(Action<TextWriter, T> action)
            where T : struct
        {
            this.simpleActions[typeof(T)] = CastAction<TextWriter>.Create(action);
            if (!this.simpleActions.ContainsKey(typeof(T?)))
            {
                this.simpleActions[typeof(T?)] = CastAction<TextWriter>.Create(new Action<TextWriter, T?>((writer, value) => action(writer, value.Value)));
            }

            return this;
        }

        /// <summary>
        /// Try getting a writer for writing the element contents as a string.
        /// </summary>
        /// <typeparam name="TMember">
        /// The type of the member.
        /// If an int is stored in a property of type object <typeparamref name="TMember"/> will be <see cref="object"/> and <paramref name="type"/> will be <see cref="int"/>
        /// </typeparam>
        /// <param name="type">The type of the value.</param>
        /// <param name="writer"></param>
        /// <returns>True if a writer was found for <paramref name="type"/></returns>
        private bool TryGetSimple<TMember>(Type type, out Action<TextWriter, TMember> writer)
        {
            if (this.simpleActions.TryGetValue(type, out var castAction))
            {
                if (castAction.TryGet(out writer))
                {
                    return true;
                }

                return this.TryGetSimple(typeof(TMember), out writer);
            }

            if (type.IsEnum)
            {
                // ReSharper disable once PossibleNullReferenceException
                _ = typeof(XmlWriterActions).GetMethod(nameof(this.RegisterEnum), BindingFlags.Instance | BindingFlags.NonPublic)
                                             .MakeGenericMethod(type)
                                             .Invoke(this, null);
                return this.TryGetSimple(typeof(TMember), out writer);
            }

            writer = null;
            return false;
        }

        private void RegisterEnum<T>()
            where T : struct, Enum
        {
            this.SimpleStruct<T>((writer, value) => EnumWriter<T>.Default.Write(writer, value));
        }
    }
}