namespace Gu.Xml.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using NUnit.Framework;

    public class CastActionTests
    {
        [Test]
        public void CreateIntRawInvokeInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var action = CastAction<TextWriter>.Create<int>((writer, value) => writer.Write(value));
                action.Raw<int>().Invoke(stringWriter, 1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void CreateIntBoxedInvokeIFormattable()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var action = CastAction<TextWriter>.Create<int>((writer, value) => writer.Write(value));
                action.Boxed().Invoke(stringWriter, (IFormattable)1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void CreateIntBoxedInvokeObject()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var action = CastAction<TextWriter>.Create<int>((writer, value) => writer.Write(value));
                action.Boxed().Invoke(stringWriter, (object)1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void CreateNullableIntRawInvokeInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var action = CastAction<TextWriter>.Create<int?>((writer, value) => writer.Write(value));
                action.Raw<int?>().Invoke(stringWriter, 1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void CreateNullableIntRawInvokeNullableInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var action = CastAction<TextWriter>.Create<int?>((writer, value) => writer.Write(value));
                action.Raw<int?>().Invoke(stringWriter, (int?)1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void CreateNullableIntBoxedInvokeInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var action = CastAction<TextWriter>.Create<int?>((writer, value) => writer.Write(value));
                action.Boxed().Invoke(stringWriter, 1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void CreateNullableIntBoxedInvokeNullableInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var action = CastAction<TextWriter>.Create<int?>((writer, value) => writer.Write(value));
                action.Boxed().Invoke(stringWriter, (int?)1);
                Assert.AreEqual("1", sb.ToString());
            }
        }
    }
}
