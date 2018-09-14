﻿namespace Gu.Xml.Tests
{
    using System;
    using NUnit.Framework;

    public partial class XmlTests
    {
        public class ReferenceLoops
        {
            [Test]
            public void LinkedListRootOnly()
            {
                var actual = Xml.Serialize(new LinkedList());
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<LinkedList />";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void LinkedListRootOneNode()
            {
                var actual = Xml.Serialize(new LinkedList { Next = new LinkedList() });
                Dump.XmlAsCode(actual);
                var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                               "<LinkedList>" + Environment.NewLine +
                               "  <Next />" + Environment.NewLine +
                               "</LinkedList>";
                Assert.AreEqual(expected, actual);
            }

            public class LinkedList
            {
                public LinkedList Next { get; set; }
            }
        }
    }
}