// ReSharper disable RedundantCast
namespace Gu.Xml.Tests
{
    using System;
    using System.IO;
    using System.Text;
    using NUnit.Framework;

    public class CastActionTests
    {
        [Test]
        public void CreateIntTryGetDoubleReturnsFalse()
        {
            var castAction = CastAction<TextWriter>.Create<int>((writer, value) => writer.Write(value));
            Assert.AreEqual(false, castAction.TryGet<double>(out _));
        }

        [Test]
        public void CreateIntRawInvokeInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var castAction = CastAction<TextWriter>.Create<int>((writer, value) => writer.Write(value));
                Assert.AreEqual(true, castAction.TryGet<int>(out var action));
                action.Invoke(stringWriter, 1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void CreateIntBoxedInvokeIFormattable()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var castAction = CastAction<TextWriter>.Create<int>((writer, value) => writer.Write(value));
                Assert.AreEqual(true, castAction.TryGet<object>(out var action));
                action.Invoke(stringWriter, (IFormattable)1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void CreateIntBoxedInvokeObject()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var castAction = CastAction<TextWriter>.Create<int>((writer, value) => writer.Write(value));
                Assert.AreEqual(true, castAction.TryGet<object>(out var action));
                action.Invoke(stringWriter, (object)1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void CreateNullableIntRawInvokeInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var castAction = CastAction<TextWriter>.Create<int?>((writer, value) => writer.Write(value));
                Assert.AreEqual(true, castAction.TryGet<int?>(out var action));
                action.Invoke(stringWriter, 1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void CreateNullableIntRawInvokeNullableInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var castAction = CastAction<TextWriter>.Create<int?>((writer, value) => writer.Write(value));
                Assert.AreEqual(true, castAction.TryGet<int?>(out var action));
                action.Invoke(stringWriter, (int?)1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void CreateNullableIntBoxedInvokeInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var castAction = CastAction<TextWriter>.Create<int?>((writer, value) => writer.Write(value));
                Assert.AreEqual(true, castAction.TryGet<object>(out var action));
                action.Invoke(stringWriter, 1);
                Assert.AreEqual("1", sb.ToString());
            }
        }

        [Test]
        public void CreateNullableIntBoxedInvokeNullableInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var castAction = CastAction<TextWriter>.Create<int?>((writer, value) => writer.Write(value));
                Assert.AreEqual(true, castAction.TryGet<object>(out var action));
                action.Invoke(stringWriter, (int?)1);
                Assert.AreEqual("1", sb.ToString());
            }
        }
    }
}
