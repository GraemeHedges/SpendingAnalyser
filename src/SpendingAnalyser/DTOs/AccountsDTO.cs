using System.Text.Json.Serialization;

namespace SpendingAnalyser.DTOs;

public record AccountsDTO
{
    [JsonPropertyName("accounts")]
    public required IEnumerable<AccountDTO> Accounts { get; set; }
}
