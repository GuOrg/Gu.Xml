// ReSharper disable All temp disabling to not get here all the time.
namespace Gu.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml;

    /// <summary>
    /// Methods for deserializing from XML.
    /// </summary>
    public static partial class Xml
    {
        private static readonly ReadMaps Maps = new ReadMaps()
            .RegisterSimple(x => bool.Parse(x))
            .RegisterSimple(x => byte.Parse(x, NumberFormatInfo.InvariantInfo));

        /// <summary>
        /// Deserialize <paramref name="xml"/> to an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="xml">The XML.</param>
        /// <returns>The instance.</returns>
        public static T Deserialize<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                throw new ArgumentException($"'{nameof(xml)}' cannot be null or empty", nameof(xml));
            }

            using var input = new StringReader(xml);
            using var reader = XmlReader.Create(input);
            return Deserialize<T>(reader);
        }

        /// <summary>
        /// Deserialize <paramref name="stream"/> to an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="stream">The XML.</param>
        /// <returns>The instance.</returns>
        public static T Deserialize<T>(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            using var reader = XmlReader.Create(stream);
            return Deserialize<T>(reader);
        }

        /// <summary>
        /// Deserialize <paramref name="reader"/> to an instance of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <param name="reader">The <see cref="XmlReader"/>.</param>
        /// <returns>The instance.</returns>
        public static T Deserialize<T>(XmlReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            if (Maps.TryGet<T>(out var map))
            {
                return map.ReadElement<T>(reader);
            }

            // first, attempt to find a constructor
            // so far we'll assume there's a single constructor
            // it may be a no parameter one, in which case we'll resort to assigning values to properties
            var constructor = typeof(T).GetConstructors().SingleOrDefault();
            var parameters = constructor?.GetParameters();
            if (parameters is null || parameters.Length == 0)
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
                            .GetMethod(nameof(Enumerable.Empty), BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                            .MakeGenericMethod(new Type[] { parameter.ParameterType.GetGenericArguments().Single() })
                            .Invoke(null, null);
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
