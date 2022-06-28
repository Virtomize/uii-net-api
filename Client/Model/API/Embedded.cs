using System.Text.Json.Serialization;

namespace UII
{
    public class Embedded<T>
    {
        [JsonPropertyName("_embedded")]
        public T Inner { get; set; }
    }
}