namespace Gu.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class ComplexWriteMap : WriteMap
    {
        internal ComplexWriteMap(IReadOnlyList<CastAction<XmlWriter>> attributes, IReadOnlyList<CastAction<XmlWriter>> elements)
        {
            this.Attributes = attributes;
            this.Elements = elements;
        }

        internal IReadOnlyList<CastAction<XmlWriter>> Attributes { get; }

        internal IReadOnlyList<CastAction<XmlWriter>> Elements { get; }

        internal static ComplexWriteMap Create(Type type, WriteMaps actions)
        {
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.FlattenHierarchy);
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            if (!type.IsValueType &&
                type.BaseType != typeof(object))
            {
                Array.Sort(fields, BaseTypeCountComparer.Default);
                Array.Sort(properties, BaseTypeCountComparer.Default);
            }

            return new ComplexWriteMap(
                Attributes().ToArray(),
                Elements().ToArray());

            IEnumerable<CastAction<XmlWriter>> Attributes()
            {
                foreach (var field in fields)
                {
                    if (AttributeAction.TryCreate(field, actions, out var action))
                    {
                        yield return action;
                    }
                }

                foreach (var property in properties)
                {
                    if (AttributeAction.TryCreate(property, actions, out var action))
                    {
                        yield return action;
                    }
                }
            }

            IEnumerable<CastAction<XmlWriter>> Elements()
            {
                foreach (var field in fields)
                {
                    if (ElementAction.TryCreate(field, actions, out var action))
                    {
                        yield return action;
                    }
                }

                foreach (var property in properties)
                {
                    if (ElementAction.TryCreate(property, actions, out var action))
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
            internal static bool TryCreate(PropertyInfo property, WriteMaps actions, out CastAction<XmlWriter> writer)
            {
                writer = null;
                return property.GetMethod is MethodInfo getMethod &&
                       property.GetIndexParameters().Length == 0 &&
                       !getMethod.IsStatic &&
                       !IsIgnoredAccessibility() &&
                       !IsIgnoredCalculated() &&
                       TryGetName(property, out var name) &&
                       TryCreate(new FieldOrProperty(property), name, actions, out writer);

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

            internal static bool TryCreate(FieldInfo field, WriteMaps actions, out CastAction<XmlWriter> writer)
            {
                writer = null;
                return !field.IsStatic &&
                       !field.IsPrivate &&
                       !field.IsFamily &&
                       TryGetName(field, out var name) &&
                       TryCreate(new FieldOrProperty(field), name, actions, out writer);
            }

            private static bool TryCreate(FieldOrProperty fieldOrProperty, string name, WriteMaps actions, out CastAction<XmlWriter> castAction)
            {
                if (actions.TryGetSimpleCached(fieldOrProperty.ValueType, out var valueAction))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    castAction = (CastAction<XmlWriter>)typeof(ElementAction)
                                                        .GetMethod(nameof(CreateSimpleCached), BindingFlags.Static | BindingFlags.NonPublic)
                                                        .MakeGenericMethod(fieldOrProperty.SourceType, fieldOrProperty.ValueType)
                                                        .Invoke(null, new object[] { name, fieldOrProperty.CreateGetter(), valueAction });
                    return true;
                }

                if (actions.TryGetComplexCached(fieldOrProperty.ValueType, out var map))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    castAction = (CastAction<XmlWriter>)typeof(ElementAction)
                                                        .GetMethod(nameof(CreateComplexCached), BindingFlags.Static | BindingFlags.NonPublic)
                                                        .MakeGenericMethod(fieldOrProperty.SourceType, fieldOrProperty.ValueType)
                                                        .Invoke(null, new object[] { name, fieldOrProperty.CreateGetter(), map });
                    return true;
                }

                // ReSharper disable once PossibleNullReferenceException
                castAction = (CastAction<XmlWriter>)typeof(ElementAction)
                                                       .GetMethod(nameof(Create), BindingFlags.Static | BindingFlags.NonPublic)
                                                       .MakeGenericMethod(fieldOrProperty.SourceType, fieldOrProperty.ValueType)
                                                       .Invoke(null, new object[] { name, fieldOrProperty.CreateGetter() });
                return true;
            }

            private static CastAction<XmlWriter> CreateSimpleCached<TSource, TValue>(string name, Func<TSource, TValue> getter, SimpleWriteMap map)
            {
                return CastAction<XmlWriter>.Create<TSource>((writer, source) =>
                {
                    if (getter(source) is TValue value)
                    {
                        map.WriteElement(writer, name, value);
                        writer.WriteLine();
                    }
                });
            }

            private static CastAction<XmlWriter> CreateComplexCached<TSource, TValue>(string name, Func<TSource, TValue> getter, ComplexWriteMap writeMap)
            {
                return CastAction<XmlWriter>.Create<TSource>((writer, source) =>
                {
                    if (getter(source) is TValue value)
                    {
                        writer.WriteElement(name, value, writeMap);
                        writer.WriteLine();
                    }
                });
            }

            private static CastAction<XmlWriter> Create<TSource, TValue>(string name, Func<TSource, TValue> getter)
            {
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
            internal static bool TryCreate(PropertyInfo property, WriteMaps actions, out CastAction<XmlWriter> writer)
            {
                writer = null;
                return TryGetName(property, out var name) &&
                       TryCreate(new FieldOrProperty(property), name, actions, out writer);
            }

            internal static bool TryCreate(FieldInfo field, WriteMaps actions, out CastAction<XmlWriter> writer)
            {
                writer = null;
                return TryGetName(field, out var name) &&
                       TryCreate(new FieldOrProperty(field), name, actions, out writer);
            }

            private static bool TryCreate(FieldOrProperty member, string name, WriteMaps actions, out CastAction<XmlWriter> writer)
            {
                if (actions.TryGetSimpleCached(member.ValueType, out var simpleMap))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    writer = (CastAction<XmlWriter>)typeof(AttributeAction)
                                                    .GetMethod(nameof(CreateCached), BindingFlags.Static | BindingFlags.NonPublic)
                                                    .MakeGenericMethod(member.SourceType, member.ValueType)
                                                    .Invoke(null, new object[] { name, member.CreateGetter(), simpleMap });
                    return true;
                }

                // ReSharper disable once PossibleNullReferenceException
                writer = (CastAction<XmlWriter>)typeof(AttributeAction)
                                                .GetMethod(nameof(Create), BindingFlags.Static | BindingFlags.NonPublic)
                                                .MakeGenericMethod(member.SourceType, member.ValueType)
                                                .Invoke(null, new object[] { name, member.CreateGetter() });
                return true;
            }

            private static CastAction<XmlWriter> Create<TSource, TValue>(string name, Func<TSource, TValue> getter)
            {
                return CastAction<XmlWriter>.Create<TSource>((writer, source) =>
                {
                    if (getter(source) is TValue value)
                    {
                        if (writer.TryGetSimple(value, out var map))
                        {
                            map.WriteAttribute(writer.TextWriter, name, value);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Could not find an Action<TextWriter, {nameof(TValue)}> for {value} of type {value.GetType()}");
                        }
                    }
                });
            }

            private static CastAction<XmlWriter> CreateCached<TSource, TValue>(string name, Func<TSource, TValue> getter, SimpleWriteMap map)
            {
                var valueWriter = map.Write.Get<TValue>();
                return CastAction<XmlWriter>.Create<TSource>((writer, source) =>
                {
                    if (getter(source) is TValue value)
                    {
                        var textWriter = writer.TextWriter;
                        textWriter.WriteMany(" ", name, "=\"");
                        valueWriter.Invoke(textWriter, value);
                        textWriter.Write("\"");
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

        private sealed class BaseTypeCountComparer : IComparer<FieldInfo>, IComparer<PropertyInfo>
        {
            public static readonly BaseTypeCountComparer Default = new BaseTypeCountComparer();

            int IComparer<FieldInfo>.Compare(FieldInfo x, FieldInfo y) => Compare(x, y);

            int IComparer<PropertyInfo>.Compare(PropertyInfo x, PropertyInfo y) => Compare(x, y);

            private static int Compare(MemberInfo x, MemberInfo y)
            {
                var xType = x?.DeclaringType;
                var yType = y?.DeclaringType;
                if (ReferenceEquals(xType, yType))
                {
                    return 0;
                }

                if (xType == null)
                {
                    return -1;
                }

                if (yType == null)
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