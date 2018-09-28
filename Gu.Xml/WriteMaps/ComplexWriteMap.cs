namespace Gu.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class ComplexWriteMap : WriteMap
    {
        internal readonly IReadOnlyList<CastAction<XmlWriter>> Attributes;

        internal readonly IReadOnlyList<CastAction<XmlWriter>> Elements;

        private ComplexWriteMap(IReadOnlyList<CastAction<XmlWriter>> attributes, IReadOnlyList<CastAction<XmlWriter>> elements)
        {
            this.Attributes = attributes;
            this.Elements = elements;
        }

        internal static ComplexWriteMap Create(Type type, WriteMaps maps)
        {
            var fops = FieldsAndProperties.GetOrCreate(type);

            return new ComplexWriteMap(
                fops.Attributes.Select(x => AttributeAction.Create(x, maps)).ToArray(),
                fops.Elements.Select(x => ElementAction.Create(x, maps)).ToArray());
        }

        private static class ElementAction
        {
            internal static CastAction<XmlWriter> Create(FieldOrProperty fieldOrProperty, WriteMaps maps)
            {
                if (maps.TryGetSimpleCached(fieldOrProperty.ValueType, out var valueAction))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    return (CastAction<XmlWriter>)typeof(ElementAction)
                                                        .GetMethod(nameof(CreateSimpleCached), BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                                        .MakeGenericMethod(fieldOrProperty.SourceType, fieldOrProperty.ValueType)
                                                        .Invoke(null, new object[] { fieldOrProperty.ElementName(), fieldOrProperty.CreateGetter(), valueAction });
                }

                if (maps.TryGetComplexCached(fieldOrProperty.ValueType, out var map))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    return (CastAction<XmlWriter>)typeof(ElementAction)
                                                        .GetMethod(nameof(CreateComplexCached), BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                                        .MakeGenericMethod(fieldOrProperty.SourceType, fieldOrProperty.ValueType)
                                                        .Invoke(null, new object[] { fieldOrProperty.ElementName(), fieldOrProperty.CreateGetter(), map });
                }

                if (ItemsWriteMap.TryCreate(fieldOrProperty, maps, out var itemsMap))
                {
                    // ReSharper disable once PossibleNullReferenceException
                   return (CastAction<XmlWriter>)typeof(ElementAction)
                                                        .GetMethod(nameof(CreateItemsCached), BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                                        .MakeGenericMethod(fieldOrProperty.SourceType, fieldOrProperty.ValueType)
                                                        .Invoke(null, new object[] { fieldOrProperty.ElementName(), fieldOrProperty.CreateGetter(), itemsMap });
                }

                // ReSharper disable once PossibleNullReferenceException
                return (CastAction<XmlWriter>)typeof(ElementAction)
                                                       .GetMethod(nameof(CreateDefault), BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                                       .MakeGenericMethod(fieldOrProperty.SourceType, fieldOrProperty.ValueType)
                                                       .Invoke(null, new object[] { fieldOrProperty.ElementName(), fieldOrProperty.CreateGetter() });
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

            private static CastAction<XmlWriter> CreateItemsCached<TSource, TValue>(string name, Func<TSource, TValue> getter, ItemsWriteMap writeMap)
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

            private static CastAction<XmlWriter> CreateDefault<TSource, TValue>(string name, Func<TSource, TValue> getter)
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
        }

        private static class AttributeAction
        {
            internal static CastAction<XmlWriter> Create(FieldOrProperty member, WriteMaps maps)
            {
                if (maps.TryGetSimpleCached(member.ValueType, out var map))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    return (CastAction<XmlWriter>)typeof(AttributeAction)
                                                    .GetMethod(nameof(CreateCached), BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                                    .MakeGenericMethod(member.SourceType, member.ValueType)
                                                    .Invoke(null, new object[] { member.AttributeName(), member.CreateGetter(), map });
                }

                // ReSharper disable once PossibleNullReferenceException
                return (CastAction<XmlWriter>)typeof(AttributeAction)
                                                .GetMethod(nameof(CreateDefault), BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                                .MakeGenericMethod(member.SourceType, member.ValueType)
                                                .Invoke(null, new object[] { member.AttributeName(), member.CreateGetter() });
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

            private static CastAction<XmlWriter> CreateDefault<TSource, TValue>(string name, Func<TSource, TValue> getter)
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
        }
    }
}