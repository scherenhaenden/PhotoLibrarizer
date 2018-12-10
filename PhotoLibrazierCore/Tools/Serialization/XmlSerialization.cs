using System;
using System.Xml;

namespace PhotoLibrazierCore.Tools.Serialization
{
    public class XmlSerialization: ISerialization
    {
        public object ToObjectByString(string json)
        {
            throw new NotImplementedException();
        }

        public T ToObjectByString<T>(string json)
        {
            throw new NotImplementedException();
        }

        string ISerialization.ToJsonByObject(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
