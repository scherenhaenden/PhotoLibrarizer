using System;
namespace PhotoLibrazierCore.Tools.Serialization
{
    public interface ISerialization
    {
        string ToJsonByObject(object obj);

        object ToObjectByString(string json);

        T ToObjectByString<T>(string json);
    }
}
