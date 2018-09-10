namespace Gu.Xml
{
    using System;
    using System.IO;

    public sealed class EnumWriter : SimpleValueWriter
    {
        /// <summary>
        /// An <see cref="EnumWriter"/> that serializes with 'G' format string.
        /// </summary>
        public static readonly EnumWriter Default = new EnumWriter("G");

        /// <summary>
        /// An <see cref="EnumWriter"/> that serializes with 'D' format string.
        /// </summary>
        public static readonly EnumWriter Integer = new EnumWriter("D");

        private readonly string format;

        private EnumWriter(string format)
        {
            this.format = format;
        }

        public override void Write<T>(TextWriter writer, T value)
        {
            writer.Write(Enum.Format(typeof(T), value, this.format).Replace(",", string.Empty));
        }
    }
}