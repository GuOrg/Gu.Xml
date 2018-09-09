namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    public class ComplexValueWriter
    {
        private static readonly ConcurrentDictionary<Type, ComplexValueWriter> Default = new ConcurrentDictionary<Type, ComplexValueWriter>();

        public ComplexValueWriter(AttributeWriter[] attributes, ElementWriter[] elements)
        {
            this.Attributes = attributes;
            this.Elements = elements;
        }

        public AttributeWriter[] Attributes { get; }

        public ElementWriter[] Elements { get; }

        public static ComplexValueWriter GetOrCreate<T>(T value)
        {
            return Default.GetOrAdd(value.GetType(), x => Create(x));
        }

        private static ComplexValueWriter Create(Type type)
        {
            return new ComplexValueWriter(
                Array.Empty<AttributeWriter>(),
                type.GetProperties().Select(x => new ElementWriter(x, x.Name)).ToArray());
        }
    }
}