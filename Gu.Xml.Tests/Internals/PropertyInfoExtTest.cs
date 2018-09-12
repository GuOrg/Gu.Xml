namespace Gu.Xml.Tests.Internals
{
    using System;
    using NUnit.Framework;

    public class PropertyInfoExtTest
    {
        [Test]
        public void CreateGenericGetter()
        {
            var getter = typeof(string).GetProperty("Length").CreateGetter<string, int>();
            Assert.AreEqual(3, getter("abc"));
        }

        [Test]
        public void CreateGetter()
        {
            var getter = (Func<string, int>)typeof(string).GetProperty("Length").CreateGetter();
            Assert.AreEqual(3, getter("abc"));
        }

        [Test]
        public void CreateGetterStruct()
        {
            var getter = (Func<DateTime, int>)typeof(DateTime).GetProperty("Day").CreateGetter();
            Assert.AreEqual(3, getter(new DateTime(2018, 09, 3)));
        }
    }
}