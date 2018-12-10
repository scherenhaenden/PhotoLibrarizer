namespace PhotoLibrazierCore.Tools.Serialization
{
    public class ToJson: ISerialize
    {
        public string ByObject(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}


