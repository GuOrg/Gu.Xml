namespace Gu.Xml
{
    using System;
    using System.Globalization;
    using System.IO;

    /// <summary>
    /// Wraps a <see cref="System.IO.TextWriter"/> and exposes methods for writing XML.
    /// </summary>
    public sealed class XmlWriter : IDisposable
    {
#pragma warning disable SA1401 // Fields should be private cheating here, maybe we can clean it up later.
        internal readonly TextWriter TextWriter;
#pragma warning restore SA1401 // Fields should be private

        private static readonly XmlWriterActions DefaultWriterActions = new XmlWriterActions()
            .SimpleClass<string>((writer, value) => writer.Write(value))
            .SimpleStruct<bool>((writer, value) => writer.Write(value ? "true" : "false"))
            .SimpleStruct<byte>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<char>((writer, value) => writer.Write((int)value))
            .SimpleStruct<DateTime>((writer, value) => writer.Write(value.ToString("O", NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<DateTimeOffset>((writer, value) => writer.Write(value.ToString("O", NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<decimal>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<double>((writer, value) => writer.Write(XmlFormat.ToString(value)))
            .SimpleStruct<float>((writer, value) => writer.Write(XmlFormat.ToString(value)))
            .SimpleStruct<Guid>((writer, value) => writer.Write(value.ToString()))
            .SimpleStruct<int>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<IntPtr>((writer, value) => writer.Write(value.ToString()))
            .SimpleStruct<long>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<short>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<TimeSpan>((writer, value) => writer.Write(value.ToString("c", NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<sbyte>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<ulong>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<uint>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)))
            .SimpleStruct<UIntPtr>((writer, value) => writer.Write(value.ToString()))
            .SimpleStruct<ushort>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));

        private int indentLevel;
        private bool pendingCloseStartElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWriter"/> class.
        /// </summary>
        /// <param name="writer">The inner <see cref="System.IO.TextWriter"/>.</param>
        public XmlWriter(TextWriter writer)
        {
            this.TextWriter = writer;
        }

        /// <summary>
        /// Writes &lt;?xml version="1.0" encoding="utf-8"?&gt;.
        /// </summary>
        public void WriteXmlDeclaration()
        {
            this.TextWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
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
                return;
            }

            this.ClosePendingStart();
            if (DefaultWriterActions.TryGetSimple(value, out var simple))
            {
                this.WriteIndentation();
                this.TextWriter.WriteMany("<", name, ">");
                simple(this.TextWriter, value);
                this.TextWriter.WriteMany("</", name, ">");
            }
            else if (DefaultWriterActions.TryGetCollection(value, out var itemWriter))
            {
                this.WriteIndentation();
                this.TextWriter.Write("<");
                this.TextWriter.Write(name);
                this.pendingCloseStartElement = true;

                this.indentLevel++;
                itemWriter(this, value);
                this.indentLevel--;

                if (this.pendingCloseStartElement)
                {
                    this.TextWriter.Write(" />");
                    this.pendingCloseStartElement = false;
                }
                else
                {
                    this.WriteIndentation();
                    this.TextWriter.WriteMany("</", name, ">");
                }
            }
            else if (DefaultWriterActions.TryGetWriteMap(value, out var map))
            {
                this.WriteElement(name, value, map);
            }
            else
            {
                throw new InvalidOperationException($"Failed getting a value writer for {value}");
            }
        }

        internal void ClosePendingStart()
        {
            if (this.pendingCloseStartElement)
            {
                this.TextWriter.WriteLine(">");
                this.pendingCloseStartElement = false;
            }
        }

        internal void WriteElement<T>(string name, T value, WriteMap map)
        {
            this.ClosePendingStart();

            this.WriteIndentation();
            this.TextWriter.Write("<");
            this.TextWriter.Write(name);
            foreach (var castAction in map.Attributes)
            {
                castAction.Invoke(this, value);
            }

            this.pendingCloseStartElement = true;

            this.indentLevel++;
            foreach (var elementWriter in map.Elements)
            {
                elementWriter.Write(this, value);
            }

            this.indentLevel--;
            if (this.pendingCloseStartElement)
            {
                this.TextWriter.Write(" />");
                this.pendingCloseStartElement = false;
            }
            else
            {
                this.WriteIndentation();
                this.TextWriter.WriteMany("</", name, ">");
            }
        }

        public void WriteLine()
        {
            this.TextWriter.WriteLine();
        }

        public bool TryGetSimple<TValue>(TValue value, out Action<TextWriter, TValue> writer)
        {
            return DefaultWriterActions.TryGetSimple(value, out writer);
        }

        public void Dispose()
        {
#pragma warning disable IDISP007 // Don't dispose injected.
            this.TextWriter.Dispose();
#pragma warning restore IDISP007 // Don't dispose injected.
        }

        internal void Reset()
        {
            System.Diagnostics.Debug.Assert(this.indentLevel == 0, "this.indentLevel == 0");
            this.indentLevel = 0;
            System.Diagnostics.Debug.Assert(!this.pendingCloseStartElement, "!this.pendingCloseStartElement");
            this.pendingCloseStartElement = false;
        }

        internal void WriteIndentation()
        {
            for (var i = 0; i < this.indentLevel; i++)
            {
                this.TextWriter.Write("  ");
            }
        }
    }
}