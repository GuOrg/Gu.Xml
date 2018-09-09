namespace Gu.Xml
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Wraps a <see cref="TextWriter"/> and exposes methods for writing XML.
    /// </summary>
    public sealed class XmlWriter : IDisposable
    {
        private readonly TextWriter writer;

        private int indentLevel;
        private bool disposed;

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

        public void WriteElement<T>(string name, T value)
        {
            if (value == null)
            {
            }
            else if (SimpleValueWriter.TryGet(value, out var simple))
            {
                this.WriteStartElement(name);
                simple.Write(this.writer, value);
                this.WriteEndElement(name);
            }
            else if (ComplexValueWriter.GetOrCreate(value) is ComplexValueWriter complex)
            {
                this.WriteStartElement(name, value, complex.Attributes);
                this.writer.WriteLine();
                foreach (var elementWriter in complex.Elements)
                {
                    elementWriter.Write(this, value);
                }

                this.WriteEndElement(name);
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

        public void WriteStartElement(string name)
        {
            this.WriteIndentation();
            this.writer.Write("<");
            this.writer.Write(name);
            this.writer.Write(">");
            this.indentLevel++;
        }

        public void WriteStartElement<T>(string name, T source, IReadOnlyList<AttributeWriter> attributeWriters)
        {
            this.WriteIndentation();
            this.writer.Write("<");
            this.writer.Write(name);
            foreach (var attributeWriter in attributeWriters)
            {
                attributeWriter.Write(this.writer, source);
            }

            this.writer.Write(">");
            this.indentLevel++;
        }

        public void WriteEndElement(string name)
        {
            this.writer.Write("</");
            this.writer.Write(name);
            this.writer.Write(">");
            this.indentLevel--;
        }

        public void WriteLine()
        {
            this.writer.WriteLine();
        }

        public void Dispose()
        {
            if (this.disposed)
            {
                return;
            }

            this.disposed = true;
#pragma warning disable IDISP007 // Don't dispose injected.
            this.writer.Dispose();
#pragma warning restore IDISP007 // Don't dispose injected.
        }

        private void WriteIndentation()
        {
            for (var i = 0; i < this.indentLevel; i++)
            {
                this.writer.Write("  ");
            }
        }

        private void ThrowIfDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }
        }
    }
}