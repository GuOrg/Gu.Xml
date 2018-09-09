namespace Gu.Xml
{
    using System;
    using System.IO;
    using System.Reflection;

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
                    if (SimpleValueWriter.TryGet(value, out var valueWriter))
                    {
                        writer.Write(this.Name);
                        writer.Write("=\"");
                        valueWriter.Write(writer, value);
                        writer.Write("\"");
                    }
                    else
                    {
                        throw new InvalidOperationException($"Could not find a {nameof(SimpleValueWriter)} for {value} of type {value.GetType()}");
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Source is null. Should not get here. Bug in Gu.Xml.");
            }
        }
    }
}