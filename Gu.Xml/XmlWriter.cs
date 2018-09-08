﻿namespace Gu.Xml
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
                return;
            }

            if (ValueWriter.TryGet(value, out var valueWriter))
            {
                this.WriteIndentation();
                this.WriteStartElement(name);
                valueWriter.Write(this.writer, value);
                this.WriteEndElement(name);
                return;
            }

            this.WriteIndentation();
            this.WriteStartElement(name);
            this.writer.WriteLine();
            this.indentLevel++;
            foreach (var property in value.GetType().GetProperties())
            {
                this.WriteElement(property.Name, property.GetValue(value));
                this.writer.WriteLine();
            }

            this.indentLevel--;
            this.WriteEndElement(name);
        }

        public void Write(string text)
        {
            this.writer.Write(text);
        }

        public void WriteStartElement(string name)
        {
            this.writer.Write("<");
            this.writer.Write(name);
            this.writer.Write(">");
        }

        public void WriteEndElement(string name)
        {
            this.writer.Write("</");
            this.writer.Write(name);
            this.writer.Write(">");
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