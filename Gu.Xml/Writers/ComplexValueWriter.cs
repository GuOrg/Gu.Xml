namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class ComplexValueWriter
    {
        private static readonly ConcurrentDictionary<Type, ComplexValueWriter> Default = new ConcurrentDictionary<Type, ComplexValueWriter>();

        public ComplexValueWriter(IReadOnlyList<AttributeWriter> attributes, IReadOnlyList<ElementWriter> elements)
        {
            this.Attributes = attributes;
            this.Elements = elements;
        }

        public IReadOnlyList<AttributeWriter> Attributes { get; }

        public IReadOnlyList<ElementWriter> Elements { get; }

        public static ComplexValueWriter GetOrCreate<T>(T value)
        {
            return Default.GetOrAdd(value.GetType(), x => Create(x));
        }

        private static ComplexValueWriter Create(Type type)
        {
            return new ComplexValueWriter(
                Array.Empty<AttributeWriter>(),
                type.GetProperties().Where(x => x.SetMethod != null || Attribute.GetCustomAttribute(x.GetMethod, typeof(CompilerGeneratedAttribute)) != null)
                                    .Select(x => ElementWriter.Create(x.Name, x)).ToArray());
        }
    }
}