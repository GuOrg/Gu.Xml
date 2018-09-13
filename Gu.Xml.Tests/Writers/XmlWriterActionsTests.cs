﻿namespace Gu.Xml.Tests.Writers
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using NUnit.Framework;

    public class XmlWriterActionsTests
    {
        [Test]
        public void RegisterSimpleNullableInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var actions = new WriteMaps().RegisterSimple<int?>((writer, value) => writer.Write(value?.ToString(NumberFormatInfo.InvariantInfo)));
                int? nullableValue = 1;

                Assert.AreEqual(true, actions.TryGetSimple(nullableValue, out var action));
                action(stringWriter, nullableValue);
                Assert.AreEqual("1", sb.ToString());

                var intValue = 2;
                Assert.AreEqual(true, actions.TryGetSimple(intValue, out var intAction));
                intAction(stringWriter, intValue);
                Assert.AreEqual("12", sb.ToString());
            }
        }

        [Test]
        public void RegisterSimpleInt()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                var actions = new WriteMaps().RegisterSimple<int>((writer, value) => writer.Write(value.ToString(NumberFormatInfo.InvariantInfo)));

                var intValue = 1;
                Assert.AreEqual(true, actions.TryGetSimple(intValue, out var intAction));
                intAction(stringWriter, intValue);
                Assert.AreEqual("1", sb.ToString());

                int? nullableValue = 2;
                Assert.AreEqual(true, actions.TryGetSimple(nullableValue, out var nullableAction));
                nullableAction(stringWriter, nullableValue);
                Assert.AreEqual("12", sb.ToString());
            }
        }
    }
}