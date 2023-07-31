using System.Text.Json.Serialization;

namespace StandUpKitV2.Models;

internal class CustomTransform
{
    /// <summary>
    /// File name
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// File path
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; set; }

    /// <summary>
    /// File content escaped with CDATA tags
    /// </summary>
    [JsonPropertyName("codeContent")]
    public string CodeContent { get; set; }

}