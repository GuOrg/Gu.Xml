using System.Globalization;

namespace Gu.Xml
{
    using System.Text;

    public static partial class Xml
    {
        public static string Serialize<T>(T value)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.WriteElement(value.GetType().Name, value, 0);
            return sb.ToString();
        }

        private static void WriteElement<T>(this StringBuilder sb, string name, T value, int indentLevel)
        {
            sb.AppendStartElement(name, indentLevel);
            switch (value)
            {
                case int i:
                    sb.Append(i.ToString(CultureInfo.InvariantCulture)).AppendEndElement(name);
                    return;
            }

            sb.AppendLine();
            foreach (var property in value.GetType().GetProperties())
            {
                sb.WriteElement(property.Name, property.GetValue(value), 1);
                sb.AppendLine();
            }

            sb.AppendIndentation(indentLevel).AppendEndElement(name);
        }

        private static void AppendStartElement(this StringBuilder sb, string name, int indentLevel)
        {
            sb.AppendIndentation(indentLevel).Append("<").Append(name).Append(">");
        }

        private static void AppendEndElement(this StringBuilder sb, string name)
        {
            sb.Append("</").Append(name).Append(">");
        }

        private static StringBuilder AppendIndentation(this StringBuilder sb, int indentLevel)
        {
            for (var i = 0; i < indentLevel; i++)
            {
                sb.Append("  ");
            }

            return sb;
        }
    }
}