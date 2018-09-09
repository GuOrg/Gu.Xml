namespace Gu.Xml
{
    /// <summary>
    /// Methods for serializing to XML.
    /// </summary>
    public static partial class Xml
    {
        /// <summary>
        /// Serialize <paramref name="value"/> to XML.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="value"/>.</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The string with the XML.</returns>
        public static string Serialize<T>(T value)
        {
            var borrowed = StringWriterPool.Borrow();
            borrowed.Writer.WriteXmlDeclaration();
            borrowed.Writer.WriteRootElement(value);
            var xml = borrowed.Builder.ToString();
            StringWriterPool.Return(borrowed);
            return xml;
        }
    }
}