// ReSharper disable RedundantCast
namespace Gu.Xml.Tests.Internals
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
        public void CreateIntInvokeDoubleThrows()
        {
            var castAction = CastAction<TextWriter>.Create<int>((writer, value) => writer.Write(value));
            var exception = Assert.Throws<InvalidOperationException>(() => castAction.Invoke(null, 2.0));
            Assert.AreEqual("Calling invoke on a cast action with wrong argument types. Expected <System.IO.TextWriter, System.Int32> was <System.IO.TextWriter, System.Double>.\r\nBug in Gu.Xml.", exception.Message);
        }

        [Test]
        public void CreateIntInvokeInt()
        {
            var sb = new StringBuilder();
            using var stringWriter = new StringWriter(sb);
            var castAction = CastAction<TextWriter>.Create<int>((writer, value) => writer.Write(value));

            castAction.Invoke(stringWriter, 1);
            Assert.AreEqual("1", sb.ToString());

            Assert.AreEqual(true, castAction.TryGet<int>(out var action));
            action.Invoke(stringWriter, 2);
            Assert.AreEqual("12", sb.ToString());
        }

        [Test]
        public void CreateIntInvokeIFormattable()
        {
            var sb = new StringBuilder();
            using var stringWriter = new StringWriter(sb);
            var castAction = CastAction<TextWriter>.Create<int>((writer, value) => writer.Write(value));

            castAction.Invoke(stringWriter, (IFormattable)1);
            Assert.AreEqual("1", sb.ToString());

            Assert.AreEqual(true, castAction.TryGet<object>(out var action));
            action.Invoke(stringWriter, (IFormattable)2);
            Assert.AreEqual("12", sb.ToString());
        }

        [Test]
        public void CreateIntInvokeObject()
        {
            var sb = new StringBuilder();
            using var stringWriter = new StringWriter(sb);
            var castAction = CastAction<TextWriter>.Create<int>((writer, value) => writer.Write(value));

            castAction.Invoke(stringWriter, (object)1);
            Assert.AreEqual("1", sb.ToString());

            Assert.AreEqual(true, castAction.TryGet<object>(out var action));
            action.Invoke(stringWriter, (object)2);
            Assert.AreEqual("12", sb.ToString());
        }

        [Test]
        public void CreateNullableIntInvokeInt()
        {
            var sb = new StringBuilder();
            using var stringWriter = new StringWriter(sb);
            var castAction = CastAction<TextWriter>.Create<int?>((writer, value) => writer.Write(value));

            ////castAction.Invoke(stringWriter, 1);
            ////Assert.AreEqual("1", sb.ToString());

            Assert.AreEqual(true, castAction.TryGet<int?>(out var action));
            action.Invoke(stringWriter, 2);
            Assert.AreEqual("2", sb.ToString());
        }

        [Test]
        public void CreateNullableIntInvokeNullableInt()
        {
            var sb = new StringBuilder();
            using var stringWriter = new StringWriter(sb);
            var castAction = CastAction<TextWriter>.Create<int?>((writer, value) => writer.Write(value));

            castAction.Invoke(stringWriter, (int?)1);
            Assert.AreEqual("1", sb.ToString());

            Assert.AreEqual(true, castAction.TryGet<int?>(out var action));
            action.Invoke(stringWriter, (int?)2);
            Assert.AreEqual("12", sb.ToString());
        }

        [Test]
        public void CreateNullableIntInvokeBoxedInt()
        {
            var sb = new StringBuilder();
            using var stringWriter = new StringWriter(sb);
            var castAction = CastAction<TextWriter>.Create<int?>((writer, value) => writer.Write(value));

            castAction.Invoke(stringWriter, (object)1);
            Assert.AreEqual("1", sb.ToString());

            Assert.AreEqual(true, castAction.TryGet<object>(out var action));
            action.Invoke(stringWriter, (object)2);
            Assert.AreEqual("12", sb.ToString());
        }

        [Test]
        public void CreateNullableIntBoxedInvokeNullableInt()
        {
            var sb = new StringBuilder();
            using var stringWriter = new StringWriter(sb);
            var castAction = CastAction<TextWriter>.Create<int?>((writer, value) => writer.Write(value));

            castAction.Invoke(stringWriter, (int?)1);
            Assert.AreEqual("1", sb.ToString());

            Assert.AreEqual(true, castAction.TryGet<object>(out var action));
            action.Invoke(stringWriter, (int?)2);
            Assert.AreEqual("12", sb.ToString());
        }
    }
}
