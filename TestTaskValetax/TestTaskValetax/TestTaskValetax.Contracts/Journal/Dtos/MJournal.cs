namespace TestTaskValetax.Contracts.Journal.Dtos;
public record MJournal(
    long EventId,
    DateTime CreatedAt,
    string Text);