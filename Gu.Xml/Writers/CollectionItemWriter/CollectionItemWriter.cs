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
                return DictionaryItemWriter;
            }

            return EnumerableItemWriter;
        }
    }
}
