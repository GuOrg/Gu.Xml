namespace Gu.Xml
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public abstract class ElementWriter
    {
        protected ElementWriter(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public abstract void Write<T>(XmlWriter writer, T source);

        private static ElementWriter<TSource, TValue> CreateWriter<TSource, TValue>(string name, PropertyInfo property)
        {
            return new ElementWriter<TSource, TValue>(name, property.CreateGetter<TSource,TValue>());
        }

        public static bool TryCreate(PropertyInfo property, out ElementWriter writer)
        {
            if (property.GetMethod is MethodInfo getMethod &&
                property.GetIndexParameters().Length == 0 &&
                !getMethod.IsStatic &&
                !getMethod.IsPrivate &&
                !getMethod.IsFamily &&
                !IsIgnoredCalculated() &&
                Attribute.GetCustomAttribute(property, typeof(XmlIgnoreAttribute)) == null &&
                Attribute.GetCustomAttribute(property, typeof(XmlAttributeAttribute)) == null)
            {
                writer = (ElementWriter)typeof(ElementWriter)
                                                .GetMethod(nameof(CreateWriter),BindingFlags.Static | BindingFlags.NonPublic)
                                                .MakeGenericMethod(property.ReflectedType, property.PropertyType)
                                                .Invoke(null, new object[] { Name(), property });
                return true;
            }

            writer = null;
            return false;

            bool IsIgnoredCalculated()
            {
                return property.SetMethod == null &&
                       Attribute.GetCustomAttribute(property.GetMethod, typeof(CompilerGeneratedAttribute)) == null &&
                       Attribute.GetCustomAttribute(property.GetMethod, typeof(XmlElementAttribute)) == null;
            }

            string Name()
            {
                if (Attribute.GetCustomAttribute(property, typeof(XmlElementAttribute)) is XmlElementAttribute xmlElement &&
                    xmlElement.ElementName is string name)
                {
                    return name;
                }

                return property.Name;
            }
        }
    }
}