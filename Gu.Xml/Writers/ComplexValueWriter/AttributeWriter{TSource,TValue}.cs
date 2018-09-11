namespace Gu.Xml
{
    using System;
    using System.IO;

    public class AttributeWriter<TSource, TValue> : AttributeWriter
    {
        private readonly Func<TSource, TValue> getter;

        public AttributeWriter(string name, Func<TSource, TValue> getter)
            : base(name)
        {
            this.getter = getter;
        }

        public override void Write<T>(TextWriter writer, T source)
        {
            if (source is TSource typedSource)
            {
                if (this.getter(typedSource) is TValue value)
                {
                    if (WriterAction.TryGetSimple(value, out var valueWriter))
                    {
                        writer.Write(" ");
                        writer.Write(this.Name);
                        writer.Write("=\"");
                        valueWriter(writer, value);
                        writer.Write("\"");
                    }
                    else
                    {
                        throw new InvalidOperationException($"Could not find a Action<TextWriter, {nameof(TValue)}> for {value} of type {value.GetType()}");
                    }
                }
            }
            else
            {
                throw new InvalidOperationException($"Source is {source}. Should not get here. Bug in Gu.Xml.");
            }
        }
    }
}