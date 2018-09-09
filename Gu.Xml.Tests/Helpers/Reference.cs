namespace Gu.Xml.Tests
{
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;

    public static class Reference
    {
        public static string Xml(object value)
        {
            var sb = new StringBuilder();
            var serializer = new XmlSerializer(value.GetType());
            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, value);
            }

            return sb.Replace("utf-16", "utf-8")
                     .Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", "")
                     .ToString();
        }
    }
}
