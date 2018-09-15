namespace Gu.Xml
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal static class ExpressionFactory
    {
        internal static readonly Func<Type, string, ParameterExpression> RefParameter = CreateRefParameterFunc();

        internal static readonly Func<MemberExpression, ParameterExpression, BinaryExpression> AssignReadonly = CreateAssignReadonlyFunc();

        private static Func<Type, string, ParameterExpression> CreateRefParameterFunc()
        {
            var type = Expression.Parameter(typeof(Type), "type");
            var name = Expression.Parameter(typeof(string), "name");
            //// ReSharper disable once AssignNullToNotNullAttribute
            return Expression.Lambda<Func<Type, string, ParameterExpression>>(
                Expression.Call(
                    null,
                    typeof(ParameterExpression).GetMethod("Make", BindingFlags.Static | BindingFlags.NonPublic),
                    type,
                    name,
                    Expression.Constant(true)),
                type,
                name).Compile();
        }

        private static Func<MemberExpression, ParameterExpression, BinaryExpression> CreateAssignReadonlyFunc()
        {
            var left = Expression.Parameter(typeof(Expression), "left");
            var right = Expression.Parameter(typeof(Expression), "right");
            //// ReSharper disable once AssignNullToNotNullAttribute
            return Expression.Lambda<Func<MemberExpression, ParameterExpression, BinaryExpression>>(
                                 Expression.New(
                                     typeof(BinaryExpression).Assembly.GetType("System.Linq.Expressions.AssignBinaryExpression", throwOnError: true)
                                                             .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(Expression), typeof(Expression) }, null),
                                     left,
                                     right),
                                 left,
                                 right)
                             .Compile();
        }
    }
}