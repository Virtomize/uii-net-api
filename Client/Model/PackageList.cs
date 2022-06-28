using System.Text.Json.Serialization;

namespace UII
{
    public class PackageList
    {
        [JsonPropertyName("dist")]
        public string Distribution { get; set; }
    
        [JsonPropertyName("version")]
        public string Version { get; set; }
    
        [JsonPropertyName("packages")]
        public List<string> Packages { get; set; }
    }
}