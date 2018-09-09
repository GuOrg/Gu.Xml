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
            if (!type.IsGenericType)
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
                else
                {
                    builder.Append(current.Name);
                }
            }
        }
    }
}
