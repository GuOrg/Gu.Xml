namespace Gu.Xml
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class FieldInfoExt
    {
        internal static Func<TSource, TValue> CreateGetter<TSource, TValue>(this FieldInfo property)
        {
            var parameter = Expression.Parameter(typeof(TSource), "source");
            return Expression.Lambda<Func<TSource, TValue>>(Expression.Field(parameter, property), parameter)
                             .Compile();
        }
    }
}