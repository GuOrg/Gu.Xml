namespace Gu.Xml
{
    using System;
    using System.Globalization;
    using System.IO;

    public sealed class XmlWriter : IDisposable
    {
        private readonly TextWriter writer;

        private int indentLevel;
        private bool disposed;

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

            switch (value)
            {
                case int i:
                    this.WriteIndentation();
                    this.WriteStartElement(name);
                    this.WriteInt(i);
                    this.WriteEndElement(name);
                    return;
                case double d:
                    this.WriteIndentation();
                    this.WriteStartElement(name);
                    this.WriteDouble(d);
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

        public void WriteInt(int i)
        {
            this.writer.Write(i.ToString(NumberFormatInfo.InvariantInfo));
        }

        public void WriteDouble(double d)
        {
            // https://www.w3.org/TR/xmlschema-2/#double
            if (double.IsNegativeInfinity(d))
            {
                this.writer.Write("-INF");
                return;
            }

            if (double.IsPositiveInfinity(d))
            {
                this.writer.Write("INF");
                return;
            }

            if (d == 0 &&
                BitConverter.DoubleToInt64Bits(d) != BitConverter.DoubleToInt64Bits(0.0))
            {
                this.writer.Write("-0");
                return;
            }

            this.writer.Write(d.ToString("R", NumberFormatInfo.InvariantInfo));
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