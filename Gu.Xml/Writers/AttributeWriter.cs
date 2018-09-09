namespace Gu.Xml
{
    using System;
    using System.IO;
    using System.Reflection;

    public class AttributeWriter
    {
        public AttributeWriter(PropertyInfo member, string name)
        {
            this.Member = member;
            this.Name = name;
        }

        public PropertyInfo Member { get; }

        public string Name { get; }

        public void Write<T>(TextWriter writer, T source)
        {
            throw new NotImplementedException();
        }
    }
}