namespace Gu.Xml
{
    using System.Collections;

    public class EnumerableItemWriter : CollectionItemWriter
    {
        public override void WriteItems<T>(XmlWriter writer, T value)
        {
            if (value is IEnumerable enumerable)
            {
                foreach (var item in enumerable)
                {
                    writer.WriteElement(RootName.Get(item?.GetType() ?? typeof(T).GetElementType()), item);
                    writer.WriteLine();
                }
            }
        }
    }
}