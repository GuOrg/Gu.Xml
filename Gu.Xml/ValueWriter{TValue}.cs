namespace Gu.Xml
{
    using System;
    using System.IO;

    public class ValueWriter<TValue> : ValueWriter
    {
        private readonly Action<TextWriter, TValue> write;

        public ValueWriter(Action<TextWriter, TValue> write)
        {
            this.write = write;
        }

        public override void Write<T>(TextWriter writer, T value)
        {
            if (value is TValue typedValue)
            {
                this.write(writer, typedValue);
                return;
            }

            throw new InvalidOperationException();
        }
    }
}