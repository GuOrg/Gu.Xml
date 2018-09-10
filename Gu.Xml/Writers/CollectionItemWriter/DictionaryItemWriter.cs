namespace Gu.Xml
{
    using System.Collections;

    public class DictionaryItemWriter : CollectionItemWriter
    {
        public override void WriteItems<T>(XmlWriter writer, T value)
        {
            if (value is IDictionary dictionary)
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