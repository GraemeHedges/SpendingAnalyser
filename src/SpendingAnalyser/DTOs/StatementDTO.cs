using CsvHelper.Configuration.Attributes;

namespace SpendingAnalyser.DTOs;

public record StatementDTO
{
    [Name("Date")]
    public required string Date { get; init; }

    [Name("Counter Party")]
    public required string CounterParty { get; init; }

    [Name("Reference")]
    public required string Reference { get; init; }

    [Name("Type")]
    public required string Type { get; init; }

    [Name("Amount (GBP)")]
    public required double AmountGBP { get; init; }

    [Name("Balance (GBP)")]
    public required double BalanceGBP { get; init;}

    [Name("Spending Category")]
    public required string SpendingCategory { get; init; }

    [Name("Notes")]
    public string? Notes { get; init; }
}
