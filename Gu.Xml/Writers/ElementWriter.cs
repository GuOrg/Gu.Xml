namespace Gu.Xml
{
    using System.Reflection;

    public class ElementWriter
    {
        public ElementWriter(PropertyInfo member, string name)
        {
            this.Member = member;
            this.Name = name;
        }

        public PropertyInfo Member { get; }

        public string Name { get; }

        public void Write<T>(XmlWriter writer, T source)
        {
            var o = this.Member.GetValue(source);
            if (o != null)
            {
                writer.WriteElement(this.Name, o);
                writer.WriteLine();
            }
        }
    }
}