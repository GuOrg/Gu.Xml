namespace Gu.Xml
{
    using System;
    using System.IO;
    using System.Reflection;

    public abstract class AttributeWriter
    {
        public AttributeWriter(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public static AttributeWriter Create(string name, PropertyInfo property)
        {
            return (AttributeWriter)typeof(AttributeWriter)
                                     .GetMethod(nameof(CreateWriter), BindingFlags.Static | BindingFlags.NonPublic)
                                     .MakeGenericMethod(property.ReflectedType, property.PropertyType)
                                     .Invoke(null, new object[] { name, property });
        }

        public abstract void Write<T>(TextWriter writer, T source);

        private static AttributeWriter<TSource, TValue> CreateWriter<TSource, TValue>(string name, PropertyInfo property)
        {
            return new AttributeWriter<TSource, TValue>(name, (Func<TSource, TValue>)Delegate.CreateDelegate(typeof(Func<TSource, TValue>), property.GetMethod));
        }
    }
}