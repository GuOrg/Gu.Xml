namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
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
                return VerifyName(name);
            }

            if (!type.IsGenericType &&
                !type.HasElementType)
            {
                if (type.Name.StartsWith("<") &&
                    typeof(IEnumerable).IsAssignableFrom(type) &&
                    typeof(IEnumerator).IsAssignableFrom(type) &&
                    type.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>)).FirstOrDefault() is Type enumerableType)
                {
                    return Create(enumerableType);
                }

                return VerifyName(type.Name);
            }

            var builder = new StringBuilder();
            AppendRecursive(type);
            return VerifyName(builder.ToString());

            void AppendRecursive(Type current)
            {
                if (current.IsGenericType)
                {
                    if (type.IsAnonymous())
                    {
                        builder.Append("Anonymous");
                    }
                    else
                    {
                        builder.Append(current.Name, 0, current.Name.IndexOf('`'));
                    }

                    builder.Append("Of");
                    foreach (var argument in current.GenericTypeArguments)
                    {
                        AppendRecursive(argument);
                    }
                }
                else if (current.IsArray)
                {
                    if (current.HasElementType)
                    {
                        builder.Append("ArrayOf");
                        AppendRecursive(current.GetElementType());
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
            if (type.TryGetCustomAttribute(out T attribute))
            {
                name = getName(attribute);
            }

            return !string.IsNullOrEmpty(name);
        }

        private static string VerifyName(string name)
        {
            if (name.Contains("<") || name.Contains(">"))
            {
                throw new InvalidOperationException($"The name {name} is not supported.");
            }

            return name;
        }
    }
}
