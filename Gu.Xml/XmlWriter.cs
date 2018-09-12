namespace Gu.Xml
{
    using System;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// Wraps a <see cref="TextWriter"/> and exposes methods for writing XML.
    /// </summary>
    public sealed class XmlWriter : IDisposable
    {
        private static readonly XmlWriterActions DefaultWriterActions = new XmlWriterActions()
            .SimpleClass<string>((writer, value) => writer.Write(value))
            .SimpleStruct<bool>((writer, value) => writer.Write(value ? "true" : "false"))
            .SimpleStruct<byte>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<char>((writer, value) => writer.Write((int)value))
            .SimpleStruct<DateTime>((writer, value) => writer.Write(value.ToString("O", NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<decimal>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<double>((writer, value) => writer.Write(XmlFormat.ToString(value)))
            .SimpleStruct<float>((writer, value) => writer.Write(XmlFormat.ToString(value)))
            .SimpleStruct<Guid>((writer, value) => writer.Write(value.ToString()))
            .SimpleStruct<int>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<long>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<short>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<sbyte>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<ulong>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<uint>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<ushort>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));

        private readonly TextWriter writer;

        private int indentLevel;
        private bool pendingCloseStartElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriter"/> class.
        /// </summary>
        /// <param name="writer">The inner <see cref="TextWriter"/>.</param>
        public XmlWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Writes &lt;?xml version="1.0" encoding="utf-8"?&gt;.
        /// </summary>
        public void WriteXmlDeclaration()
        {
            this.writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
        }

        /// <summary>
        /// Writes the root element  for <paramref name="value"/> and it's contents.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="value">The value.</param>
        public void WriteRootElement<T>(T value)
        {
            this.WriteElement(RootName.Get(value.GetType()), value);
        }

        /// <summary>
        /// Write an element for <paramref name="value"/> and it's contents.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="name">The element name.</param>
        /// <param name="value">The value.</param>
        public void WriteElement<T>(string name, T value)
        {
            if (value == null)
            {
            }
            else if (DefaultWriterActions.TryGetSimple(value, out var simple))
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
                simple(this.writer, value);
                this.writer.Write("</");
                this.writer.Write(name);
                this.writer.Write(">");
            }
            else if (DefaultWriterActions.TryGetCollection(value, out var itemWriter))
            {
                if (this.pendingCloseStartElement)
                {
                    this.writer.WriteLine(">");
                    this.pendingCloseStartElement = false;
                }

                this.WriteIndentation();
                this.writer.Write("<");
                this.writer.Write(name);
                this.pendingCloseStartElement = true;

                this.indentLevel++;
                itemWriter(this, value);
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
                    attributeWriter.Write(DefaultWriterActions, this.writer, value);
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