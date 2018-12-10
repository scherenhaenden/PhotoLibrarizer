namespace PhotoLibrazierCore.Tools.Serialization
{
    public class Json: ISerialization
    {
        public string ByObject(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}


