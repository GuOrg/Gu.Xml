namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

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
                Elements().ToArray());

            IEnumerable<ElementWriter> Elements()
            {
                foreach (var property in type.GetProperties())
                {
                    if (property.SetMethod != null ||
                        Attribute.GetCustomAttribute(property.GetMethod, typeof(CompilerGeneratedAttribute)) != null)
                    {
                        if (Attribute.GetCustomAttribute(property, typeof(XmlElementAttribute)) is XmlElementAttribute xmlElement)
                        {
                            yield return ElementWriter.Create(xmlElement.ElementName, property);
                        }
                        else
                        {
                            yield return ElementWriter.Create(property.Name, property);
                        }
                    }
                }
            }
        }
    }
}