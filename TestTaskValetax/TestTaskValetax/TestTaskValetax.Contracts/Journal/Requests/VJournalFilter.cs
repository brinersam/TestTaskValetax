namespace TestTaskValetax.Contracts.Journal.Requests;

public record VJournalFilter(
    DateTime? From,
    DateTime? To,
    string? Search);