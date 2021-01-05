namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;

    internal class FieldsAndProperties
    {
        internal readonly IReadOnlyList<FieldOrProperty> Attributes;

        internal readonly IReadOnlyList<FieldOrProperty> Elements;

        private static readonly ConcurrentDictionary<Type, FieldsAndProperties> Cache = new ConcurrentDictionary<Type, FieldsAndProperties>();

        private FieldsAndProperties(IReadOnlyList<FieldOrProperty> attributes, IReadOnlyList<FieldOrProperty> elements)
        {
            this.Attributes = attributes;
            this.Elements = elements;
        }

        internal static FieldsAndProperties GetOrCreate(Type type) => Cache.GetOrAdd(type, t => Create(t));

        private static FieldsAndProperties Create(Type type)
        {
            var attributes = new List<FieldOrProperty>();
            var elements = new List<FieldOrProperty>();
            foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.FlattenHierarchy))
            {
                var candidate = new FieldOrProperty(field);
                if (!field.IsStatic &&
                    !field.IsPrivate &&
                    !field.IsFamily &&
                    FieldOrProperty.TryGetElementName(candidate, out _))
                {
                    elements.Add(candidate);
                }
                else if (FieldOrProperty.TryGetAttributeName(candidate, out _))
                {
                    attributes.Add(candidate);
                }
            }

            foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy))
            {
                if (TryCreateElement(property, out var element))
                {
                    elements.Add(element);
                }
                else if (new FieldOrProperty(property) is var candidate &&
                         FieldOrProperty.TryGetAttributeName(candidate, out _))
                {
                    attributes.Add(candidate);
                }
            }

            if (!type.IsValueType &&
                type.BaseType != typeof(object))
            {
                elements.Sort(BaseTypeCountComparer.Default);
            }

            return new FieldsAndProperties(attributes, elements);
        }

        private static bool TryCreateElement(PropertyInfo property, out FieldOrProperty fieldOrProperty)
        {
            if (property.GetMethod is { IsStatic: false } getMethod &&
                property.GetIndexParameters().Length == 0 &&
                !IsIgnoredAccessibility() &&
                !IsIgnoredCalculated() &&
                new FieldOrProperty(property) is var candidate &&
                FieldOrProperty.TryGetElementName(candidate, out _))
            {
                fieldOrProperty = candidate;
                return true;
            }

            fieldOrProperty = default;
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
                return property.SetMethod is null &&
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

        private sealed class BaseTypeCountComparer : IComparer<FieldOrProperty>
        {
            public static readonly BaseTypeCountComparer Default = new BaseTypeCountComparer();

            int IComparer<FieldOrProperty>.Compare(FieldOrProperty x, FieldOrProperty y) => Compare(x.MemberInfo, y.MemberInfo);

            private static int Compare(MemberInfo x, MemberInfo y)
            {
                var xType = x?.DeclaringType;
                var yType = y?.DeclaringType;
                if (ReferenceEquals(xType, yType))
                {
                    return 0;
                }

                if (xType is null)
                {
                    return -1;
                }

                if (yType is null)
                {
                    return 1;
                }

                if (xType == yType)
                {
                    return 0;
                }

                return Count(xType).CompareTo(Count(yType));
            }

            private static int Count(Type type)
            {
                var count = 0;
                while (type.BaseType != null)
                {
                    count++;
                    type = type.BaseType;
                }

                return count;
            }
        }
    }
}
