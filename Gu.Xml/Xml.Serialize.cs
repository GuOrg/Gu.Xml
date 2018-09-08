namespace Gu.Xml
{
    public static partial class Xml
    {
        public static string Serialize<T>(T value)
        {
            var borrowed = StringWriterPool.Borrow();
            borrowed.Writer.WriteXmlDeclaration();
            borrowed.Writer.WriteElement(value.GetType().Name, value);
            var xml = borrowed.Builder.ToString();
            StringWriterPool.Return(borrowed);
            return xml;
        }
    }
}