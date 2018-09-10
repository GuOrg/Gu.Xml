namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;

    public abstract class CollectionItemWriter
    {
        private static readonly EnumerableItemWriter EnumerableItemWriter = new EnumerableItemWriter();
        private static readonly DictionaryItemWriter DictionaryItemWriter = new DictionaryItemWriter();
        private static readonly ConcurrentDictionary<Type, CollectionItemWriter> Default = new ConcurrentDictionary<Type, CollectionItemWriter>();

        public static bool TryGet<T>(T value, out CollectionItemWriter writer)
        {
            if (value is IEnumerable)
            {
                writer = Default.GetOrAdd(value.GetType(), x => Create(x));
                return true;
            }

            writer = null;
            return false;
        }

        public abstract void WriteItems<T>(XmlWriter writer, T value);

        private static CollectionItemWriter Create(Type type)
        {
            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                if (TryCreateGenericDictionaryWriter(out var writer))
                {
                    return writer;
                }

                return DictionaryItemWriter;
            }

            return EnumerableItemWriter;

            bool TryCreateGenericDictionaryWriter(out CollectionItemWriter writer)
            {
                if (type.IsGenericType &&
                    type.GetInterface("ICollection`1") is Type collectionType &&
                    collectionType.GenericTypeArguments.TrySingle(out var kvpType))
                {
                    writer = (CollectionItemWriter)Activator.CreateInstance(typeof(DictionaryItemWriter<,>).MakeGenericType(kvpType.GenericTypeArguments));
                    return true;
                }

                writer = null;
                return false;
            }
        }
    }
}
