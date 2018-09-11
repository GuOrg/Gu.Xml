namespace Gu.Xml.Tests
{
    using System.IO;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    public static class Reference
    {
        public static string XmlSerializer(object value)
        {
            var serializer = new XmlSerializer(value.GetType());
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, value);
            }

            return sb.Replace("utf-16", "utf-8")
                     .Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", string.Empty)
                     .Replace(" xsi:type=\"xsd:boolean\"", string.Empty)
                     .Replace(" xsi:type=\"xsd:int\"", string.Empty)
                     .Replace(" xsi:type=\"xsd:double\"", string.Empty)
                     .ToString();
        }

        public static string XmlSerializerSoap(object value)
        {
#pragma warning disable GU0051 // Cache the XmlSerializer. Fine here in tests.
            var serializer = new XmlSerializer(new SoapReflectionImporter().ImportTypeMapping(value.GetType()));
#pragma warning restore GU0051 // Cache the XmlSerializer.
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, value);
            }

            return sb.Replace("utf-16", "utf-8")
                     .Replace(" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"", string.Empty)
                     .Replace(" id=\"id1\"", string.Empty)
                     .Replace(" xsi:type=\"xsd:boolean\"", string.Empty)
                     .Replace(" xsi:type=\"xsd:int\"", string.Empty)
                     .ToString();
        }

        public static string DataContractSerializer(object value)
        {
            var serializer = new DataContractSerializer(value.GetType());
            var sb = new StringBuilder();
            using (var writer = System.Xml.XmlWriter.Create(new StringWriter(sb), new System.Xml.XmlWriterSettings { Indent = true }))
            {
                serializer.WriteObject(writer, value);
            }

            return sb.Replace("utf-16", "utf-8")
                     .Replace(" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/Gu.Xml.Tests\"", string.Empty)
                     .ToString();
        }
    }
}
