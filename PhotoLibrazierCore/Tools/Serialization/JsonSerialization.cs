using Newtonsoft.Json;

namespace PhotoLibrazierCore.Tools.Serialization
{
    public class JsonSerialization: ISerialization
    {
        public string ToJsonByObject(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public object ToObjectByString(string json)
        {
            /*JsonSerializerSettings h = new JsonSerializerSettings();
            var gg = h.StringEscapeHandling;*/
            return JsonConvert.DeserializeObject(json);
        }

        public T ToObjectByString<T>(string json)
        {
            /*JsonSerializerSettings h = new JsonSerializerSettings();
            var gg = h.StringEscapeHandling;*/
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}


