using System.Text.Json.Serialization;

public class Embedded<T>
{
    [JsonPropertyName("_embedded")]
    public T Inner { get; set; }
}