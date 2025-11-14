using TestTaskValetax.Contracts.Journal.Dtos;
using TestTaskValetax.Domain.Models;

namespace TestTaskValetax.Application.Features.Journal.Mapper;
public static class JournalMapper
{
    public static MJournal ToDto(this JournalInfo journal)
    => new MJournal
            (
                journal.EventId,
                journal.CreatedAt,
                $"""
                Query: {String.Join(';', journal.Context.Query.Select(q => $"[{q.Key}]={q.Value}"))}
                Body: {journal.Context.Body}
                Stacktrace: {journal.Trace}
                """
            );
}
