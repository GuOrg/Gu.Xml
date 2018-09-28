// ReSharper disable AssignNullToNotNullAttribute
namespace Gu.Xml.Tests.Internals
{
    using System.Linq.Expressions;
    using System.Reflection;
    using NUnit.Framework;

    public class ExpressionFactoryTests
    {
#pragma warning disable CS0649
        private readonly int value;
#pragma warning restore CS0649

        [Test]
        public void AssignReadonly()
        {
            var member = Expression.Field(Expression.Constant(this), typeof(ExpressionFactoryTests).GetField(nameof(this.value), BindingFlags.NonPublic | BindingFlags.Instance));
            var parameter = Expression.Parameter(typeof(int), "value");
            var assign = ExpressionFactory.AssignReadonly.Invoke(member, parameter);
            Assert.AreEqual(member, assign.Left);
            Assert.AreEqual(parameter, assign.Right);
        }
    }
}