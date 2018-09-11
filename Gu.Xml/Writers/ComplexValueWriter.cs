namespace Gu.Xml
{
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ComplexValueWriter
    {
        private static readonly ConcurrentDictionary<Type, ComplexValueWriter> Default = new ConcurrentDictionary<Type, ComplexValueWriter>();

        public ComplexValueWriter(IReadOnlyList<AttributeWriter> attributes, IReadOnlyList<ElementWriter> elements)
        {
            this.Attributes = attributes;
            this.Elements = elements;
        }

        public IReadOnlyList<AttributeWriter> Attributes { get; }

        public IReadOnlyList<ElementWriter> Elements { get; }

        public static ComplexValueWriter GetOrCreate<T>(T value)
        {
            return Default.GetOrAdd(value.GetType(), x => Create(x));
        }

        public static ComplexValueWriter Create(Type type)
        {
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.FlattenHierarchy);
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.FlattenHierarchy);
            if (!type.IsValueType &&
                type.BaseType != typeof(object))
            {
                Array.Sort(fields, BaseTypeCountComparer.Default);
                Array.Sort(properties, BaseTypeCountComparer.Default);
            }

            return new ComplexValueWriter(
                Attributes().ToArray(),
                Elements().ToArray());

            IEnumerable<AttributeWriter> Attributes()
            {
                foreach (var field in fields)
                {
                    if (AttributeWriter.TryCreate(field, out var writer))
                    {
                        yield return writer;
                    }
                }

                foreach (var property in properties)
                {
                    if (AttributeWriter.TryCreate(property, out var writer))
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