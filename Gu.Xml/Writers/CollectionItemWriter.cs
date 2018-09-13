namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    internal static class CollectionItemWriter
    {
        private static readonly CastAction<XmlWriter> EnumerableItemWriter = CastAction<XmlWriter>.Create<IEnumerable>(WriteItems);
        private static readonly CastAction<XmlWriter> DictionaryItemWriter = CastAction<XmlWriter>.Create<IDictionary>(WriteItems);

        internal static CastAction<XmlWriter> Create(Type type, WriteMaps actions)
        {
            if (type.IsArray &&
                type.GetArrayRank() > 1)
            {
                throw new NotSupportedException("Multidimensional arrays are not yet supported. Issue #26.");
            }

            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                if (TryCreateGenericDictionaryWriter(out var writer))
                {
                    return writer;
                }

                return DictionaryItemWriter;
            }
            else
            {
                if (TryCreateGenericEnumerableWriter(out var writer))
                {
                    return writer;
                }

                return EnumerableItemWriter;
            }

            bool TryCreateGenericDictionaryWriter(out CastAction<XmlWriter> result)
            {
                if (type.IsGenericEnumerable(out var enumerableType) &&
                    enumerableType.GenericTypeArguments.TrySingle(out var entryType))
                {
                    if (actions.TryGetWriteMapCached(entryType, out var map))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        result = (CastAction<XmlWriter>)typeof(CollectionItemWriter).GetMethod(nameof(CreateCachedGenericComplexEnumerableWriter), BindingFlags.Static | BindingFlags.NonPublic)
                                                                                .MakeGenericMethod(type, entryType)
                                                                                .Invoke(null, new object[] { "Entry", map });
                        return true;
                    }

                    // ReSharper disable once PossibleNullReferenceException
                    result = (CastAction<XmlWriter>)typeof(CollectionItemWriter).GetMethod(nameof(CreateGenericDictionaryWriter), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                                 .MakeGenericMethod(type, entryType.GenericTypeArguments[0], entryType.GenericTypeArguments[1])
                                                                 .Invoke(null, null);
                    return true;
                }

                result = null;
                return false;
            }

            bool TryCreateGenericEnumerableWriter(out CastAction<XmlWriter> result)
            {
                if (type.IsGenericEnumerable(out var enumerableType) &&
                    enumerableType.GenericTypeArguments.TrySingle(out var elementType))
                {
                    if (actions.TryGetSimpleCached(elementType, out var castAction))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        result = (CastAction<XmlWriter>)typeof(CollectionItemWriter).GetMethod(nameof(CreateCachedGenericSimpleEnumerableWriter), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                                                .MakeGenericMethod(type, elementType)
                                                                                .Invoke(null, new object[] { RootName.Get(elementType), castAction });
                        return true;
                    }

                    if (actions.TryGetWriteMapCached(elementType, out var map))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        result = (CastAction<XmlWriter>)typeof(CollectionItemWriter).GetMethod(nameof(CreateCachedGenericComplexEnumerableWriter), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                                                .MakeGenericMethod(type, elementType)
                                                                                .Invoke(null, new object[] { RootName.Get(elementType), map });
                        return true;
                    }

                    // ReSharper disable once PossibleNullReferenceException
                    result = (CastAction<XmlWriter>)typeof(CollectionItemWriter).GetMethod(nameof(CreateGenericEnumerableWriter), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                                            .MakeGenericMethod(type, elementType)
                                                                            .Invoke(null, null);
                    return true;
                }

                result = null;
                return false;
            }
        }

        private static void WriteItems(XmlWriter writer, IDictionary dictionary)
        {
            foreach (var item in dictionary)
            {
                writer.WriteElement("Entry", item);
                writer.WriteLine();
            }
        }

        private static CastAction<XmlWriter> CreateGenericDictionaryWriter<TDictionary, TKey, TValue>()
            where TDictionary : ICollection<KeyValuePair<TKey, TValue>>
        {
            return CastAction<XmlWriter>.Create<TDictionary>((writer, dictionary) =>
            {
                foreach (var item in dictionary)
                {
                    writer.WriteElement("Entry", item);
                    writer.WriteLine();
                }
            });
        }

        private static void WriteItems(XmlWriter writer, IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                writer.WriteElement(item == null ? "NULL" : RootName.Get(item.GetType()), item);
                writer.WriteLine();
            }
        }

        private static CastAction<XmlWriter> CreateGenericEnumerableWriter<TEnumerable, TValue>()
            where TEnumerable : IEnumerable<TValue>
        {
            return CastAction<XmlWriter>.Create<TEnumerable>((writer, enumerable) =>
            {
                foreach (var item in enumerable)
                {
                    writer.WriteElement(RootName.Get(item?.GetType() ?? typeof(TValue)), item);
                    writer.WriteLine();
                }
            });
        }

        /// <summary>
        /// This is an optimization for collections of sealed simple types.
        /// </summary>
        /// <typeparam name="TEnumerable"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        private static CastAction<XmlWriter> CreateCachedGenericSimpleEnumerableWriter<TEnumerable, TValue>(string elementName, CastAction<TextWriter> castAction)
            where TEnumerable : IEnumerable<TValue>
        {
            var cachedWrite = castAction.Get<TValue>();
            return CastAction<XmlWriter>.Create<TEnumerable>((writer, enumerable) =>
            {
                var textWriter = writer.TextWriter;
                using (var enumerator = enumerable.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        writer.ClosePendingStart();
                        writer.WriteIndentation();
                        textWriter.WriteMany("<", elementName, ">");
                        cachedWrite(textWriter, enumerator.Current);
                        textWriter.WriteMany("</", elementName, ">");
                        textWriter.WriteLine();
                    }

                    while (enumerator.MoveNext())
                    {
                        writer.WriteIndentation();
                        textWriter.WriteMany("<", elementName, ">");
                        cachedWrite(textWriter, enumerator.Current);
                        textWriter.WriteMany("</", elementName, ">");
                        textWriter.WriteLine();
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
        private static CastAction<XmlWriter> CreateCachedGenericComplexEnumerableWriter<TEnumerable, TValue>(string elementName, ComplexWriteMap writeMap)
            where TEnumerable : IEnumerable<TValue>
        {
            return CastAction<XmlWriter>.Create<TEnumerable>((writer, enumerable) =>
            {
                foreach (var item in enumerable)
                {
                    writer.WriteElement(elementName, item, writeMap);
                    writer.WriteLine();
                }
            });
        }
    }
}
