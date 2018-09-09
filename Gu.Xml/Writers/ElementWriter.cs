namespace Gu.Xml
{
    using System;
    using System.Reflection;

    public abstract class ElementWriter
    {
        protected ElementWriter(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public static ElementWriter Create(string name, PropertyInfo property)
        {
            return (ElementWriter)typeof(ElementWriter)
                                   .GetMethod(nameof(CreateWriter), BindingFlags.Static | BindingFlags.NonPublic)
                                   .MakeGenericMethod(property.ReflectedType, property.PropertyType)
                                   .Invoke(null, new object[] { name, property });
        }

        public abstract void Write<T>(XmlWriter writer, T source);

        private static ElementWriter<TSource, TValue> CreateWriter<TSource, TValue>(string name, PropertyInfo property)
        {
            return new ElementWriter<TSource, TValue>(name, (Func<TSource, TValue>)Delegate.CreateDelegate(typeof(Func<TSource, TValue>), property.GetMethod));
        }
    }
}