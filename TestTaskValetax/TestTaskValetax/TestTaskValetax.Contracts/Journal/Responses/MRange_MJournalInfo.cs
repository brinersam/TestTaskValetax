using TestTaskValetax.Contracts.Journal.Dtos;

namespace TestTaskValetax.Contracts.Journal.Responses;

public record MRange_MJournalInfo(
    int Skip,
    int Count,
    List<MJournal> Items);
