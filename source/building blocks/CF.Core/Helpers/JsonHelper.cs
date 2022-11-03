using System.Text.Json;

namespace CF.Core.Helpers
{
    public class JsonHelper
    {
        public static string ToJson<TObject>(TObject @object) => JsonSerializer.Serialize(@object);
        public static TObject? FromJson<TObject>(string json) => JsonSerializer.Deserialize<TObject>(json);
        public static TTarget? SerializeAndDeserialize<TOrigin, TTarget>(TOrigin @object)
        {
            string json = ToJson(@object);

            return FromJson<TTarget>(json);
        }
    }
}
