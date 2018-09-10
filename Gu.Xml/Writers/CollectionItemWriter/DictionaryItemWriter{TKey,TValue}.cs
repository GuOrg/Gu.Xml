namespace Gu.Xml
{
    using System.Collections.Generic;

    public class DictionaryItemWriter<TKey, TValue> : CollectionItemWriter
    {
        public override void WriteItems<T>(XmlWriter writer, T value)
        {
            if (value is ICollection<KeyValuePair<TKey, TValue>> dictionary)
            {
                foreach (var item in dictionary)
                {
                    writer.WriteElement("Entry", item);
                    writer.WriteLine();
                }
            }
        }
    }
}