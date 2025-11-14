namespace TestTaskValetax.Application.Features.Journal.GetEntries;
public record GetJournalEntriesQuery(
        int Skip,
        int Take,
        GetEntriesFilter? Filter);

public record GetEntriesFilter(
    DateTime? From,
    DateTime? To,
    string? Search);