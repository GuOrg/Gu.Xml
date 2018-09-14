namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    internal class ItemsWriteMap : WriteMap
    {
        internal readonly CastAction<XmlWriter> Write;

        private const string Null = "null";
        private const string Entry = "Entry";

        private static readonly ItemsWriteMap DefaultEnumerableMap = EnumerableMap.CreateDefault(null);

        private static readonly ItemsWriteMap DefaultDictionaryMap = DictionaryMap.CreateDefault(null);

        private ItemsWriteMap(CastAction<XmlWriter> write)
        {
            this.Write = write;
        }

        internal static bool TryCreate(FieldOrProperty member, WriteMaps maps, out ItemsWriteMap map)
        {
            if (typeof(IEnumerable).IsAssignableFrom(member.ValueType))
            {
                if (member.MemberInfo.TryGetCustomAttribute<System.Xml.Serialization.XmlArrayItemAttribute>(
                        out var attribute) &&
                    attribute.ElementName is string elementName &&
                    !string.IsNullOrEmpty(elementName))
                {
                    return TryCreate(member.ValueType, maps, elementName, out map);
                }

                return TryCreate(member.ValueType, maps, null, out map);
            }

            map = null;
            return false;
        }

        internal static ItemsWriteMap Create(Type type, WriteMaps maps)
        {
            if (type.IsArray &&
                type.GetArrayRank() > 1)
            {
                throw new NotSupportedException("Multidimensional arrays are not yet supported. Issue #26.");
            }

            if (TryCreate(type, maps, null, out var map))
            {
                return map;
            }

            throw new InvalidOperationException("Failed creating ItemsWriteMap. Bug in Gu.Xml.");
        }

        private static bool TryCreate(Type type, WriteMaps maps, string elementName, out ItemsWriteMap map)
        {
            if (type.IsArray &&
                type.GetArrayRank() > 1)
            {
                map = null;
                return false;
            }

            return DictionaryMap.TryCreate(type, maps, elementName, out map) ||
                   EnumerableMap.TryCreate(type, maps, elementName, out map);
        }

        private static ItemsWriteMap Create<T>(Action<XmlWriter, T> writeItems)
        {
            return new ItemsWriteMap(CastAction<XmlWriter>.Create(writeItems));
        }

        private static class DictionaryMap
        {
            internal static ItemsWriteMap CreateDefault(string elementName)
            {
                elementName = elementName ?? Entry;
                return Create<IDictionary>((writer, dictionary) =>
                {
                    foreach (var item in dictionary)
                    {
                        if (item == null)
                        {
                            writer.WriteEmptyElement(Null);
                            writer.WriteLine();
                        }
                        else
                        {
                            writer.WriteElement(elementName, item);
                            writer.WriteLine();
                        }
                    }
                });
            }

            internal static bool TryCreate(Type type, WriteMaps maps, string elementName, out ItemsWriteMap result)
            {
                if (!typeof(IDictionary).IsAssignableFrom(type))
                {
                    result = null;
                    return false;
                }

                if (type.IsGenericEnumerable(out var enumerableType) &&
                    enumerableType.GenericTypeArguments.TrySingle(out var entryType))
                {
                    if (maps.TryGetComplexCached(entryType, out var map))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        result = (ItemsWriteMap)typeof(EnumerableMap).GetMethod(nameof(EnumerableMap.CreateCachedComplex), BindingFlags.Static | BindingFlags.NonPublic)
                                                                                    .MakeGenericMethod(type, entryType)
                                                                                    .Invoke(null, new object[] { elementName ?? "Entry", map });
                        return true;
                    }

                    // ReSharper disable once PossibleNullReferenceException
                    result = (ItemsWriteMap)typeof(DictionaryMap).GetMethod(nameof(Create), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                                                .MakeGenericMethod(type, entryType.GenericTypeArguments[0], entryType.GenericTypeArguments[1])
                                                                                .Invoke(null, new object[] { elementName });
                    return true;
                }

                result = elementName == null ? DefaultDictionaryMap : CreateDefault(elementName);
                return true;
            }

            private static ItemsWriteMap Create<TDictionary, TKey, TValue>(string elementName)
                where TDictionary : ICollection<KeyValuePair<TKey, TValue>>
            {
                elementName = elementName ?? Entry;
                return Create<TDictionary>((writer, dictionary) =>
                {
                    foreach (var item in dictionary)
                    {
                        writer.WriteElement(elementName, item);
                        writer.WriteLine();
                    }
                });
            }
        }

        private static class EnumerableMap
        {
            internal static ItemsWriteMap CreateDefault(string elementName)
            {
                return Create<IEnumerable>((writer, enumerable) =>
                {
                    foreach (var item in enumerable)
                    {
                        if (item == null)
                        {
                            writer.WriteEmptyElement(Null);
                            writer.WriteLine();
                        }
                        else
                        {
                            writer.WriteElement(elementName ?? RootName.Get(item.GetType()), item);
                            writer.WriteLine();
                        }
                    }
                });
            }

            internal static bool TryCreate(Type type, WriteMaps maps, string elementName, out ItemsWriteMap result)
            {
                if (!typeof(IEnumerable).IsAssignableFrom(type))
                {
                    result = null;
                    return false;
                }

                if (type.IsGenericEnumerable(out var enumerableType) &&
                    enumerableType.GenericTypeArguments.TrySingle(out var elementType))
                {
                    if (maps.TryGetSimpleCached(elementType, out var simpleMap))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        result = (ItemsWriteMap)typeof(EnumerableMap)
                                                .GetMethod(nameof(CreateCachedSimple), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                .MakeGenericMethod(type, elementType)
                                                .Invoke(null, new object[] { elementName ?? RootName.Get(elementType), simpleMap });
                        return true;
                    }

                    if (maps.TryGetComplexCached(elementType, out var complexMap))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        result = (ItemsWriteMap)typeof(EnumerableMap)
                                                .GetMethod(nameof(CreateCachedComplex), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                .MakeGenericMethod(type, elementType)
                                                .Invoke(null, new object[] { elementName ?? RootName.Get(elementType), complexMap });
                        return true;
                    }

                    // ReSharper disable once PossibleNullReferenceException
                    result = (ItemsWriteMap)typeof(EnumerableMap)
                                            .GetMethod(nameof(CreateGeneric), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                            .MakeGenericMethod(type, elementType)
                                            .Invoke(null, new object[] { elementName });
                    return true;
                }

                result = elementName == null ? DefaultEnumerableMap : CreateDefault(elementName);
                return true;
            }

            /// <summary>
            /// This is an optimization for collections of sealed simple types.
            /// </summary>
            /// <typeparam name="TEnumerable"></typeparam>
            /// <typeparam name="TValue"></typeparam>
            /// <returns></returns>
            internal static ItemsWriteMap CreateCachedComplex<TEnumerable, TValue>(string elementName, ComplexWriteMap writeMap)
                where TEnumerable : IEnumerable<TValue>
            {
                return Create<TEnumerable>((writer, enumerable) =>
                {
                    foreach (var item in enumerable)
                    {
                        if (item == null)
                        {
                            writer.WriteEmptyElement(Null);
                            writer.WriteLine();
                        }
                        else
                        {
                            writer.WriteElement(elementName, item, writeMap);
                            writer.WriteLine();
                        }
                    }
                });
            }

            /// <summary>
            /// This is an optimization for collections of sealed simple types.
            /// </summary>
            /// <typeparam name="TEnumerable"></typeparam>
            /// <typeparam name="TValue"></typeparam>
            /// <returns></returns>
            private static ItemsWriteMap CreateCachedSimple<TEnumerable, TValue>(string elementName, SimpleWriteMap map)
                where TEnumerable : IEnumerable<TValue>
            {
                var cachedWrite = map.Write.Get<TValue>();
                return Create<TEnumerable>((writer, enumerable) =>
                {
                    var textWriter = writer.TextWriter;
                    using (var enumerator = enumerable.GetEnumerator())
                    {
                        if (enumerator.MoveNext())
                        {
                            if (enumerator.Current == null)
                            {
                                writer.WriteEmptyElement(Null);
                                writer.WriteLine();
                            }
                            else
                            {
                                writer.ClosePendingStart();
                                writer.WriteIndentation();
                                textWriter.WriteMany("<", elementName, ">");
                                cachedWrite.Invoke(textWriter, enumerator.Current);
                                textWriter.WriteMany("</", elementName, ">");
                                textWriter.WriteLine();
                            }
                        }

                        while (enumerator.MoveNext())
                        {
                            if (enumerator.Current == null)
                            {
                                writer.WriteEmptyElement(Null);
                                writer.WriteLine();
                            }
                            else
                            {
                                writer.WriteIndentation();
                                textWriter.WriteMany("<", elementName, ">");
                                cachedWrite(textWriter, enumerator.Current);
                                textWriter.WriteMany("</", elementName, ">");
                                textWriter.WriteLine();
                            }
                        }
                    }
                });
            }

            private static ItemsWriteMap CreateGeneric<TEnumerable, TValue>(string elementName)
                where TEnumerable : IEnumerable<TValue>
            {
                return Create<TEnumerable>((writer, enumerable) =>
                {
                    foreach (var item in enumerable)
                    {
                        if (item == null)
                        {
                            writer.WriteEmptyElement(Null);
                            writer.WriteLine();
                        }
                        else
                        {
                            writer.WriteElement(elementName ?? RootName.Get(item.GetType()), item);
                            writer.WriteLine();
                        }
                    }
                });
            }
        }
    }
}