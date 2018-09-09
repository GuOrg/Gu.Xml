namespace Gu.Xml
{
    using System;
    using System.IO;

    public class EnumWriter : SimpleValueWriter
    {
        public static readonly EnumWriter Default = new EnumWriter();

        public override void Write<T>(TextWriter writer, T value)
        {
            writer.Write(Enum.Format(typeof(T), value, "F"));
        }
    }
}