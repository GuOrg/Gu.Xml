namespace Gu.Xml
{
    using System;
    using System.Collections.Concurrent;
    using System.Text;

    internal static class RootName
    {
        private static readonly ConcurrentDictionary<Type, string> Cache = new ConcurrentDictionary<Type, string>();

        public static string Get(Type type)
        {
            return Cache.GetOrAdd(type, t => Create(t));
        }

        private static string Create(Type type)
        {
            if (TryGetNameFromAttribute<System.Xml.Serialization.XmlRootAttribute>(type, x => x.ElementName, out var name) ||
                TryGetNameFromAttribute<System.Xml.Serialization.SoapTypeAttribute>(type, x => x.TypeName, out name) ||
                TryGetNameFromAttribute<System.Runtime.Serialization.DataContractAttribute>(type, x => x.Name, out name))
            {
                return name;
            }

            if (!type.IsGenericType &&
                !type.HasElementType)
            {
                return type.Name;
            }

            var builder = new StringBuilder();
            Append(type);
            return builder.ToString();

            void Append(Type current)
            {
                if (current.IsGenericType)
                {
                    builder.Append(current.Name, 0, current.Name.IndexOf('`'));
                    builder.Append("Of");
                    foreach (var argument in current.GenericTypeArguments)
                    {
                        Append(argument);
                    }
                }
                else if (current.IsArray)
                {
                    if (current.HasElementType)
                    {
                        builder.Append("ArrayOf");
                        Append(current.GetElementType());
                    }
                    else
                    {
                        builder.Append("Array");
                    }
                }
                else
                {
                    builder.Append(current.Name);
                }
            }
        }

        private static bool TryGetNameFromAttribute<T>(Type type, Func<T, string> getName, out string name)
            where T : Attribute
        {
            name = null;
            if (Attribute.GetCustomAttribute(type, typeof(T)) is T attribute)
            {
                name = getName(attribute);
            }

            return !string.IsNullOrEmpty(name);
        }
    }
}
