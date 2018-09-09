namespace Gu.Xml
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Xml.Serialization;

    public abstract class AttributeWriter
    {
        public AttributeWriter(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public static bool TryCreate(PropertyInfo property, out AttributeWriter writer)
        {
            if (Attribute.GetCustomAttribute(property, typeof(XmlAttributeAttribute)) is XmlAttributeAttribute attribute)
            {
                var name = string.IsNullOrEmpty(attribute.AttributeName) ? property.Name : attribute.AttributeName;
                writer = (AttributeWriter)typeof(AttributeWriter)
                                        .GetMethod(nameof(CreateWriter), BindingFlags.Static | BindingFlags.NonPublic)
                                        .MakeGenericMethod(property.ReflectedType, property.PropertyType)
                                        .Invoke(null, new object[] { name, property });
                return true;
            }

            writer = null;
            return false;
        }

        public abstract void Write<T>(TextWriter writer, T source);

        private static AttributeWriter<TSource, TValue> CreateWriter<TSource, TValue>(string name, PropertyInfo property)
        {
            return new AttributeWriter<TSource, TValue>(name, (Func<TSource, TValue>)Delegate.CreateDelegate(typeof(Func<TSource, TValue>), property.GetMethod));
        }
    }
}