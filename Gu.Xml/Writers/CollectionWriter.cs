namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class CollectionWriter
    {
        private static readonly CastAction<XmlWriter> EnumerableItemWriter = CastAction<XmlWriter>.Create<IEnumerable>(WriteItems);
        private static readonly CastAction<XmlWriter> DictionaryItemWriter = CastAction<XmlWriter>.Create<IDictionary>(WriteItems);

        public static CastAction<XmlWriter> Create(Type type)
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
                if (type.IsGenericType &&
                    type.GetInterface("IEnumerable`1") is Type enumerableType &&
                    enumerableType.GenericTypeArguments.TrySingle(out var elementType))
                {
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
            return CastAction<XmlWriter>.Create<TDictionary>(WriteItems<TDictionary, TKey, TValue>);
        }

        private static void WriteItems<TDictionary, TKey, TValue>(XmlWriter writer, TDictionary dictionary)
            where TDictionary : ICollection<KeyValuePair<TKey, TValue>>
        {
            foreach (var item in dictionary)
            {
                writer.WriteElement("Entry", item);
                writer.WriteLine();
            }
        }

        private static void WriteItems(XmlWriter writer, IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                var name = item == null ? "NULL" : RootName.Get(item.GetType());
                writer.WriteElement(name, item);
                writer.WriteLine();
            }
        }

        private static CastAction<XmlWriter> CreateGenericEnumerableWriter<TEnumerable, TValue>()
            where TEnumerable : IEnumerable<TValue>
        {
            return CastAction<XmlWriter>.Create<TEnumerable>(WriteItems<TEnumerable, TValue>);
        }

        private static void WriteItems<TEnumerable, TValue>(XmlWriter writer, TEnumerable enumerable)
            where TEnumerable : IEnumerable<TValue>
        {
            foreach (var item in enumerable)
            {
                writer.WriteElement(RootName.Get(item?.GetType() ?? typeof(TValue)), item);
                writer.WriteLine();
            }
        }
    }
}
