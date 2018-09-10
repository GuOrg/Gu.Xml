namespace Gu.Xml
{
    using System.Collections;
    using System.Collections.Generic;

    public class EnumerableItemWriter<TValue> : CollectionItemWriter
    {
        public override void WriteItems<T>(XmlWriter writer, T value)
        {
            if (value is IEnumerable<TValue> enumerable)
            {
                foreach (var item in enumerable)
                {
                    writer.WriteElement(RootName.Get(item?.GetType() ?? typeof(TValue)), item);
                    writer.WriteLine();
                }
            }
        }
    }
}