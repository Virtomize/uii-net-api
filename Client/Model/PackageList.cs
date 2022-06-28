using System.Text.Json.Serialization;

namespace UII
{
    public class PackageList
    {
        [JsonPropertyName("dist")]
        public string Distribution { get; set; } = String.Empty;

        [JsonPropertyName("version")] 
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("packages")] 
        public List<string> Packages { get; set; } = new List<string>();
    }
}