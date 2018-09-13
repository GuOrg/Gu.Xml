namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public static class CollectionWriter
    {
        private static readonly CastAction<XmlWriter> EnumerableItemWriter = CastAction<XmlWriter>.Create<IEnumerable>(WriteItems);
        private static readonly CastAction<XmlWriter> DictionaryItemWriter = CastAction<XmlWriter>.Create<IDictionary>(WriteItems);

        internal static CastAction<XmlWriter> Create(Type type, XmlWriterActions actions)
        {
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
                if (type.IsGenericType &&
                    type.GetInterface("ICollection`1") is Type collectionType &&
                    collectionType.GenericTypeArguments.TrySingle(out var kvpType))
                {
                    // ReSharper disable once PossibleNullReferenceException
                    result = (CastAction<XmlWriter>)typeof(CollectionWriter).GetMethod(nameof(CreateGenericDictionaryWriter), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                                 .MakeGenericMethod(type, kvpType.GenericTypeArguments[0], kvpType.GenericTypeArguments[1])
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
                    if (actions.TryGetSimpleCached(type, out var cached))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        result = (CastAction<XmlWriter>)typeof(CollectionWriter).GetMethod(nameof(CreateCachedGenericSimpleEnumerableWriter), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                                                                                .MakeGenericMethod(type, elementType)
                                                                                .Invoke(null, new object[] { RootName.Get(elementType), cached });
                        return true;
                    }

                    // ReSharper disable once PossibleNullReferenceException
                    result = (CastAction<XmlWriter>)typeof(CollectionWriter).GetMethod(nameof(CreateGenericEnumerableWriter), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
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
    }
}
