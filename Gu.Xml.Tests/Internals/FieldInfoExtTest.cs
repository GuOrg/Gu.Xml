namespace Gu.Xml.Tests.Internals
{
    using System;
    using NUnit.Framework;

    public class FieldInfoExtTest
    {
        [Test]
        public void CreateGenericGetter()
        {
            var getter = typeof(Struct).GetField(nameof(Struct.Value)).CreateGetter<Struct, int>();
            Assert.AreEqual(3, getter(new Struct(3)));
        }

        [Test]
        public void CreateGetter()
        {
            var getter = (Func<Struct, int>)typeof(Struct).GetField(nameof(Struct.Value)).CreateGetter();
            Assert.AreEqual(3, getter(new Struct(3)));
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