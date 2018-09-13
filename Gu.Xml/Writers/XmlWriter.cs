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

        private static readonly WriteMaps DefaultMaps = new WriteMaps()
            .RegisterSimple<string>((writer, value) => writer.Write(value))
            .RegisterSimple<bool>((writer, value) => writer.Write(value ? "true" : "false"))
            .RegisterSimple<byte>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)))
            .RegisterSimple<char>((writer, value) => writer.Write((int)value))
            .RegisterSimple<DateTime>((writer, value) => writer.Write(value.ToString("O", NumberFormatInfo.InvariantInfo)))
            .RegisterSimple<DateTimeOffset>((writer, value) => writer.Write(value.ToString("O", NumberFormatInfo.InvariantInfo)))
            .RegisterSimple<decimal>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)))
            .RegisterSimple<double>((writer, value) => writer.Write(XmlFormat.ToString(value)))
            .RegisterSimple<float>((writer, value) => writer.Write(XmlFormat.ToString(value)))
            .RegisterSimple<Guid>((writer, value) => writer.Write(value.ToString()))
            .RegisterSimple<int>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)))
            .RegisterSimple<IntPtr>((writer, value) => writer.Write(value.ToString()))
            .RegisterSimple<long>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)))
            .RegisterSimple<short>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)))
            .RegisterSimple<TimeSpan>((writer, value) => writer.Write(value.ToString("c", NumberFormatInfo.InvariantInfo)))
            .RegisterSimple<sbyte>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)))
            .RegisterSimple<ulong>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)))
            .RegisterSimple<uint>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)))
            .RegisterSimple<UIntPtr>((writer, value) => writer.Write(value.ToString()))
            .RegisterSimple<ushort>((writer, value) => writer.Write(value.ToString(null, NumberFormatInfo.InvariantInfo)));

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
            if (DefaultMaps.TryGetSimple(value, out var simple))
            {
                simple.WriteElement(this, name, value);
            }
            else if (DefaultMaps.TryGetCollection(value, out var items))
            {
                this.WriteIndentation();
                this.TextWriter.Write("<");
                this.TextWriter.Write(name);
                this.pendingCloseStartElement = true;

                this.indentLevel++;
                items.Write.Invoke(this, value);
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
            else if (DefaultMaps.TryGetComplex(value, out var map))
            {
                this.WriteElement(name, value, map);
            }
            else
            {
                throw new InvalidOperationException($"Failed getting a value writer for {value}");
            }
        }

        public void WriteLine()
        {
            this.TextWriter.WriteLine();
        }

        public void Dispose()
        {
#pragma warning disable IDISP007 // Don't dispose injected.
            this.TextWriter.Dispose();
#pragma warning restore IDISP007 // Don't dispose injected.
        }

        internal void ClosePendingStart()
        {
            if (this.pendingCloseStartElement)
            {
                this.TextWriter.WriteLine(">");
                this.pendingCloseStartElement = false;
            }
        }

        internal void WriteElement<T>(string name, T value, ComplexWriteMap writeMap)
        {
            this.ClosePendingStart();
            this.WriteIndentation();
            this.TextWriter.Write("<");
            this.TextWriter.Write(name);
            foreach (var castAction in writeMap.Attributes)
            {
                castAction.Invoke(this, value);
            }

            this.pendingCloseStartElement = true;

            this.indentLevel++;
            foreach (var elementWriter in writeMap.Elements)
            {
                elementWriter.Invoke(this, value);
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

        internal void WriteEmptyElement(string name)
        {
            this.ClosePendingStart();
            this.WriteIndentation();
            this.TextWriter.WriteMany("<", name, " />");
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

        internal bool TryGetSimple<TValue>(TValue value, out SimpleWriteMap map)
        {
            return DefaultMaps.TryGetSimple(value, out map);
        }
    }
}