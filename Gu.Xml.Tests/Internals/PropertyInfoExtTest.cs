// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
namespace Gu.Xml.Tests.Internals
{
    using System;
    using NUnit.Framework;

    public class PropertyInfoExtTest
    {
        [Test]
        public void CreateGenericGetter()
        {
            var getter = typeof(Struct).GetProperty(nameof(Struct.ReadOnlyProperty)).CreateGetter<Struct, int>();
            Assert.AreEqual(3, getter(new Struct(3)));
        }

        [Test]
        public void CreateGetterStruct()
        {
            var getter = (Func<Struct, int>)typeof(Struct).GetProperty(nameof(Struct.ReadOnlyProperty)).CreateGetter();
            Assert.AreEqual(3, getter(new Struct(3)));

            getter = (Func<Struct, int>)typeof(Struct).GetProperty(nameof(Struct.MutableProperty)).CreateGetter();
            Assert.AreEqual(3, getter(new Struct(0) { MutableProperty = 3 }));
        }

        [Test]
        public void CreateGetterClass()
        {
            var getter = (Func<Class, int>)typeof(Class).GetProperty(nameof(Class.ReadOnlyProperty)).CreateGetter();
            Assert.AreEqual(3, getter(new Class(3)));

            getter = (Func<Class, int>)typeof(Class).GetProperty(nameof(Class.MutableProperty)).CreateGetter();
            Assert.AreEqual(3, getter(new Class(0) { MutableProperty = 3 }));
        }

        [Test]
        public void TryCreateSetterStructMutableProperty()
        {
            Assert.AreEqual(true, typeof(Struct).GetProperty(nameof(Struct.MutableProperty)).TryCreateSetter(out var @delegate));
            var setter = (SetAction<Struct, int>)@delegate;
            var foo = new Struct(0);
            setter.Invoke(ref foo, 3);
            Assert.AreEqual(3, foo.MutableProperty);
        }

        [Test]
        public void TryCreateSetterClassMutableProperty()
        {
            Assert.AreEqual(true, typeof(Class).GetProperty(nameof(Class.MutableProperty)).TryCreateSetter(out var @delegate));
            var setter = (SetAction<Class, int>)@delegate;
            var foo = new Class(3);
            setter.Invoke(ref foo, 3);
            Assert.AreEqual(3, foo.MutableProperty);
        }

        [Test]
        public void TryCreateSetterStructReadOnlyProperty()
        {
            Assert.AreEqual(false, typeof(Struct).GetProperty(nameof(Struct.ReadOnlyProperty)).TryCreateSetter(out _));
        }

        [Test]
        public void TryCreateSetterClassReadOnlyProperty()
        {
            Assert.AreEqual(false, typeof(Class).GetProperty(nameof(Class.ReadOnlyProperty)).TryCreateSetter(out _));
        }

        private struct Struct
        {
            // ReSharper disable once UnusedParameter.Local
            public Struct(int @readonly)
            {
                this.ReadOnlyProperty = @readonly;
                this.MutableProperty = 0;
            }

            public int ReadOnlyProperty { get; }

            public int MutableProperty { get; set; }
        }

        private class Class
        {
            // ReSharper disable once UnusedParameter.Local
            public Class(int @readonly)
            {
                this.ReadOnlyProperty = @readonly;
            }

            public int ReadOnlyProperty { get; }

            public int MutableProperty { get; set; }
        }
    }
}