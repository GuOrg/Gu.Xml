namespace Gu.Xml
{
    using System;
    using System.Collections;

    public abstract class CollectionItemWriter
    {
        private static readonly EnumerableItemWriter EnumerableItemWriter = new EnumerableItemWriter();
        private static readonly DictionaryItemWriter DictionaryItemWriter = new DictionaryItemWriter();

        public static CollectionItemWriter Create(Type type)
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

            bool TryCreateGenericDictionaryWriter(out CollectionItemWriter result)
            {
                if (type.IsGenericType &&
                    type.GetInterface("ICollection`1") is Type collectionType &&
                    collectionType.GenericTypeArguments.TrySingle(out var kvpType))
                {
                    result = (CollectionItemWriter)Activator.CreateInstance(typeof(DictionaryItemWriter<,>).MakeGenericType(kvpType.GenericTypeArguments));
                    return true;
                }

                result = null;
                return false;
            }

            bool TryCreateGenericEnumerableWriter(out CollectionItemWriter result)
            {
                if (type.IsGenericType &&
                    type.GetInterface("IEnumerable`1") is Type enumerableType &&
                    enumerableType.GenericTypeArguments.TrySingle(out var elementType))
                {
                    result = (CollectionItemWriter)Activator.CreateInstance(typeof(EnumerableItemWriter<>).MakeGenericType(elementType));
                    return true;
                }

                result = null;
                return false;
            }
        }

        public abstract void WriteItems<T>(XmlWriter writer, T value);
    }
}
