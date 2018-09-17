﻿// ReSharper disable All temp disabling to not get here all the time.
namespace Gu.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    public static partial class Xml
    {
        private static readonly ReadMaps Maps = new ReadMaps()
            .RegisterSimple(x => bool.Parse(x))
            .RegisterSimple(x => byte.Parse(x, NumberFormatInfo.InvariantInfo));

        public static T Deserialize<T>(string xml)
        {
            using (var reader = XmlReader.Create(new StringReader(xml)))
            {
                return Deserialize<T>(reader);
            }
        }

        public static T Deserialize<T>(Stream stream)
        {
            return Deserialize<T>(XmlReader.Create(stream));
        }

        public static T Deserialize<T>(XmlReader reader)
        {
            if (Maps.TryGet<T>(out var map))
            {
                return map.ReadElement<T>(reader);
            }

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
                            .GetMethod(nameof(Enumerable.Empty), BindingFlags.Public | BindingFlags.Static)
                            .MakeGenericMethod(new Type[] { parameter.ParameterType.GetGenericArguments().Single() })
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