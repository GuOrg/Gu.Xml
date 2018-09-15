// ReSharper disable AssignNullToNotNullAttribute
namespace Gu.Xml.Tests.Internals
{
    using System.Linq.Expressions;
    using System.Reflection;
    using NUnit.Framework;

    public class ExpressionFactoryTests
    {
        private readonly int value;

        [Test]
        public void RefParameter()
        {
            var parameter = ExpressionFactory.RefParameter.Invoke(typeof(string), "x");
            Assert.AreEqual(true, parameter.IsByRef);
            Assert.AreEqual(typeof(string), parameter.Type);
            Assert.AreEqual("x", parameter.Name);
        }

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