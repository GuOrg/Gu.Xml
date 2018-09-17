namespace Gu.Xml.Tests.Internals
{
    using System;
    using System.Reflection;
    using NUnit.Framework;

    public class FieldInfoExtTest
    {
        [Test]
        public void CreateGenericGetter()
        {
            var getter = typeof(Struct).GetField(nameof(Struct.ReadOnlyField), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).CreateGetter<Struct, int>();
            Assert.AreEqual(3, getter(new Struct(3)));
        }

        [Test]
        public void CreateGetterStruct()
        {
            var getter = (Func<Struct, int>)typeof(Struct).GetField(nameof(Struct.ReadOnlyField), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).CreateGetter();
            Assert.AreEqual(3, getter(new Struct(3)));

            getter = (Func<Struct, int>)typeof(Struct).GetField(nameof(Struct.MutableField), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).CreateGetter();
            Assert.AreEqual(3, getter(new Struct(0) { MutableField = 3 }));
        }

        [Test]
        public void CreateGetterClass()
        {
            var getter = (Func<Class, int>)typeof(Class).GetField(nameof(Class.ReadOnlyField), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).CreateGetter();
            Assert.AreEqual(3, getter(new Class(3)));

            getter = (Func<Class, int>)typeof(Class).GetField(nameof(Class.MutableField), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).CreateGetter();
            Assert.AreEqual(3, getter(new Class(0) { MutableField = 3 }));
        }

        [Test]
        public void TryCreateSetterStructMutableField()
        {
            Assert.AreEqual(true, typeof(Struct).GetField(nameof(Struct.MutableField), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).TryCreateSetter(out var @delegate));
            var setter = (SetAction<Struct, int>)@delegate;
            var foo = new Struct(0);
            setter.Invoke(ref foo, 3);
            Assert.AreEqual(3, foo.MutableField);
        }

        [Test]
        public void TryCreateSetterClassMutableField()
        {
            Assert.AreEqual(true, typeof(Class).GetField(nameof(Class.MutableField), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).TryCreateSetter(out var @delegate));
            var setter = (SetAction<Class, int>)@delegate;
            var foo = new Class(3);
            setter.Invoke(ref foo, 3);
            Assert.AreEqual(3, foo.MutableField);
        }

        [Test]
        public void TryCreateSetterStructReadOnlyField()
        {
            Assert.AreEqual(false, typeof(Struct).GetField(nameof(Struct.ReadOnlyField), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).TryCreateSetter(out _));
        }

        [Test]
        public void TryCreateSetterClassReadOnlyField()
        {
            Assert.AreEqual(false, typeof(Class).GetField(nameof(Class.ReadOnlyField), BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).TryCreateSetter(out _));
        }

        private struct Struct
        {
            public readonly int ReadOnlyField;

            public int MutableField;

            public Struct(int @readonly)
            {
                this.ReadOnlyField = @readonly;
                this.MutableField = 0;
            }
        }

        private class Class
        {
            public readonly int ReadOnlyField;

            public int MutableField;

            public Class(int @readonly)
            {
                this.ReadOnlyField = @readonly;
                this.MutableField = 0;
            }
        }
    }
}