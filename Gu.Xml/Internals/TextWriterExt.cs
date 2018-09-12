namespace Gu.Xml
{
    using System.IO;

    internal static class TextWriterExt
    {
        internal static void WriteMany(this TextWriter writer, string text1, string text2, string text3)
        {
            writer.Write(text1);
            writer.Write(text2);
            writer.Write(text3);
        }
    }
}