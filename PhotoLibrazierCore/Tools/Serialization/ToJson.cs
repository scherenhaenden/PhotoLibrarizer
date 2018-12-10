namespace PhotoLibrazierCore.Tools.Serialization
{
    public class ToJson
    {
        public string ByObject(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}


