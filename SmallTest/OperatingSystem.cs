using System.Diagnostics;
using System.Text.Json.Serialization;

/// <summary>
/// A operating system.
/// </summary>
[DebuggerDisplay("{DisplayName}")]
public class OperatingSystem
{
    [JsonPropertyName("displayname")]
    public string DisplayName { get; set; }
    
    [JsonPropertyName("dist")]
    public string Distribution { get; set; }
    
    [JsonPropertyName("version")]
    public string Version { get; set; }
    
    [JsonPropertyName("arch")]
    public string Architecture { get; set; }
}