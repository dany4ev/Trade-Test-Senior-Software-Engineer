using Newtonsoft.Json;


namespace Trade_Test.Utilities.Extensions
{
    public class Serializer
    {
        private readonly Formatting formatting;

        public Serializer()
        {
            formatting = Formatting.None;
        }

        public string Serialize<T>(T model, JsonSerializerSettings? jsonSerializerSettings = null) where T : class
        {
            string json = string.Empty;

            json = model != null
                ? JsonConvert.SerializeObject(model, formatting, jsonSerializerSettings)
                : "Unable Serialize Object, because model is empty";

            return json;
        }

        public T? Deserialize<T>(string jsonString, JsonSerializerSettings? jsonSerializerSettings = null) where T : class
        {
            T? json = null;

            if (!string.IsNullOrEmpty(jsonString))
            {
                json = JsonConvert.DeserializeObject<T>(jsonString, jsonSerializerSettings);
            }

            return json;
        }
    }
}
