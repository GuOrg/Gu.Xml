namespace Gu.Xml
{
    using System;
    using System.IO;

    public class SimpleValueWriter<TValue> : SimpleValueWriter
    {
        private readonly Action<TextWriter, TValue> write;

        public SimpleValueWriter(Action<TextWriter, TValue> write)
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

            throw new InvalidOperationException($"SimpleValueWriter{typeof(TValue)} was called with value of type {typeof(T)}. Bug in Gu.Xml.");
        }
    }
}