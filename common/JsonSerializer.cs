namespace Common
{
    public class JsonSerializer : ISerializer<string>
    {
        public string Serialize<TValue>(TValue value)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }

        public TValue Deserialize<TValue>(string wire)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TValue>(wire);
        }
    }
}
