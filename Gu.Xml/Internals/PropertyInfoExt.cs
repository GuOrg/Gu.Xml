namespace Gu.Xml
{
    using System;
    using System.Reflection;

    internal static class PropertyInfoExt
    {
        internal static Func<TSource, TValue> CreateGetter<TSource, TValue>(this PropertyInfo property)
        {
            if (property.DeclaringType.IsValueType)
            {
                return new StructGetter<TSource, TValue>(property).GetValue;
            }

            return (Func<TSource, TValue>)Delegate.CreateDelegate(typeof(Func<TSource, TValue>), property.GetMethod);
        }

        private class StructGetter<TSource, TValue>
        {
            private readonly GetterDelegate getter;

            public StructGetter(PropertyInfo property)
            {
                this.getter = (GetterDelegate)Delegate.CreateDelegate(typeof(GetterDelegate), property.GetMethod, throwOnBindFailure: true);
            }

            private delegate TValue GetterDelegate(ref TSource source);

            public TValue GetValue(TSource source)
            {
                return this.getter(ref source);
            }
        }
    }
}
