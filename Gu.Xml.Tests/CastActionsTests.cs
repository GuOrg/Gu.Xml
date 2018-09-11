namespace Gu.Xml.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using NUnit.Framework;

    public class CastActionsTests
    {
        [Test]
        public void RegisterIntRawIntInvokeInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var actions = new CastActions<StringWriter>();
                actions.Register<int>((writer, value) => writer.Write(value));
                Assert.AreEqual(true, actions.TryGet(typeof(int), out var action));
                action.Raw<int>().Invoke(stringWriter, 1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void RegisterIntBoxedInvokeIFormattable()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var actions = new CastActions<StringWriter>();
                actions.Register<int>((writer, value) => writer.Write(value));
                Assert.AreEqual(true, actions.TryGet(typeof(int), out var action));
                action.Boxed().Invoke(stringWriter, (IFormattable)1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void RegisterIntBoxedInvokeObject()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var actions = new CastActions<StringWriter>();
                actions.Register<int>((writer, value) => writer.Write(value));
                Assert.AreEqual(true, actions.TryGet(typeof(int), out var action));
                action.Boxed().Invoke(stringWriter, (object)1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void RegisterIntRawNullableIntInvokeNullableInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var actions = new CastActions<StringWriter>();
                actions.Register<int>((writer, value) => writer.Write(value));
                Assert.AreEqual(true, actions.TryGet(typeof(int?), out var action));
                action.Raw<int?>().Invoke(stringWriter, (int?)1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void RegisterIntBoxedInvokeNullableInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var actions = new CastActions<StringWriter>();
                actions.Register<int>((writer, value) => writer.Write(value));
                Assert.AreEqual(true, actions.TryGet(typeof(int?), out var action));
                action.Boxed().Invoke(stringWriter, (int?)1);
                Assert.AreEqual("1", sb.ToString());
            }
        }
    }
}