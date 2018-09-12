namespace Gu.Xml
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class FieldInfoExt
    {
        internal static Func<TSource, TValue> CreateGetter<TSource, TValue>(this FieldInfo field)
        {
            var parameter = Expression.Parameter(typeof(TSource), "source");
            return Expression.Lambda<Func<TSource, TValue>>(Expression.Field(parameter, field), parameter)
                             .Compile();
        }

        internal static Delegate CreateGetter(this FieldInfo field)
        {
            var parameter = Expression.Parameter(field.ReflectedType, "source");
            return Expression.Lambda(
                                 typeof(Func<,>).MakeGenericType(field.ReflectedType, field.FieldType),
                                 Expression.Field(parameter, field),
                                 parameter)
                             .Compile();
        }
    }
}