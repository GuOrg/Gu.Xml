namespace Gu.Xml
{
    using System;
    using System.Xml;

    internal class ReadMap
    {
        private readonly Delegate read;

        private ReadMap(Delegate read)
        {
            this.read = read;
        }

        internal static ReadMap CreateSimple<T>(Func<string, T> read) => new ReadMap(new Func<XmlReader, T>(reader => read.Invoke(reader.ReadContentAsString())));

        internal static ReadMap CreateComplex<T>(ReadMaps maps)
        {
            return new ReadMap(new Func<XmlReader, T>(reader =>
            {
                var ctors = typeof(T).GetConstructors();
                if (ctors.TrySingle(out var ctor) &&
                    ctor.GetParameters()
                        .Length == 0)
                {
                    var fieldsAndProperties = FieldsAndProperties.GetOrCreate(typeof(T));
                    var value = (T)Activator.CreateInstance(typeof(T), !ctor.IsPublic);
                    foreach (var element in fieldsAndProperties.Elements)
                    {

                    }
                }

                throw new NotImplementedException("No support for ctor parameters yet.");
            }));
        }

        internal T ReadElement<T>(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element)
            {
                return ((Func<XmlReader, T>)this.read).Invoke(reader);
            }

            throw new InvalidOperationException("Expected node type: Element");
        }

        internal T ReadAttribute<T>(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Attribute)
            {
                return ((Func<XmlReader, T>)this.read).Invoke(reader);
            }

            throw new InvalidOperationException("Expected node type: Element");
        }
    }
}