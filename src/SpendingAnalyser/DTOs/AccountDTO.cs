using System.Text.Json.Serialization;

namespace SpendingAnalyser.DTOs;

public record AccountDTO
{
    [JsonPropertyName("accountUid")]
    public required string AccountUid { get; set; }

    [JsonPropertyName("accountType")]
    public string? AccountType { get; set; }

    [JsonPropertyName("defaultCategory")]
    public string? DefaultCategory { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

}
