namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Xml.Serialization;

    public sealed class EnumWriter<T>
        where T : struct, Enum
    {
        /// <summary>
        /// An <see cref="EnumWriter{T}"/> that serializes with 'G' format string.
        /// </summary>
        public static readonly EnumWriter<T> Default = new EnumWriter<T>("G");

        /// <summary>
        /// An <see cref="EnumWriter{T}"/> that serializes with 'D' format string.
        /// </summary>
        public static readonly EnumWriter<T> Integer = new EnumWriter<T>("D");

        private readonly ConcurrentDictionary<T, string> cache = new ConcurrentDictionary<T, string>();
        private readonly string format;

        private EnumWriter(string format)
        {
            this.format = format;
        }

        public void Write(TextWriter writer, T value)
        {
            writer.Write(this.cache.GetOrAdd(value, x => this.ToString(x)));
        }

        private string ToString(T value)
        {
            if (typeof(T).GetMember(value.ToString())
                         .TryFirst(out var member) &&
                member.TryGetCustomAttribute<XmlEnumAttribute>(out var attribute) &&
                attribute.Name is string text &&
                !string.IsNullOrEmpty(text))
            {
                return text;
            }

            return Enum.Format(typeof(T), value, this.format).Replace(",", string.Empty);
        }
    }
}