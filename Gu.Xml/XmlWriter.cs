namespace Gu.Xml
{
    using System;
    using System.IO;

    /// <summary>
    /// Wraps a <see cref="TextWriter"/> and exposes methods for writing XML.
    /// </summary>
    public sealed class XmlWriter : IDisposable
    {
        private readonly TextWriter writer;

        private int indentLevel;
        private bool pendingCloseStartElement = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriter"/> class.
        /// </summary>
        /// <param name="writer">The inner <see cref="TextWriter"/>.</param>
        public XmlWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        public void WriteXmlDeclaration()
        {
            this.writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        }

        public void WriteRootElement<T>(T value)
        {
            this.WriteElement(RootName.Get(value.GetType()), value);
        }

        public void WriteElement<T>(string name, T value)
        {
            if (value == null)
            {
            }
            else if (SimpleValueWriter.TryGet(value, out var simple))
            {
                if (this.pendingCloseStartElement)
                {
                    this.writer.WriteLine(">");
                    this.pendingCloseStartElement = false;
                }

                this.WriteIndentation();
                this.writer.Write("<");
                this.writer.Write(name);
                this.writer.Write(">");
                simple.Write(this.writer, value);
                this.writer.Write("</");
                this.writer.Write(name);
                this.writer.Write(">");
            }
            else if (ComplexValueWriter.GetOrCreate(value) is ComplexValueWriter complex)
            {
                if (this.pendingCloseStartElement)
                {
                    this.writer.WriteLine(">");
                    this.pendingCloseStartElement = false;
                }

                this.WriteIndentation();
                this.writer.Write("<");
                this.writer.Write(name);
                foreach (var attributeWriter in complex.Attributes)
                {
                    attributeWriter.Write(this.writer, value);
                }

                this.pendingCloseStartElement = true;

                this.indentLevel++;
                foreach (var elementWriter in complex.Elements)
                {
                    elementWriter.Write(this, value);
                }

                this.indentLevel--;
                if (this.pendingCloseStartElement)
                {
                    this.writer.Write(" />");
                    this.pendingCloseStartElement = false;
                }
                else
                {
                    this.WriteIndentation();
                    this.writer.Write("</");
                    this.writer.Write(name);
                    this.writer.Write(">");
                }
            }
            else
            {
                throw new InvalidOperationException($"Failed getting a value writer for {value}");
            }
        }

        public void Write(string text)
        {
            this.writer.Write(text);
        }

        public void WriteLine()
        {
            this.writer.WriteLine();
        }

        public void Dispose()
        {
#pragma warning disable IDISP007 // Don't dispose injected.
            this.writer.Dispose();
#pragma warning restore IDISP007 // Don't dispose injected.
        }

        internal void Reset()
        {
            System.Diagnostics.Debug.Assert(this.indentLevel == 0, "this.indentLevel == 0");
            this.indentLevel = 0;
            System.Diagnostics.Debug.Assert(!this.pendingCloseStartElement, "!this.pendingCloseStartElement");
            this.pendingCloseStartElement = false;
        }

        private void WriteIndentation()
        {
            for (var i = 0; i < this.indentLevel; i++)
            {
                this.writer.Write("  ");
            }
        }
    }
}