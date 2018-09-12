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

            return Enum.Format(typeof(T), value, this.format).Replace(",", string.Empty);
        }
    }
}