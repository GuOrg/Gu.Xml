﻿namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Reflection;

    public class XmlWriterActions
    {
        private readonly ConcurrentDictionary<Type, object> actions = new ConcurrentDictionary<Type, object>();

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
                this.actions.GetOrAdd(value.GetType(), x => CollectionWriter.Create(x)) is CastAction<XmlWriter> castAction)
            {
                return castAction.TryGet(out writer);
            }

            writer = null;
            return false;
        }

        public bool TryGetWriteMap<T>(T value, out WriteMap map)
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

        public XmlWriterActions SimpleClass<T>(Action<TextWriter, T> action)
            where T : class
        {
            this.actions[typeof(T)] = CastAction<TextWriter>.Create(action);
            return this;
        }

        public XmlWriterActions SimpleStruct<T>(Action<TextWriter, T> action)
            where T : struct
        {
            this.actions[typeof(T)] = CastAction<TextWriter>.Create(action);
            if (!this.actions.ContainsKey(typeof(T?)))
            {
                this.actions[typeof(T?)] = CastAction<TextWriter>.Create(new Action<TextWriter, T?>((writer, value) => action(writer, value.Value)));
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
            if (this.actions.TryGetValue(type, out var value) &&
                value is CastAction<TextWriter> castAction)
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