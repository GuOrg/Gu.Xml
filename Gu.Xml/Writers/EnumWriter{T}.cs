namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Reflection;

    public sealed class EnumWriter<T>
        where T : struct, Enum
    {
        /// <summary>
        /// An <see cref="EnumWriter{T}"/> that serializes with 'G' format string and removes commas.
        /// </summary>
        public static readonly EnumWriter<T> Default = new EnumWriter<T>("G", removeCommas: true);

        /// <summary>
        /// An <see cref="EnumWriter{T}"/> that serializes with 'G' format string.
        /// </summary>
        public static readonly EnumWriter<T> String = new EnumWriter<T>("G", removeCommas: false);

        /// <summary>
        /// An <see cref="EnumWriter{T}"/> that serializes with 'D' format string.
        /// </summary>
        public static readonly EnumWriter<T> Integer = new EnumWriter<T>("D", removeCommas: false);

        private readonly ConcurrentDictionary<T, string> cache = new ConcurrentDictionary<T, string>();
        private readonly string format;
        private readonly bool removeCommas;

        private EnumWriter(string format, bool removeCommas)
        {
            this.format = format;
            this.removeCommas = removeCommas;
        }

        public void Write(TextWriter writer, T value)
        {
            writer.Write(this.cache.GetOrAdd(value, x => this.ToString(x)));
        }

        private static bool TryGetNameFromAttribute<TAttribute>(MemberInfo member, Func<TAttribute, string> getName, out string name)
            where TAttribute : Attribute
        {
            name = null;
            if (member.TryGetCustomAttribute(out TAttribute attribute))
            {
                name = getName(attribute);
            }

            return !string.IsNullOrEmpty(name);
        }

        private string ToString(T value)
        {
            if (typeof(T).GetMember(value.ToString()).TryFirst(out var member))
            {
                if (TryGetNameFromAttribute<System.Xml.Serialization.XmlEnumAttribute>(member, x => x.Name, out var name) ||
                    TryGetNameFromAttribute<System.Xml.Serialization.SoapEnumAttribute>(member, x => x.Name, out name) ||
                    TryGetNameFromAttribute<System.Runtime.Serialization.EnumMemberAttribute>(member, x => x.Value, out name))
                {
                    return name;
                }
            }

            var text = Enum.Format(typeof(T), value, this.format);
            if (this.removeCommas)
            {
                return text.Replace(",", string.Empty);
            }

            return text;
        }
    }
}