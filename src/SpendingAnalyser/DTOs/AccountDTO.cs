using System.Text.Json.Serialization;

namespace SpendingAnalyser.DTOs;

public record AccountDTO
{
    [JsonPropertyName("accountUid")]
    public required string AccountUid { get; init; }

    [JsonPropertyName("accountType")]
    public string? AccountType { get; init; }

    [JsonPropertyName("defaultCategory")]
    public string? DefaultCategory { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

}
