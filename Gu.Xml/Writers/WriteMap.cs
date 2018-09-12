namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    internal class WriteMap
    {
        internal WriteMap(IReadOnlyList<CastAction<XmlWriter>> attributes, IReadOnlyList<ElementWriter> elements)
        {
            this.Attributes = attributes;
            this.Elements = elements;
        }

        public IReadOnlyList<CastAction<XmlWriter>> Attributes { get; }

        public IReadOnlyList<ElementWriter> Elements { get; }

        public static WriteMap Create(Type type)
        {
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.FlattenHierarchy);
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            if (!type.IsValueType &&
                type.BaseType != typeof(object))
            {
                Array.Sort(fields, BaseTypeCountComparer.Default);
                Array.Sort(properties, BaseTypeCountComparer.Default);
            }

            return new WriteMap(
                Attributes().ToArray(),
                Elements().ToArray());

            IEnumerable<CastAction<XmlWriter>> Attributes()
            {
                foreach (var field in fields)
                {
                    if (TryCreateAttributeWriter(field, out var writer))
                    {
                        yield return writer;
                    }
                }

                foreach (var property in properties)
                {
                    if (TryCreateAttributeWriter(property, out var writer))
                    {
                        yield return writer;
                    }
                }
            }

            IEnumerable<ElementWriter> Elements()
            {
                foreach (var field in fields)
                {
                    if (ElementWriter.TryCreate(field, out var writer))
                    {
                        yield return writer;
                    }
                }

                foreach (var property in properties)
                {
                    if (ElementWriter.TryCreate(property, out var writer))
                    {
                        yield return writer;
                    }
                }
            }
        }

        /// <summary>
        /// Check if <paramref name="property"/> has any attributes like [XmlAttribute].
        /// </summary>
        /// <param name="property">The <see cref="PropertyInfo"/>.</param>
        /// <param name="writer">The <see cref="AttributeWriter"/>.</param>
        /// <returns>True if an <see cref="AttributeWriter"/> was created.</returns>
        private static bool TryCreateAttributeWriter(PropertyInfo property, out CastAction<XmlWriter> writer)
        {
            if (TryGetAttributeName(property, out var name))
            {
                // ReSharper disable once PossibleNullReferenceException
                writer = (CastAction<XmlWriter>)typeof(WriteMap)
                                          .GetMethod(nameof(CreateAttributeWriter), BindingFlags.Static | BindingFlags.NonPublic)
                                          .MakeGenericMethod(property.ReflectedType, property.PropertyType)
                                          .Invoke(null, new object[] { name, property });
                return true;
            }

            writer = null;
            return false;
        }

        private static bool TryCreateAttributeWriter(FieldInfo field, out CastAction<XmlWriter> writer)
        {
            if (TryGetAttributeName(field, out var name))
            {
                // ReSharper disable once PossibleNullReferenceException
                writer = (CastAction<XmlWriter>)typeof(WriteMap)
                                     .GetMethod(nameof(CreateAttributeWriter), BindingFlags.Static | BindingFlags.NonPublic)
                                     .MakeGenericMethod(field.ReflectedType, field.FieldType)
                                     .Invoke(null, new object[] { name, field });
                return true;
            }

            writer = null;
            return false;
        }

        private static CastAction<XmlWriter> CreateAttributeWriter<TSource, TValue>(string name, MemberInfo member)
        {
            // Caching via closure here.
            var cachedGetter = CreateGetter();
            Action<TextWriter, TValue> cachedWriter = null;

            return CastAction<XmlWriter>.Create<TSource>((writer, source) =>
            {
                if (cachedGetter(source) is TValue value)
                {
                    if (cachedWriter != null ||
                        writer.TryGetSimple(value, out cachedWriter))
                    {
                        var textWriter = writer.TextWriter;
                        textWriter.WriteMany(" ", name, "=\"");
                        cachedWriter(textWriter, value);
                        textWriter.Write("\"");
                        if (!typeof(TValue).IsSealed)
                        {
                            // We can't cache it as we are not sure it is the same type.
                            cachedWriter = null;
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException($"Could not find an Action<TextWriter, {nameof(TValue)}> for {value} of type {value.GetType()}");
                    }
                }
            });

            Func<TSource, TValue> CreateGetter()
            {
                switch (member)
                {
                    case PropertyInfo property:
                        return property.CreateGetter<TSource, TValue>();
                    case FieldInfo field:
                        return field.CreateGetter<TSource, TValue>();
                    default:
                        throw new InvalidOperationException($"Not handling {member}. Bug in Gu.Xml.");
                }
            }
        }

        private static bool TryGetAttributeName(MemberInfo member, out string name)
        {
            name = null;
            if (member.TryGetCustomAttribute(out System.Xml.Serialization.XmlAttributeAttribute xmlAttribute))
            {
                name = xmlAttribute.AttributeName ?? string.Empty;
            }
            else if (member.TryGetCustomAttribute(out System.Xml.Serialization.SoapAttributeAttribute soapAttribute))
            {
                name = soapAttribute.AttributeName ?? string.Empty;
            }

            if (name == null)
            {
                return false;
            }

            if (name == string.Empty)
            {
                name = member.Name;
            }

            return true;
        }

        private sealed class BaseTypeCountComparer : IComparer<MemberInfo>, IComparer
        {
            public static readonly BaseTypeCountComparer Default = new BaseTypeCountComparer();

            public int Compare(MemberInfo x, MemberInfo y)
            {
                var xType = x.DeclaringType;
                var yType = y.DeclaringType;
                if (xType == yType)
                {
                    return 0;
                }

                return Count(xType).CompareTo(Count(yType));
            }

            int IComparer.Compare(object x, object y)
            {
                if (x is MemberInfo xp &&
                    y is MemberInfo yp)
                {
                    return this.Compare(xp, yp);
                }

                return 0;
            }

            private static int Count(Type type)
            {
                var count = 0;
                while (type.BaseType != null)
                {
                    count++;
                    type = type.BaseType;
                }

                return count;
            }
        }
    }
}