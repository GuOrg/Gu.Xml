namespace Gu.Xml.Tests.Internals
{
    using System;
    using NUnit.Framework;

    public class FieldInfoExtTest
    {
        [Test]
        public void CreateGenericGetter()
        {
            var getter = typeof(Struct).GetProperty(nameof(Struct.Value)).CreateGetter<Struct, int>();
            Assert.AreEqual(3, getter(new Struct(2)));
        }

        [Test]
        public void CreateGetter()
        {
            var getter = (Func<Struct, int>)typeof(string).GetProperty("Length").CreateGetter();
            Assert.AreEqual(3, getter("abc"));
        }

        [Test]
        public void CreateGetterStruct()
        {
            var getter = (Func<DateTime, int>)typeof(DateTime).GetProperty("Day").CreateGetter();
            Assert.AreEqual(3, getter(new DateTime(2018, 09, 3)));
        }

        public struct Struct
        {
            public readonly int Value;

            public Struct(int value)
            {
                this.Value = value;
            }
        }
    }
}