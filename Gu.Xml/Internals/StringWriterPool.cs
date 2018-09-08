namespace Gu.Xml
{
    using System.Collections.Concurrent;
    using System.IO;
    using System.Text;

    internal static class StringWriterPool
    {
        private static readonly ConcurrentQueue<Item> Cache = new ConcurrentQueue<Item>();

        internal static Item Borrow()
        {
            if (Cache.TryDequeue(out var cached))
            {
                return cached;
            }

            return new Item();
        }

        internal static void Return(Item item)
        {
            item.Builder.Clear();
            Cache.Enqueue(item);
        }

        internal class Item
        {
            public Item()
            {
                this.Writer = new XmlWriter(new StringWriter(this.Builder));
            }

            internal StringBuilder Builder { get; } = new StringBuilder();

#pragma warning disable IDISP002 // Dispose member.
#pragma warning disable IDISP006 // Implement IDisposable.
            internal XmlWriter Writer { get; }
#pragma warning restore IDISP006 // Implement IDisposable.
#pragma warning restore IDISP002 // Dispose member.
        }
    }
}