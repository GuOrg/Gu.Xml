namespace Gu.Xml.Tests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    public partial class XmlTests
    {
        [Test]
        public void SealedGraph()
        {
            var graph = new SealedGraphClass
            {
                Number1 = 1,
                Number2 = 2,
                Number3 = 3,
                Number4 = 4,
                Text1 = "1",
                Text2 = "2",
                Text3 = "3",
                Text4 = "4",
                KeyValuePair1 = new KeyValuePair<int, double>(0, 1),
                KeyValuePair2 = new KeyValuePair<int, double>(0, 2),
                Node1 = new SealedGraphClass.SealedNode
                {
                    Number1 = 11,
                    Number2 = 12,
                    Number3 = 13,
                    Number4 = 14,
                    Text1 = "11",
                    Text2 = "12",
                    Text3 = "13",
                    Text4 = "14",
                    KeyValuePair1 = new KeyValuePair<int, double>(1, 1),
                    KeyValuePair2 = new KeyValuePair<int, double>(1, 2),
                },
                Node2 = new SealedGraphClass.SealedNode
                {
                    Number1 = 21,
                    Number2 = 22,
                    Number3 = 23,
                    Number4 = 24,
                    Text1 = "21",
                    Text2 = "22",
                    Text3 = "23",
                    Text4 = "24",
                    KeyValuePair1 = new KeyValuePair<int, double>(2, 1),
                    KeyValuePair2 = new KeyValuePair<int, double>(2, 2),
                },
                Node3 = new SealedGraphClass.SealedNode
                {
                    Number1 = 31,
                    Number2 = 32,
                    Number3 = 33,
                    Number4 = 34,
                    Text1 = "31",
                    Text2 = "32",
                    Text3 = "33",
                    Text4 = "34",
                    KeyValuePair1 = new KeyValuePair<int, double>(3, 1),
                    KeyValuePair2 = new KeyValuePair<int, double>(3, 2),
                },
                Node4 = new SealedGraphClass.SealedNode
                {
                    Number1 = 41,
                    Number2 = 42,
                    Number3 = 43,
                    Number4 = 44,
                    Text1 = "41",
                    Text2 = "42",
                    Text3 = "43",
                    Text4 = "44",
                    KeyValuePair1 = new KeyValuePair<int, double>(4, 1),
                    KeyValuePair2 = new KeyValuePair<int, double>(4, 2),
                },
            };
            var actual = Xml.Serialize(graph);
            var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                           "<SealedGraphClass>" + Environment.NewLine +
                           "  <Number1>1</Number1>" + Environment.NewLine +
                           "  <Number2>2</Number2>" + Environment.NewLine +
                           "  <Text1>1</Text1>" + Environment.NewLine +
                           "  <Text2>2</Text2>" + Environment.NewLine +
                           "  <Number3>3</Number3>" + Environment.NewLine +
                           "  <Number4>4</Number4>" + Environment.NewLine +
                           "  <Text3>3</Text3>" + Environment.NewLine +
                           "  <Text4>4</Text4>" + Environment.NewLine +
                           "  <KeyValuePair1>" + Environment.NewLine +
                           "    <Key>0</Key>" + Environment.NewLine +
                           "    <Value>1</Value>" + Environment.NewLine +
                           "  </KeyValuePair1>" + Environment.NewLine +
                           "  <KeyValuePair2>" + Environment.NewLine +
                           "    <Key>0</Key>" + Environment.NewLine +
                           "    <Value>2</Value>" + Environment.NewLine +
                           "  </KeyValuePair2>" + Environment.NewLine +
                           "  <Node1>" + Environment.NewLine +
                           "    <Number1>11</Number1>" + Environment.NewLine +
                           "    <Number2>12</Number2>" + Environment.NewLine +
                           "    <Text1>11</Text1>" + Environment.NewLine +
                           "    <Text2>12</Text2>" + Environment.NewLine +
                           "    <Number3>13</Number3>" + Environment.NewLine +
                           "    <Number4>14</Number4>" + Environment.NewLine +
                           "    <Text3>13</Text3>" + Environment.NewLine +
                           "    <Text4>14</Text4>" + Environment.NewLine +
                           "    <KeyValuePair1>" + Environment.NewLine +
                           "      <Key>1</Key>" + Environment.NewLine +
                           "      <Value>1</Value>" + Environment.NewLine +
                           "    </KeyValuePair1>" + Environment.NewLine +
                           "    <KeyValuePair2>" + Environment.NewLine +
                           "      <Key>1</Key>" + Environment.NewLine +
                           "      <Value>2</Value>" + Environment.NewLine +
                           "    </KeyValuePair2>" + Environment.NewLine +
                           "  </Node1>" + Environment.NewLine +
                           "  <Node2>" + Environment.NewLine +
                           "    <Number1>21</Number1>" + Environment.NewLine +
                           "    <Number2>22</Number2>" + Environment.NewLine +
                           "    <Text1>21</Text1>" + Environment.NewLine +
                           "    <Text2>22</Text2>" + Environment.NewLine +
                           "    <Number3>23</Number3>" + Environment.NewLine +
                           "    <Number4>24</Number4>" + Environment.NewLine +
                           "    <Text3>23</Text3>" + Environment.NewLine +
                           "    <Text4>24</Text4>" + Environment.NewLine +
                           "    <KeyValuePair1>" + Environment.NewLine +
                           "      <Key>2</Key>" + Environment.NewLine +
                           "      <Value>1</Value>" + Environment.NewLine +
                           "    </KeyValuePair1>" + Environment.NewLine +
                           "    <KeyValuePair2>" + Environment.NewLine +
                           "      <Key>2</Key>" + Environment.NewLine +
                           "      <Value>2</Value>" + Environment.NewLine +
                           "    </KeyValuePair2>" + Environment.NewLine +
                           "  </Node2>" + Environment.NewLine +
                           "  <Node3>" + Environment.NewLine +
                           "    <Number1>31</Number1>" + Environment.NewLine +
                           "    <Number2>32</Number2>" + Environment.NewLine +
                           "    <Text1>31</Text1>" + Environment.NewLine +
                           "    <Text2>32</Text2>" + Environment.NewLine +
                           "    <Number3>33</Number3>" + Environment.NewLine +
                           "    <Number4>34</Number4>" + Environment.NewLine +
                           "    <Text3>33</Text3>" + Environment.NewLine +
                           "    <Text4>34</Text4>" + Environment.NewLine +
                           "    <KeyValuePair1>" + Environment.NewLine +
                           "      <Key>3</Key>" + Environment.NewLine +
                           "      <Value>1</Value>" + Environment.NewLine +
                           "    </KeyValuePair1>" + Environment.NewLine +
                           "    <KeyValuePair2>" + Environment.NewLine +
                           "      <Key>3</Key>" + Environment.NewLine +
                           "      <Value>2</Value>" + Environment.NewLine +
                           "    </KeyValuePair2>" + Environment.NewLine +
                           "  </Node3>" + Environment.NewLine +
                           "  <Node4>" + Environment.NewLine +
                           "    <Number1>41</Number1>" + Environment.NewLine +
                           "    <Number2>42</Number2>" + Environment.NewLine +
                           "    <Text1>41</Text1>" + Environment.NewLine +
                           "    <Text2>42</Text2>" + Environment.NewLine +
                           "    <Number3>43</Number3>" + Environment.NewLine +
                           "    <Number4>44</Number4>" + Environment.NewLine +
                           "    <Text3>43</Text3>" + Environment.NewLine +
                           "    <Text4>44</Text4>" + Environment.NewLine +
                           "    <KeyValuePair1>" + Environment.NewLine +
                           "      <Key>4</Key>" + Environment.NewLine +
                           "      <Value>1</Value>" + Environment.NewLine +
                           "    </KeyValuePair1>" + Environment.NewLine +
                           "    <KeyValuePair2>" + Environment.NewLine +
                           "      <Key>4</Key>" + Environment.NewLine +
                           "      <Value>2</Value>" + Environment.NewLine +
                           "    </KeyValuePair2>" + Environment.NewLine +
                           "  </Node4>" + Environment.NewLine +
                           "</SealedGraphClass>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Graph()
        {
            var graph = new GraphClass
            {
                Number1 = 1,
                Number2 = 2,
                Number3 = 3,
                Number4 = 4,
                Text1 = "1",
                Text2 = "2",
                Text3 = "3",
                Text4 = "4",
                KeyValuePair1 = new KeyValuePair<int, double>(0, 1),
                KeyValuePair2 = new KeyValuePair<int, double>(0, 2),
                Node1 = new GraphClass.Node
                {
                    Number1 = 11,
                    Number2 = 12,
                    Number3 = 13,
                    Number4 = 14,
                    Text1 = "11",
                    Text2 = "12",
                    Text3 = "13",
                    Text4 = "14",
                    KeyValuePair1 = new KeyValuePair<int, double>(1, 1),
                    KeyValuePair2 = new KeyValuePair<int, double>(1, 2),
                },
                Node2 = new GraphClass.Node
                {
                    Number1 = 21,
                    Number2 = 22,
                    Number3 = 23,
                    Number4 = 24,
                    Text1 = "21",
                    Text2 = "22",
                    Text3 = "23",
                    Text4 = "24",
                    KeyValuePair1 = new KeyValuePair<int, double>(2, 1),
                    KeyValuePair2 = new KeyValuePair<int, double>(2, 2),
                },
                Node3 = new GraphClass.Node
                {
                    Number1 = 31,
                    Number2 = 32,
                    Number3 = 33,
                    Number4 = 34,
                    Text1 = "31",
                    Text2 = "32",
                    Text3 = "33",
                    Text4 = "34",
                    KeyValuePair1 = new KeyValuePair<int, double>(3, 1),
                    KeyValuePair2 = new KeyValuePair<int, double>(3, 2),
                },
                Node4 = new GraphClass.Node
                {
                    Number1 = 41,
                    Number2 = 42,
                    Number3 = 43,
                    Number4 = 44,
                    Text1 = "41",
                    Text2 = "42",
                    Text3 = "43",
                    Text4 = "44",
                    KeyValuePair1 = new KeyValuePair<int, double>(4, 1),
                    KeyValuePair2 = new KeyValuePair<int, double>(4, 2),
                },
            };
            var actual = Xml.Serialize(graph);
            var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                           "<GraphClass>" + Environment.NewLine +
                           "  <Number1>1</Number1>" + Environment.NewLine +
                           "  <Number2>2</Number2>" + Environment.NewLine +
                           "  <Text1>1</Text1>" + Environment.NewLine +
                           "  <Text2>2</Text2>" + Environment.NewLine +
                           "  <Number3>3</Number3>" + Environment.NewLine +
                           "  <Number4>4</Number4>" + Environment.NewLine +
                           "  <Text3>3</Text3>" + Environment.NewLine +
                           "  <Text4>4</Text4>" + Environment.NewLine +
                           "  <KeyValuePair1>" + Environment.NewLine +
                           "    <Key>0</Key>" + Environment.NewLine +
                           "    <Value>1</Value>" + Environment.NewLine +
                           "  </KeyValuePair1>" + Environment.NewLine +
                           "  <KeyValuePair2>" + Environment.NewLine +
                           "    <Key>0</Key>" + Environment.NewLine +
                           "    <Value>2</Value>" + Environment.NewLine +
                           "  </KeyValuePair2>" + Environment.NewLine +
                           "  <Node1>" + Environment.NewLine +
                           "    <Number1>11</Number1>" + Environment.NewLine +
                           "    <Number2>12</Number2>" + Environment.NewLine +
                           "    <Text1>11</Text1>" + Environment.NewLine +
                           "    <Text2>12</Text2>" + Environment.NewLine +
                           "    <Number3>13</Number3>" + Environment.NewLine +
                           "    <Number4>14</Number4>" + Environment.NewLine +
                           "    <Text3>13</Text3>" + Environment.NewLine +
                           "    <Text4>14</Text4>" + Environment.NewLine +
                           "    <KeyValuePair1>" + Environment.NewLine +
                           "      <Key>1</Key>" + Environment.NewLine +
                           "      <Value>1</Value>" + Environment.NewLine +
                           "    </KeyValuePair1>" + Environment.NewLine +
                           "    <KeyValuePair2>" + Environment.NewLine +
                           "      <Key>1</Key>" + Environment.NewLine +
                           "      <Value>2</Value>" + Environment.NewLine +
                           "    </KeyValuePair2>" + Environment.NewLine +
                           "  </Node1>" + Environment.NewLine +
                           "  <Node2>" + Environment.NewLine +
                           "    <Number1>21</Number1>" + Environment.NewLine +
                           "    <Number2>22</Number2>" + Environment.NewLine +
                           "    <Text1>21</Text1>" + Environment.NewLine +
                           "    <Text2>22</Text2>" + Environment.NewLine +
                           "    <Number3>23</Number3>" + Environment.NewLine +
                           "    <Number4>24</Number4>" + Environment.NewLine +
                           "    <Text3>23</Text3>" + Environment.NewLine +
                           "    <Text4>24</Text4>" + Environment.NewLine +
                           "    <KeyValuePair1>" + Environment.NewLine +
                           "      <Key>2</Key>" + Environment.NewLine +
                           "      <Value>1</Value>" + Environment.NewLine +
                           "    </KeyValuePair1>" + Environment.NewLine +
                           "    <KeyValuePair2>" + Environment.NewLine +
                           "      <Key>2</Key>" + Environment.NewLine +
                           "      <Value>2</Value>" + Environment.NewLine +
                           "    </KeyValuePair2>" + Environment.NewLine +
                           "  </Node2>" + Environment.NewLine +
                           "  <Node3>" + Environment.NewLine +
                           "    <Number1>31</Number1>" + Environment.NewLine +
                           "    <Number2>32</Number2>" + Environment.NewLine +
                           "    <Text1>31</Text1>" + Environment.NewLine +
                           "    <Text2>32</Text2>" + Environment.NewLine +
                           "    <Number3>33</Number3>" + Environment.NewLine +
                           "    <Number4>34</Number4>" + Environment.NewLine +
                           "    <Text3>33</Text3>" + Environment.NewLine +
                           "    <Text4>34</Text4>" + Environment.NewLine +
                           "    <KeyValuePair1>" + Environment.NewLine +
                           "      <Key>3</Key>" + Environment.NewLine +
                           "      <Value>1</Value>" + Environment.NewLine +
                           "    </KeyValuePair1>" + Environment.NewLine +
                           "    <KeyValuePair2>" + Environment.NewLine +
                           "      <Key>3</Key>" + Environment.NewLine +
                           "      <Value>2</Value>" + Environment.NewLine +
                           "    </KeyValuePair2>" + Environment.NewLine +
                           "  </Node3>" + Environment.NewLine +
                           "  <Node4>" + Environment.NewLine +
                           "    <Number1>41</Number1>" + Environment.NewLine +
                           "    <Number2>42</Number2>" + Environment.NewLine +
                           "    <Text1>41</Text1>" + Environment.NewLine +
                           "    <Text2>42</Text2>" + Environment.NewLine +
                           "    <Number3>43</Number3>" + Environment.NewLine +
                           "    <Number4>44</Number4>" + Environment.NewLine +
                           "    <Text3>43</Text3>" + Environment.NewLine +
                           "    <Text4>44</Text4>" + Environment.NewLine +
                           "    <KeyValuePair1>" + Environment.NewLine +
                           "      <Key>4</Key>" + Environment.NewLine +
                           "      <Value>1</Value>" + Environment.NewLine +
                           "    </KeyValuePair1>" + Environment.NewLine +
                           "    <KeyValuePair2>" + Environment.NewLine +
                           "      <Key>4</Key>" + Environment.NewLine +
                           "      <Value>2</Value>" + Environment.NewLine +
                           "    </KeyValuePair2>" + Environment.NewLine +
                           "  </Node4>" + Environment.NewLine +
                           "</GraphClass>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BoxingVirtualGraph()
        {
            var graph = new BoxingVirtualGraphClass
            {
                Number1 = 1,
                Number2 = 2,
                Number3 = 3,
                Number4 = 4,
                Text1 = "1",
                Text2 = "2",
                Text3 = "3",
                Text4 = "4",
                KeyValuePair1 = new KeyValuePair<int, double>(0, 1),
                KeyValuePair2 = new KeyValuePair<int, double>(0, 2),
                Node1 = new BoxingVirtualGraphClass.Node
                {
                    Number1 = 11,
                    Number2 = 12,
                    Number3 = 13,
                    Number4 = 14,
                    Text1 = "11",
                    Text2 = "12",
                    Text3 = "13",
                    Text4 = "14",
                    KeyValuePair1 = new KeyValuePair<int, double>(1, 1),
                    KeyValuePair2 = new KeyValuePair<int, double>(1, 2),
                },
                Node2 = new BoxingVirtualGraphClass.Node
                {
                    Number1 = 21,
                    Number2 = 22,
                    Number3 = 23,
                    Number4 = 24,
                    Text1 = "21",
                    Text2 = "22",
                    Text3 = "23",
                    Text4 = "24",
                    KeyValuePair1 = new KeyValuePair<int, double>(2, 1),
                    KeyValuePair2 = new KeyValuePair<int, double>(2, 2),
                },
                Node3 = new BoxingVirtualGraphClass.Node
                {
                    Number1 = 31,
                    Number2 = 32,
                    Number3 = 33,
                    Number4 = 34,
                    Text1 = "31",
                    Text2 = "32",
                    Text3 = "33",
                    Text4 = "34",
                    KeyValuePair1 = new KeyValuePair<int, double>(3, 1),
                    KeyValuePair2 = new KeyValuePair<int, double>(3, 2),
                },
                Node4 = new BoxingVirtualGraphClass.Node
                {
                    Number1 = 41,
                    Number2 = 42,
                    Number3 = 43,
                    Number4 = 44,
                    Text1 = "41",
                    Text2 = "42",
                    Text3 = "43",
                    Text4 = "44",
                    KeyValuePair1 = new KeyValuePair<int, double>(4, 1),
                    KeyValuePair2 = new KeyValuePair<int, double>(4, 2),
                },
            };
            var actual = Xml.Serialize(graph);
            var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine +
                           "<BoxingVirtualGraphClass>" + Environment.NewLine +
                           "  <Number1>1</Number1>" + Environment.NewLine +
                           "  <Number2>2</Number2>" + Environment.NewLine +
                           "  <Text1>1</Text1>" + Environment.NewLine +
                           "  <Text2>2</Text2>" + Environment.NewLine +
                           "  <Number3>3</Number3>" + Environment.NewLine +
                           "  <Number4>4</Number4>" + Environment.NewLine +
                           "  <Text3>3</Text3>" + Environment.NewLine +
                           "  <Text4>4</Text4>" + Environment.NewLine +
                           "  <KeyValuePair1>" + Environment.NewLine +
                           "    <Key>0</Key>" + Environment.NewLine +
                           "    <Value>1</Value>" + Environment.NewLine +
                           "  </KeyValuePair1>" + Environment.NewLine +
                           "  <KeyValuePair2>" + Environment.NewLine +
                           "    <Key>0</Key>" + Environment.NewLine +
                           "    <Value>2</Value>" + Environment.NewLine +
                           "  </KeyValuePair2>" + Environment.NewLine +
                           "  <Node1>" + Environment.NewLine +
                           "    <Number1>11</Number1>" + Environment.NewLine +
                           "    <Number2>12</Number2>" + Environment.NewLine +
                           "    <Text1>11</Text1>" + Environment.NewLine +
                           "    <Text2>12</Text2>" + Environment.NewLine +
                           "    <Number3>13</Number3>" + Environment.NewLine +
                           "    <Number4>14</Number4>" + Environment.NewLine +
                           "    <Text3>13</Text3>" + Environment.NewLine +
                           "    <Text4>14</Text4>" + Environment.NewLine +
                           "    <KeyValuePair1>" + Environment.NewLine +
                           "      <Key>1</Key>" + Environment.NewLine +
                           "      <Value>1</Value>" + Environment.NewLine +
                           "    </KeyValuePair1>" + Environment.NewLine +
                           "    <KeyValuePair2>" + Environment.NewLine +
                           "      <Key>1</Key>" + Environment.NewLine +
                           "      <Value>2</Value>" + Environment.NewLine +
                           "    </KeyValuePair2>" + Environment.NewLine +
                           "  </Node1>" + Environment.NewLine +
                           "  <Node2>" + Environment.NewLine +
                           "    <Number1>21</Number1>" + Environment.NewLine +
                           "    <Number2>22</Number2>" + Environment.NewLine +
                           "    <Text1>21</Text1>" + Environment.NewLine +
                           "    <Text2>22</Text2>" + Environment.NewLine +
                           "    <Number3>23</Number3>" + Environment.NewLine +
                           "    <Number4>24</Number4>" + Environment.NewLine +
                           "    <Text3>23</Text3>" + Environment.NewLine +
                           "    <Text4>24</Text4>" + Environment.NewLine +
                           "    <KeyValuePair1>" + Environment.NewLine +
                           "      <Key>2</Key>" + Environment.NewLine +
                           "      <Value>1</Value>" + Environment.NewLine +
                           "    </KeyValuePair1>" + Environment.NewLine +
                           "    <KeyValuePair2>" + Environment.NewLine +
                           "      <Key>2</Key>" + Environment.NewLine +
                           "      <Value>2</Value>" + Environment.NewLine +
                           "    </KeyValuePair2>" + Environment.NewLine +
                           "  </Node2>" + Environment.NewLine +
                           "  <Node3>" + Environment.NewLine +
                           "    <Number1>31</Number1>" + Environment.NewLine +
                           "    <Number2>32</Number2>" + Environment.NewLine +
                           "    <Text1>31</Text1>" + Environment.NewLine +
                           "    <Text2>32</Text2>" + Environment.NewLine +
                           "    <Number3>33</Number3>" + Environment.NewLine +
                           "    <Number4>34</Number4>" + Environment.NewLine +
                           "    <Text3>33</Text3>" + Environment.NewLine +
                           "    <Text4>34</Text4>" + Environment.NewLine +
                           "    <KeyValuePair1>" + Environment.NewLine +
                           "      <Key>3</Key>" + Environment.NewLine +
                           "      <Value>1</Value>" + Environment.NewLine +
                           "    </KeyValuePair1>" + Environment.NewLine +
                           "    <KeyValuePair2>" + Environment.NewLine +
                           "      <Key>3</Key>" + Environment.NewLine +
                           "      <Value>2</Value>" + Environment.NewLine +
                           "    </KeyValuePair2>" + Environment.NewLine +
                           "  </Node3>" + Environment.NewLine +
                           "  <Node4>" + Environment.NewLine +
                           "    <Number1>41</Number1>" + Environment.NewLine +
                           "    <Number2>42</Number2>" + Environment.NewLine +
                           "    <Text1>41</Text1>" + Environment.NewLine +
                           "    <Text2>42</Text2>" + Environment.NewLine +
                           "    <Number3>43</Number3>" + Environment.NewLine +
                           "    <Number4>44</Number4>" + Environment.NewLine +
                           "    <Text3>43</Text3>" + Environment.NewLine +
                           "    <Text4>44</Text4>" + Environment.NewLine +
                           "    <KeyValuePair1>" + Environment.NewLine +
                           "      <Key>4</Key>" + Environment.NewLine +
                           "      <Value>1</Value>" + Environment.NewLine +
                           "    </KeyValuePair1>" + Environment.NewLine +
                           "    <KeyValuePair2>" + Environment.NewLine +
                           "      <Key>4</Key>" + Environment.NewLine +
                           "      <Value>2</Value>" + Environment.NewLine +
                           "    </KeyValuePair2>" + Environment.NewLine +
                           "  </Node4>" + Environment.NewLine +
                           "</BoxingVirtualGraphClass>";
            Assert.AreEqual(expected, actual);
        }

        public sealed class SealedGraphClass
        {
            public int Number1 { get; set; }

            public int Number2 { get; set; }

            public string Text1 { get; set; }

            public string Text2 { get; set; }

            public int Number3 { get; set; }

            public int Number4 { get; set; }

            public string Text3 { get; set; }

            public string Text4 { get; set; }

            public KeyValuePair<int, double> KeyValuePair1 { get; set; }

            public KeyValuePair<int, double> KeyValuePair2 { get; set; }

            public SealedNode Node1 { get; set; }

            public SealedNode Node2 { get; set; }

            public SealedNode Node3 { get; set; }

            public SealedNode Node4 { get; set; }

            public sealed class SealedNode
            {
                public int Number1 { get; set; }

                public int Number2 { get; set; }

                public string Text1 { get; set; }

                public string Text2 { get; set; }

                public int Number3 { get; set; }

                public int Number4 { get; set; }

                public string Text3 { get; set; }

                public string Text4 { get; set; }

                public KeyValuePair<int, double> KeyValuePair1 { get; set; }

                public KeyValuePair<int, double> KeyValuePair2 { get; set; }
            }
        }

        public class GraphClass
        {
            public int Number1 { get; set; }

            public int Number2 { get; set; }

            public string Text1 { get; set; }

            public string Text2 { get; set; }

            public int Number3 { get; set; }

            public int Number4 { get; set; }

            public string Text3 { get; set; }

            public string Text4 { get; set; }

            public KeyValuePair<int, double> KeyValuePair1 { get; set; }

            public KeyValuePair<int, double> KeyValuePair2 { get; set; }

            public Node Node1 { get; set; }

            public Node Node2 { get; set; }

            public Node Node3 { get; set; }

            public Node Node4 { get; set; }

            public class Node
            {
                public int Number1 { get; set; }

                public int Number2 { get; set; }

                public string Text1 { get; set; }

                public string Text2 { get; set; }

                public int Number3 { get; set; }

                public int Number4 { get; set; }

                public string Text3 { get; set; }

                public string Text4 { get; set; }

                public KeyValuePair<int, double> KeyValuePair1 { get; set; }

                public KeyValuePair<int, double> KeyValuePair2 { get; set; }
            }
        }

        public class BoxingVirtualGraphClass
        {
            public virtual object Number1 { get; set; }

            public virtual object Number2 { get; set; }

            public virtual object Text1 { get; set; }

            public virtual object Text2 { get; set; }

            public virtual object Number3 { get; set; }

            public virtual object Number4 { get; set; }

            public virtual object Text3 { get; set; }

            public virtual object Text4 { get; set; }

            public virtual object KeyValuePair1 { get; set; }

            public virtual object KeyValuePair2 { get; set; }

            public virtual object Node1 { get; set; }

            public virtual object Node2 { get; set; }

            public virtual object Node3 { get; set; }

            public virtual object Node4 { get; set; }

            public class Node
            {
                public virtual object Number1 { get; set; }

                public virtual object Number2 { get; set; }

                public virtual object Text1 { get; set; }

                public virtual object Text2 { get; set; }

                public virtual object Number3 { get; set; }

                public virtual object Number4 { get; set; }

                public virtual object Text3 { get; set; }

                public virtual object Text4 { get; set; }

                public virtual object KeyValuePair1 { get; set; }

                public virtual object KeyValuePair2 { get; set; }
            }
        }
    }
}