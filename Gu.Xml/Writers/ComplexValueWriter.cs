﻿namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;

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
                type.GetProperties().Select(x => new ElementWriter(x, x.Name)).ToArray());
        }
    }
}