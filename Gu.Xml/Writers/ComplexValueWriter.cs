namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

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
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            return new ComplexValueWriter(
                Attributes().ToArray(),
                Elements().ToArray());

            IEnumerable<AttributeWriter> Attributes()
            {
                foreach (var field in fields)
                {
                    if (AttributeWriter.TryCreate(field, out var writer))
                    {
                        yield return writer;
                    }
                }

                foreach (var property in properties)
                {
                    if (AttributeWriter.TryCreate(property, out var writer))
                    {
                        yield return writer;
                    }
                }
            }

            IEnumerable<ElementWriter> Elements()
            {
                foreach (var field in fields)
                {
                    if (ElementWriter.TryCreate(field, out var writer))
                    {
                        yield return writer;
                    }
                }

                foreach (var property in properties)
                {
                    if (ElementWriter.TryCreate(property, out var writer))
                    {
                        yield return writer;
                    }
                }
            }
        }
    }
}