namespace Gu.Xml
{
    using System.Text;

    public static partial class Xml
    {
        public static string Serialize<T>(T value)
        {
            var sb = new StringBuilder();
            using (var writer = new XmlWriter(sb))
            {
                writer.WriteXmlDeclaration();
                writer.WriteElement(value.GetType().Name, value);
            }

            return sb.ToString();
        }
    }
}