using CSharpFunctionalExtensions;
using TestTaskValetax.Application.Features.Journal.Mapper;
using TestTaskValetax.Application.Interfaces;
using TestTaskValetax.Contracts.Journal.Responses;
using TestTaskValetax.Core.HelperModels;

namespace TestTaskValetax.Application.Features.Journal.GetEntries;
public class GetJournalEntriesHandler
{
    private readonly IJournalRepository _journalRepository;

    public GetJournalEntriesHandler(IJournalRepository journalRepository)
    {
        _journalRepository = journalRepository;
    }

    public async Task<Result<MRange_MJournalInfo, Error>> HandleAsync(
        GetJournalEntriesQuery query,
        CancellationToken ct = default)
    {
        var (result, totalAmount) = await _journalRepository.GetFiltered(query, ct);

        return new MRange_MJournalInfo
            (
                query.Skip,
                totalAmount,
                result.Select(x => x.ToDto()).ToList()
            );
    }
}
