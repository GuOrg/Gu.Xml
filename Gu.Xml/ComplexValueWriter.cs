namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Reflection;

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

        public class AttributeWriter
        {
            public AttributeWriter(PropertyInfo member, string name)
            {
                this.Member = member;
                this.Name = name;
            }

            public PropertyInfo Member { get; }

            public string Name { get; }

            public void Write<T>(TextWriter writer, T source)
            {
                throw new NotImplementedException();
            }
        }

        public class ElementWriter
        {
            public ElementWriter(PropertyInfo member, string name)
            {
                this.Member = member;
                this.Name = name;
            }

            public PropertyInfo Member { get; }

            public string Name { get; }

            public void Write<T>(XmlWriter writer, T source)
            {
                var o = this.Member.GetValue(source);
                if (o != null)
                {
                    writer.WriteElement(this.Name, o);
                    writer.WriteLine();
                }
            }
        }
    }
}