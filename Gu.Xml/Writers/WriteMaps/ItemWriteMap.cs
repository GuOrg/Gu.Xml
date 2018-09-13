namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    internal class ItemWriteMap : WriteMap
    {
        private const string Null = "null";
        private const string Entry = "Entry";

        internal readonly CastAction<XmlWriter> Write;

        private static readonly ItemWriteMap DefaultEnumerableMap = Create<IEnumerable>((writer, enumerable) =>
        {
            foreach (var item in enumerable)
            {
                if (item == null)
                {
                    writer.WriteEmptyElement(Null);
                }
                else
                {
                    writer.WriteElement(RootName.Get(item.GetType()), item);
                    writer.WriteLine();
                }
            }
        });

        private static readonly ItemWriteMap DefaultDictionaryMap = Create<IDictionary>((writer, dictionary) =>
        {
            foreach (var item in dictionary)
            {
                if (item == null)
                {
                    writer.WriteEmptyElement(Null);
                }
                else
                {
                    writer.WriteElement(Entry, item);
                    writer.WriteLine();
                }
            }
        });

        private ItemWriteMap(CastAction<XmlWriter> write)
        {
            this.Write = write;
        }

        internal static ItemWriteMap Create(Type type, WriteMaps actions)
        {
            if (type.IsArray &&
                type.GetArrayRank() > 1)
            {
                throw new NotSupportedException("Multidimensional arrays are not yet supported. Issue #26.");
            }

            if (DictionaryMap.TryCreate(type, actions, out var map) ||
                EnumerableMap.TryCreate(type, actions, out map))
            {
                return map;
            }

            throw new InvalidOperationException("Failed creating ItemWriteMap. Bug in Gu.Xml.");
        }

        private static ItemWriteMap Create<T>(Action<XmlWriter, T> writeItems)
        {
            return new ItemWriteMap(CastAction<XmlWriter>.Create(writeItems));
        }

        private static class DictionaryMap
        {
            internal static bool TryCreate(Type type, WriteMaps actions, out ItemWriteMap result)
            {
                if (!typeof(IDictionary).IsAssignableFrom(type))
                {
                    result = null;
                    return false;
                }

                if (type.IsGenericEnumerable(out var enumerableType) &&
                    enumerableType.GenericTypeArguments.TrySingle(out var entryType))
                {
                    if (actions.TryGetComplexCached(entryType, out var map))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        result = (ItemWriteMap)typeof(EnumerableMap).GetMethod(nameof(EnumerableMap.CreateCachedComplex), BindingFlags.Static | BindingFlags.NonPublic)
                                                                                    .MakeGenericMethod(type, entryType)
                                                                                    .Invoke(null, new object[] { "Entry", map });
                        return true;
                    }

                    // ReSharper disable once PossibleNullReferenceException
                    result = (ItemWriteMap)typeof(DictionaryMap).GetMethod(nameof(Create), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                                                .MakeGenericMethod(type, entryType.GenericTypeArguments[0], entryType.GenericTypeArguments[1])
                                                                                .Invoke(null, null);
                    return true;
                }

                result = DefaultDictionaryMap;
                return true;
            }

            private static ItemWriteMap Create<TDictionary, TKey, TValue>()
                where TDictionary : ICollection<KeyValuePair<TKey, TValue>>
            {
                return Create<TDictionary>((writer, dictionary) =>
                {
                    foreach (var item in dictionary)
                    {
                        writer.WriteElement("Entry", item);
                        writer.WriteLine();
                    }
                });
            }
        }

        private static class EnumerableMap
        {
            internal static bool TryCreate(Type type, WriteMaps actions, out ItemWriteMap result)
            {
                if (!typeof(IEnumerable).IsAssignableFrom(type))
                {
                    result = null;
                    return false;
                }

                if (type.IsGenericEnumerable(out var enumerableType) &&
                    enumerableType.GenericTypeArguments.TrySingle(out var elementType))
                {
                    if (actions.TryGetSimpleCached(elementType, out var simpleMap))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        result = (ItemWriteMap)typeof(EnumerableMap).GetMethod(nameof(CreateCachedSimple), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                                                .MakeGenericMethod(type, elementType)
                                                                                .Invoke(null, new object[] { RootName.Get(elementType), simpleMap });
                        return true;
                    }

                    if (actions.TryGetComplexCached(elementType, out var map))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        result = (ItemWriteMap)typeof(EnumerableMap).GetMethod(nameof(CreateCachedComplex), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                                                .MakeGenericMethod(type, elementType)
                                                                                .Invoke(null, new object[] { RootName.Get(elementType), map });
                        return true;
                    }

                    // ReSharper disable once PossibleNullReferenceException
                    result = (ItemWriteMap)typeof(EnumerableMap).GetMethod(nameof(CreateGenericEnumerableWriter), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                                            .MakeGenericMethod(type, elementType)
                                                                            .Invoke(null, null);
                    return true;
                }

                result = DefaultEnumerableMap;
                return true;
            }

            /// <summary>
            /// This is an optimization for collections of sealed simple types.
            /// </summary>
            /// <typeparam name="TEnumerable"></typeparam>
            /// <typeparam name="TValue"></typeparam>
            /// <returns></returns>
            internal static ItemWriteMap CreateCachedComplex<TEnumerable, TValue>(string elementName, ComplexWriteMap writeMap)
                where TEnumerable : IEnumerable<TValue>
            {
                return Create<TEnumerable>((writer, enumerable) =>
                {
                    foreach (var item in enumerable)
                    {
                        if (item == null)
                        {
                            writer.WriteEmptyElement(Null);
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
            private static ItemWriteMap CreateCachedSimple<TEnumerable, TValue>(string elementName, SimpleWriteMap map)
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

            private static ItemWriteMap CreateGenericEnumerableWriter<TEnumerable, TValue>()
                where TEnumerable : IEnumerable<TValue>
            {
                return Create<TEnumerable>((writer, enumerable) =>
                {
                    foreach (var item in enumerable)
                    {
                        if (item == null)
                        {
                            writer.WriteEmptyElement(Null);
                        }
                        else
                        {
                            writer.WriteElement(RootName.Get(item.GetType()), item);
                            writer.WriteLine();
                        }
                    }
                });
            }
        }
    }
}