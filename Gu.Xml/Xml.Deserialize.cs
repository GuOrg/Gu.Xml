namespace Gu.Xml
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    public static partial class Xml
    {
        public static T Deserialize<T>(Stream stream, XSerializer serializer = null)
        {
            return Deserialize<T>(XmlReader.Create(stream), serializer);
        }

        private static T Deserialize<T>(XmlReader reader, XSerializer serializer = null)
        {
            serializer = serializer ?? XSerializer.Default;

            // first, attempt to find a constructor
            // so far we'll assume there's a single constructor
            // it may be a no parameter one, in which case we'll resort to assigning values to properties
            var constructor = typeof(T).GetConstructors().SingleOrDefault();
            var parameters = constructor?.GetParameters();
            if (parameters == null || parameters.Length == 0)
            {
                // default constructor
                var root = Activator.CreateInstance<T>();
            }
            else
            {
                var arguments = new Dictionary<string, KeyValuePair<int, object>>();
                foreach (var parameter in parameters)
                {
                    bool isOptional;
                    object replacementValue;
                    if (parameter.HasDefaultValue)
                    {
                        isOptional = true;
                        replacementValue = parameter.DefaultValue;
                    }
                    else if (parameter.ParameterType.IsConstructedGenericType &&
                             parameter.ParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                    {
                        isOptional = true;
                        replacementValue = typeof(Enumerable)
                            .GetMethod(nameof(Enumerable.Empty), BindingFlags.Static | BindingFlags.Public)
                            .MakeGenericMethod(new Type[] {parameter.ParameterType.GetGenericArguments().Single()})
                            .Invoke(null, Array.Empty<object>());
                    }
                    else if (Nullable.GetUnderlyingType(parameter.ParameterType) != null)
                    {
                        isOptional = true;
                        replacementValue = null;
                    }
                    else
                    {
                        isOptional = false;
                        replacementValue = null;
                    }
                }
            }

            throw new NotImplementedException();
        }
    }
}