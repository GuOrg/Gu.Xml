namespace Gu.Xml.Tests.Internals
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;

    public class TypeExtTests
    {
        [TestCase(typeof(string), typeof(IEnumerable<char>))]
        [TestCase(typeof(int[]), typeof(IEnumerable<int>))]
        [TestCase(typeof(int), null)]
        [TestCase(typeof(WithTwoIEnumerable), null)]
        public void IsEnumerable(Type type, Type expected)
        {
            Assert.AreEqual(expected != null, type.IsGenericEnumerable(out var result));
            Assert.AreEqual(expected, result);
        }

        private class WithTwoIEnumerable : IEnumerable<int>, IEnumerable<double>
        {
            IEnumerator<double> IEnumerable<double>.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator<int> IEnumerable<int>.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
    }
}