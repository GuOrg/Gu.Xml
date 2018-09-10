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

        public static bool TryCreate(PropertyInfo property, out ElementWriter writer)
        {
            if (property.GetMethod is MethodInfo getMethod &&
                property.GetIndexParameters().Length == 0 &&
                !getMethod.IsStatic &&
                !getMethod.IsPrivate &&
                !getMethod.IsFamily &&
                !IsIgnoredCalculated() &&
                TryGetElementName(property, out var name))
            {
                writer = (ElementWriter)typeof(ElementWriter)
                                                .GetMethod(nameof(CreateWriter), BindingFlags.Static | BindingFlags.NonPublic)
                                                .MakeGenericMethod(property.ReflectedType, property.PropertyType)
                                                .Invoke(null, new object[] { name, property });
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
        }

        public static bool TryCreate(FieldInfo field, out ElementWriter writer)
        {
            if (!field.IsStatic &&
                !field.IsPrivate &&
                !field.IsFamily &&
                TryGetElementName(field, out var name))
            {
                writer = (ElementWriter)typeof(ElementWriter)
                                        .GetMethod(nameof(CreateWriter), BindingFlags.Static | BindingFlags.NonPublic)
                                        .MakeGenericMethod(field.ReflectedType, field.FieldType)
                                        .Invoke(null, new object[] { name, field });
                return true;
            }

            writer = null;
            return false;
        }

        public abstract void Write<T>(XmlWriter writer, T source);

        private static ElementWriter<TSource, TValue> CreateWriter<TSource, TValue>(string name, MemberInfo member)
        {
            switch (member)
            {
                case PropertyInfo property:
                    return new ElementWriter<TSource, TValue>(name, property.CreateGetter<TSource, TValue>());
                case FieldInfo field:
                    return new ElementWriter<TSource, TValue>(name, field.CreateGetter<TSource, TValue>());
                default:
                    throw new InvalidOperationException($"Not handling {member}. Bug in Gu.Xml.");
            }
        }

        private static bool TryGetElementName(MemberInfo member, out string name)
        {
            name = null;
            if (Attribute.GetCustomAttribute(member, typeof(XmlElementAttribute)) is XmlElementAttribute xmlAttribute)
            {
                name = xmlAttribute.ElementName ?? string.Empty;
            }
            else if (Attribute.GetCustomAttribute(member, typeof(SoapElementAttribute)) is SoapElementAttribute soapAttribute)
            {
                name = soapAttribute.ElementName ?? string.Empty;
            }

            if (name == null)
            {
                if (Attribute.GetCustomAttribute(member, typeof(XmlIgnoreAttribute)) != null ||
                    Attribute.GetCustomAttribute(member, typeof(XmlAttributeAttribute)) != null ||
                    Attribute.GetCustomAttribute(member, typeof(SoapIgnoreAttribute)) != null ||
                    Attribute.GetCustomAttribute(member, typeof(SoapAttributeAttribute)) != null)
                {
                    return false;
                }

                name = member.Name;
            }

            if (name == string.Empty)
            {
                name = member.Name;
            }

            return true;
        }
    }
}