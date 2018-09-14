namespace Gu.Xml
{
    using System.IO;

    internal class SimpleWriteMap : WriteMap
    {
        internal readonly CastAction<TextWriter> Write;

        internal SimpleWriteMap(CastAction<TextWriter> write)
        {
            this.Write = write;
        }

        internal void WriteElement<TValue>(XmlWriter writer, string name, TValue value)
        {
            writer.ClosePendingStart();
            writer.WriteIndentation();
            var textWriter = writer.TextWriter;
            textWriter.WriteMany("<", name, ">");
            this.Write.Get<TValue>().Invoke(textWriter, value);
            textWriter.WriteMany("</", name, ">");
        }

        internal void WriteAttribute<TValue>(TextWriter textWriter, string name, TValue value)
        {
            textWriter.WriteMany(" ", name, "=\"");
            this.Write.Get<TValue>().Invoke(textWriter, value);
            textWriter.Write("\"");
        }
    }
}