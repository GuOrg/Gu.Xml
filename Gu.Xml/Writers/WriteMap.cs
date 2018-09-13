namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal class WriteMap
    {
        internal WriteMap(IReadOnlyList<CastAction<XmlWriter>> attributes, IReadOnlyList<CastAction<XmlWriter>> elements)
        {
            this.Attributes = attributes;
            this.Elements = elements;
        }

        internal IReadOnlyList<CastAction<XmlWriter>> Attributes { get; }

        internal IReadOnlyList<CastAction<XmlWriter>> Elements { get; }

        internal static WriteMap Create(Type type)
        {
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.FlattenHierarchy);
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            if (!type.IsValueType &&
                type.BaseType != typeof(object))
            {
                Array.Sort(fields, BaseTypeCountComparer.Default);
                Array.Sort(properties, BaseTypeCountComparer.Default);
            }

            return new WriteMap(
                Attributes().ToArray(),
                Elements().ToArray());

            IEnumerable<CastAction<XmlWriter>> Attributes()
            {
                foreach (var field in fields)
                {
                    if (AttributeAction.TryCreate(field, out var action))
                    {
                        yield return action;
                    }
                }

                foreach (var property in properties)
                {
                    if (AttributeAction.TryCreate(property, out var action))
                    {
                        yield return action;
                    }
                }
            }

            IEnumerable<CastAction<XmlWriter>> Elements()
            {
                foreach (var field in fields)
                {
                    if (ElementAction.TryCreate(field, out var action))
                    {
                        yield return action;
                    }
                }

                foreach (var property in properties)
                {
                    if (ElementAction.TryCreate(property, out var action))
                    {
                        yield return action;
                    }
                }
            }
        }

        private static bool TryGetNameFromAttribute<TAttribute>(MemberInfo member, Func<TAttribute, string> getName, out string name)
            where TAttribute : Attribute
        {
            if (member.TryGetCustomAttribute(out TAttribute attribute))
            {
                name = getName(attribute);
                if (string.IsNullOrEmpty(name))
                {
                    name = member.Name;
                }

                return true;
            }

            name = null;
            return false;
        }

        private static class ElementAction
        {
            internal static bool TryCreate(PropertyInfo property, out CastAction<XmlWriter> writer)
            {
                if (property.GetMethod is MethodInfo getMethod &&
                    property.GetIndexParameters().Length == 0 &&
                    !getMethod.IsStatic &&
                    !IsIgnoredAccessibility() &&
                    !IsIgnoredCalculated() &&
                    TryGetName(property, out var name))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    writer = (CastAction<XmlWriter>)typeof(ElementAction)
                                                    .GetMethod(nameof(Create), BindingFlags.Static | BindingFlags.NonPublic)
                                                    .MakeGenericMethod(property.ReflectedType, property.PropertyType)
                                                    .Invoke(null, new object[] { name, property.CreateGetter() });
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

            internal static bool TryCreate(FieldInfo field, out CastAction<XmlWriter> writer)
            {
                if (!field.IsStatic &&
                    !field.IsPrivate &&
                    !field.IsFamily &&
                    TryGetName(field, out var name))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    writer = (CastAction<XmlWriter>)typeof(ElementAction)
                                            .GetMethod(nameof(Create), BindingFlags.Static | BindingFlags.NonPublic)
                                            .MakeGenericMethod(field.ReflectedType, field.FieldType)
                                            .Invoke(null, new object[] { name, field.CreateGetter() });
                    return true;
                }

                writer = null;
                return false;
            }

            private static CastAction<XmlWriter> Create<TSource, TValue>(string name, Func<TSource, TValue> getter)
            {
                // Caching via closure here.
                return CastAction<XmlWriter>.Create<TSource>((writer, source) =>
                {
                    if (getter(source) is TValue value)
                    {
                        writer.WriteElement(name, value);
                        writer.WriteLine();
                    }
                });
            }

            private static bool TryGetName(MemberInfo member, out string name)
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
        }

        private static class AttributeAction
        {
            /// <summary>
            /// Check if <paramref name="property"/> has any attributes like [XmlAttribute].
            /// </summary>
            /// <param name="property">The <see cref="PropertyInfo"/>.</param>
            /// <param name="writer">The <see cref="AttributeAction"/>.</param>
            /// <returns>True if an <see cref="AttributeAction"/> was created.</returns>
            internal static bool TryCreate(PropertyInfo property, out CastAction<XmlWriter> writer)
            {
                if (TryGetName(property, out var name))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    writer = (CastAction<XmlWriter>)typeof(AttributeAction)
                                              .GetMethod(nameof(Create), BindingFlags.Static | BindingFlags.NonPublic)
                                              .MakeGenericMethod(property.ReflectedType, property.PropertyType)
                                              .Invoke(null, new object[] { name, property.CreateGetter() });
                    return true;
                }

                writer = null;
                return false;
            }

            internal static bool TryCreate(FieldInfo field, out CastAction<XmlWriter> writer)
            {
                if (TryGetName(field, out var name))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    writer = (CastAction<XmlWriter>)typeof(AttributeAction)
                                         .GetMethod(nameof(Create), BindingFlags.Static | BindingFlags.NonPublic)
                                         .MakeGenericMethod(field.ReflectedType, field.FieldType)
                                         .Invoke(null, new object[] { name, field.CreateGetter() });
                    return true;
                }

                writer = null;
                return false;
            }

            private static CastAction<XmlWriter> Create<TSource, TValue>(string name, Func<TSource, TValue> getter)
            {
                // Caching via closure here.
                Action<TextWriter, TValue> cachedWriter = null;

                return CastAction<XmlWriter>.Create<TSource>((writer, source) =>
                {
                    if (getter(source) is TValue value)
                    {
                        if (cachedWriter != null ||
                            writer.TryGetSimple(value, out cachedWriter))
                        {
                            var textWriter = writer.TextWriter;
                            textWriter.WriteMany(" ", name, "=\"");
                            cachedWriter(textWriter, value);
                            textWriter.Write("\"");
                            if (!typeof(TValue).IsSealed)
                            {
                                // We can't cache it as we are not sure it is the same type.
                                cachedWriter = null;
                            }
                        }
                        else
                        {
                            throw new InvalidOperationException($"Could not find an Action<TextWriter, {nameof(TValue)}> for {value} of type {value.GetType()}");
                        }
                    }
                });
            }

            private static bool TryGetName(MemberInfo member, out string name)
            {
                if (TryGetNameFromAttribute<System.Xml.Serialization.XmlAttributeAttribute>(member, x => x.AttributeName, out name) ||
                    TryGetNameFromAttribute<System.Xml.Serialization.SoapAttributeAttribute>(member, x => x.AttributeName, out name))
                {
                    if (name == string.Empty)
                    {
                        name = member.Name;
                    }

                    return true;
                }

                return false;
            }
        }

        private sealed class BaseTypeCountComparer : IComparer<MemberInfo>, IComparer
        {
            public static readonly BaseTypeCountComparer Default = new BaseTypeCountComparer();

            public int Compare(MemberInfo x, MemberInfo y)
            {
                var xType = x.DeclaringType;
                var yType = y.DeclaringType;
                if (xType == yType)
                {
                    return 0;
                }

                return Count(xType).CompareTo(Count(yType));
            }

            int IComparer.Compare(object x, object y)
            {
                if (x is MemberInfo xp &&
                    y is MemberInfo yp)
                {
                    return this.Compare(xp, yp);
                }

                return 0;
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