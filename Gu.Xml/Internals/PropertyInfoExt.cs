namespace Gu.Xml
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class PropertyInfoExt
    {
        internal static bool IsGetOnly(this PropertyInfo property)
        {
            return property.GetMethod?.GetCustomAttribute<CompilerGeneratedAttribute>() != null;
        }

        internal static Func<TSource, TValue> CreateGetter<TSource, TValue>(this PropertyInfo property)
        {
            var parameter = Expression.Parameter(typeof(TSource), "source");
            return Expression.Lambda<Func<TSource, TValue>>(Expression.Property(parameter, property), parameter)
                             .Compile();
        }

        internal static Delegate CreateGetter(this PropertyInfo property)
        {
            // ReSharper disable once AssignNullToNotNullAttribute don't think it can be null here.
            var parameter = Expression.Parameter(property.ReflectedType, "source");
            return Expression.Lambda(
                                 typeof(Func<,>).MakeGenericType(property.ReflectedType, property.PropertyType),
                                 Expression.Property(parameter, property),
                                 parameter)
                             .Compile();
        }

        internal static bool TryCreateSetter(this PropertyInfo property, out Delegate setter)
        {
            // ReSharper disable once PossibleNullReferenceException
            var sourceParameter = Expression.Parameter(property.ReflectedType.MakeByRefType(), "source");
            var valueParameter = Expression.Parameter(property.PropertyType, "value");
            if (property.SetMethod != null)
            {
                setter = Expression.Lambda(
                                     typeof(SetAction<,>).MakeGenericType(property.ReflectedType, property.PropertyType),
                                     ExpressionFactory.AssignReadonly(Expression.Property(sourceParameter, property), valueParameter),
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
