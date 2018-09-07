using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace Gu.Xml
{
    public class XSerializer
    {
        public static readonly XSerializer Default = new XSerializer();
    }

    public class Xml
    {
        public T Deserialize<T>(string s, XSerializer serializer = null)
        {
            return Deserialize<T>(new StringReader(s), serializer);
        }

        public T Deserialize<T>(TextReader reader, XSerializer serializer = null)
        {
            serializer = serializer ?? XSerializer.Default;
            // first, attempt to find a constructor
            // so far we'll assume there's a single constructor
            // it may be a no parameter one, in which case we'll resort to assigning values to properties
            var constructor = typeof(T).GetConstructors().SingleOrDefault();
            var parameters = constructor?.GetParameters();
            if (parameters == null || parameters.Length == 0)
            {
                // default constructor
            }
            else
            {
                // non-default constructor
            }
            throw new NotImplementedException();
        }
    }

    public class XDocument
    {

    }

    public class XReader
    {

    }
}
