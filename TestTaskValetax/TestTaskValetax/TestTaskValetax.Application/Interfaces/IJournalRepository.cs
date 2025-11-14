using CSharpFunctionalExtensions;
using TestTaskValetax.Application.Features.Journal.GetEntries;
using TestTaskValetax.Core.HelperModels;
using TestTaskValetax.Domain.Models;

namespace TestTaskValetax.Application.Interfaces;

public interface IJournalRepository
{
    Task<Result<long, Error>> AddAsync(JournalInfo journalInfo, CancellationToken cancellationToken = default);
    Task<Result<long, Error>> Delete(JournalInfo journalInfo, CancellationToken cancellationToken = default);
    Task<Result<long, Error>> Save(JournalInfo journalInfo, CancellationToken cancellationToken = default);
    Task<(List<JournalInfo> data, int totalCount)> GetFiltered(GetJournalEntriesQuery query, CancellationToken cancellationToken = default);
}