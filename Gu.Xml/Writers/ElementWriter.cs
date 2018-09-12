namespace Gu.Xml
{
    using System;
    using System.Reflection;

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
                !IsIgnoredAccessibility() &&
                !IsIgnoredCalculated() &&
                TryGetElementName(property, out var name))
            {
                // ReSharper disable once PossibleNullReferenceException
                writer = (ElementWriter)typeof(ElementWriter)
                                                .GetMethod(nameof(CreateWriter), BindingFlags.Static | BindingFlags.NonPublic)
                                                .MakeGenericMethod(property.ReflectedType, property.PropertyType)
                                                .Invoke(null, new object[] { name, property });
                return true;
            }

            writer = null;
            return false;

            bool IsIgnoredAccessibility()
            {
                if (getMethod.IsPrivate ||
                    getMethod.IsFamily)
                {
                    return !property.TryGetCustomAttribute<System.Xml.Serialization.XmlElementAttribute>(out _) &&
                           !property.TryGetCustomAttribute<System.Xml.Serialization.XmlArrayAttribute>(out _) &&
                           !property.TryGetCustomAttribute<System.Xml.Serialization.SoapElementAttribute>(out _) &&
                           !property.TryGetCustomAttribute<System.Runtime.Serialization.DataMemberAttribute>(out _);
                }

                return false;
            }

            bool IsIgnoredCalculated()
            {
                return property.SetMethod == null &&
                       !property.GetMethod.TryGetCustomAttribute<System.Runtime.CompilerServices.CompilerGeneratedAttribute>(out _) &&
                       !property.GetMethod.TryGetCustomAttribute<System.Xml.Serialization.XmlElementAttribute>(out _) &&
                       !property.DeclaringType.IsAnonymous() &&
                       !HasBackingField();
            }

            bool HasBackingField()
            {
                if (property.DeclaringType is Type declaringType)
                {
                    foreach (var field in declaringType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.DeclaredOnly))
                    {
                        if (string.Equals(property.Name, field.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }

                        if (field.Name.StartsWith("_") &&
                            field.Name.Length == property.Name.Length + 1 &&
                            field.Name.IndexOf(property.Name, StringComparison.OrdinalIgnoreCase) == 1)
                        {
                            return true;
                        }

                        if (field.Name.StartsWith("m_") &&
                            field.Name.Length == property.Name.Length + 2 &&
                            field.Name.IndexOf(property.Name, StringComparison.OrdinalIgnoreCase) == 2)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public static bool TryCreate(FieldInfo field, out ElementWriter writer)
        {
            if (!field.IsStatic &&
                !field.IsPrivate &&
                !field.IsFamily &&
                TryGetElementName(field, out var name))
            {
                // ReSharper disable once PossibleNullReferenceException
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
            if (TryGetNameFromAttribute<System.Xml.Serialization.XmlElementAttribute>(member, x => x.ElementName, out name) ||
                TryGetNameFromAttribute<System.Xml.Serialization.SoapElementAttribute>(member, x => x.ElementName, out name) ||
                TryGetNameFromAttribute<System.Xml.Serialization.XmlArrayAttribute>(member, x => x.ElementName, out name) ||
                TryGetNameFromAttribute<System.Runtime.Serialization.DataMemberAttribute>(member, x => x.Name, out name))
            {
                return true;
            }

            if (name == null)
            {
                if (member.TryGetCustomAttribute<System.Xml.Serialization.XmlIgnoreAttribute>(out _) ||
                    member.TryGetCustomAttribute<System.Xml.Serialization.XmlAttributeAttribute>(out _) ||
                    member.TryGetCustomAttribute<System.Xml.Serialization.SoapIgnoreAttribute>(out _) ||
                    member.TryGetCustomAttribute<System.Xml.Serialization.SoapAttributeAttribute>(out _) ||
                    member.TryGetCustomAttribute<System.Runtime.Serialization.IgnoreDataMemberAttribute>(out _))
                {
                    return false;
                }

                name = member.Name;
            }
            else if (name == string.Empty)
            {
                name = member.Name;
            }

            if (name.TryLastIndexOf('.', out var i))
            {
                name = name.Substring(i + 1);
            }

            return name.Length > 0;
        }

        private static bool TryGetNameFromAttribute<TAttribute>(MemberInfo member, Func<TAttribute, string> getName, out string name)
            where TAttribute : Attribute
        {
            name = null;
            if (member.TryGetCustomAttribute(out TAttribute attribute))
            {
                name = getName(attribute);
            }

            return !string.IsNullOrEmpty(name);
        }
    }
}