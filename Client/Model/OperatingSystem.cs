using System.Diagnostics;
using System.Text.Json.Serialization;

namespace UII
{
    /// <summary>
    /// A operating system.
    /// </summary>
    [DebuggerDisplay("{DisplayName}")]
    public class OperatingSystem
    {
        [JsonPropertyName("displayname")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonPropertyName("dist")]
        public string Distribution { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("arch")]
        public string Architecture { get; set; } = string.Empty;
    }
}