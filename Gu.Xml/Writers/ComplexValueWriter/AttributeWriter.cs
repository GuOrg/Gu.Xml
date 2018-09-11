namespace Gu.Xml
{
    using System;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Base class for an attribute writer used by <see cref="XmlWriter"/>
    /// </summary>
    public abstract class AttributeWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWriter"/> class.
        /// </summary>
        /// <param name="name">The name of the attribute to write.</param>
        public AttributeWriter(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets name of the attribute to write.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Check if <paramref name="property"/> has any attributes like [XmlAttribute].
        /// </summary>
        /// <param name="property">The <see cref="PropertyInfo"/>.</param>
        /// <param name="writer">The <see cref="AttributeWriter"/>.</param>
        /// <returns>True if an <see cref="AttributeWriter"/> was created.</returns>
        public static bool TryCreate(PropertyInfo property, out AttributeWriter writer)
        {
            if (TryGetAttributeName(property, out var name))
            {
                writer = (AttributeWriter)typeof(AttributeWriter)
                                        .GetMethod(nameof(CreateWriter), BindingFlags.Static | BindingFlags.NonPublic)
                                        .MakeGenericMethod(property.ReflectedType, property.PropertyType)
                                        .Invoke(null, new object[] { name, property });
                return true;
            }

            writer = null;
            return false;
        }

        public static bool TryCreate(FieldInfo field, out AttributeWriter writer)
        {
            if (TryGetAttributeName(field, out var name))
            {
                writer = (AttributeWriter)typeof(AttributeWriter)
                                          .GetMethod(nameof(CreateWriter), BindingFlags.Static | BindingFlags.NonPublic)
                                          .MakeGenericMethod(field.ReflectedType, field.FieldType)
                                          .Invoke(null, new object[] { name, field });
                return true;
            }

            writer = null;
            return false;
        }

        public abstract void Write<T>(WriterActions writerActions, TextWriter writer, T source);

        private static AttributeWriter<TSource, TValue> CreateWriter<TSource, TValue>(string name, MemberInfo member)
        {
            switch (member)
            {
                case PropertyInfo property:
                    return new AttributeWriter<TSource, TValue>(name, property.CreateGetter<TSource, TValue>());
                case FieldInfo field:
                    return new AttributeWriter<TSource, TValue>(name, field.CreateGetter<TSource, TValue>());
                default:
                    throw new InvalidOperationException($"Not handling {member}. Bug in Gu.Xml.");
            }
        }

        private static bool TryGetAttributeName(MemberInfo member, out string name)
        {
            name = null;
            if (member.TryGetCustomAttribute(out System.Xml.Serialization.XmlAttributeAttribute xmlAttribute))
            {
                name = xmlAttribute.AttributeName ?? string.Empty;
            }
            else if (member.TryGetCustomAttribute(out System.Xml.Serialization.SoapAttributeAttribute soapAttribute))
            {
                name = soapAttribute.AttributeName ?? string.Empty;
            }

            if (name == null)
            {
                return false;
            }

            if (name == string.Empty)
            {
                name = member.Name;
            }

            return true;
        }
    }
}