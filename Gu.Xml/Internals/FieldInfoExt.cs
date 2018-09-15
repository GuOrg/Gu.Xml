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
            // ReSharper disable once AssignNullToNotNullAttribute don't think it can be null here.
            var parameter = Expression.Parameter(field.ReflectedType, "source");
            return Expression.Lambda(
                                 typeof(Func<,>).MakeGenericType(field.ReflectedType, field.FieldType),
                                 Expression.Field(parameter, field),
                                 parameter)
                             .Compile();
        }

        internal static bool TryCreateSetter(this FieldInfo field, out Delegate setter)
        {
            // ReSharper disable once PossibleNullReferenceException
            var sourceParameter = ExpressionFactory.RefParameter(field.ReflectedType, "source");
            var valueParameter = Expression.Parameter(field.FieldType, "value");
            if (!field.IsInitOnly)
            {
                setter = Expression.Lambda(
                                       typeof(SetAction<,>).MakeGenericType(field.ReflectedType, field.FieldType),
                                       Expression.Assign(Expression.Field(sourceParameter, field), valueParameter),
                                       sourceParameter,
                                       valueParameter)
                                   .Compile();
                return true;
            }

            setter = null;
            return false;
        }
    }
}