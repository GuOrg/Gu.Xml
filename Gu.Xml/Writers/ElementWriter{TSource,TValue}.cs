namespace Gu.Xml
{
    using System;

    public class ElementWriter<TSource, TValue> : ElementWriter
    {
        private readonly Func<TSource, TValue> getter;

        public ElementWriter(string name, Func<TSource, TValue> getter)
            : base(name)
        {
            this.getter = getter;
        }

        public override void Write<T>(XmlWriter writer, T source)
        {
            if (source is TSource typedSource &&
                this.getter(typedSource) is TValue value)
            {
                writer.WriteElement(this.Name, value);
                writer.WriteLine();
            }
        }
    }
}