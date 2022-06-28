using System.Text.Json.Serialization;

namespace UII
{
    public class Network
    {
        [JsonPropertyName("dhcp")]
        public bool UsesDhcp { get; init; }

        [JsonPropertyName("domain")]
        public string Domain { get; init; } = string.Empty;

        [JsonPropertyName("dns")]
        public IEnumerable<string> DNSs{ get; init; } = Array.Empty<string>();
    
        [JsonPropertyName("ipnet")]
        public string IpNetmask { get; init; } = string.Empty;
    
        [JsonPropertyName("gateway")]
        public string Gateway { get; init; } = string.Empty;
    
        [JsonPropertyName("nointernet")]
        public bool HasNoInternet { get; init; }
    }
}