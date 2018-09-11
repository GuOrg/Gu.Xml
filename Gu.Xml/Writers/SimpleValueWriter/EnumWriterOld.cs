namespace Gu.Xml
{
    using System;
    using System.IO;

    public sealed class EnumWriterOld : SimpleValueWriter
    {
        /// <summary>
        /// An <see cref="EnumWriterOld"/> that serializes with 'G' format string.
        /// </summary>
        public static readonly EnumWriterOld Default = new EnumWriterOld("G");

        /// <summary>
        /// An <see cref="EnumWriterOld"/> that serializes with 'D' format string.
        /// </summary>
        public static readonly EnumWriterOld Integer = new EnumWriterOld("D");

        private readonly string format;

        private EnumWriterOld(string format)
        {
            this.format = format;
        }

        public override void Write<T>(TextWriter writer, T value)
        {
            writer.Write(Enum.Format(typeof(T), value, this.format).Replace(",", string.Empty));
        }
    }
}