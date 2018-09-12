namespace Gu.Xml
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class PropertyInfoExt
    {
        internal static Func<TSource, TValue> CreateGetter<TSource, TValue>(this PropertyInfo property)
        {
            var parameter = Expression.Parameter(typeof(TSource), "source");
            return Expression.Lambda<Func<TSource, TValue>>(Expression.Property(parameter, property), parameter)
                             .Compile();
        }

        internal static Delegate CreateGetter(this PropertyInfo property)
        {
            var parameter = Expression.Parameter(property.ReflectedType, "source");
            return Expression.Lambda(
                                 typeof(Func<,>).MakeGenericType(property.ReflectedType, property.PropertyType),
                                 Expression.Property(parameter, property),
                                 parameter)
                             .Compile();
        }
    }
}
