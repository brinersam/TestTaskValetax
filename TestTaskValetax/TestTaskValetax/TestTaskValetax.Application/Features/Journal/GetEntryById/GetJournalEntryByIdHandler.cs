using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TestTaskValetax.Application.Features.Journal.Mapper;
using TestTaskValetax.Application.Interfaces;
using TestTaskValetax.Contracts.Journal.Dtos;

namespace TestTaskValetax.Application.Features.Journal.GetEntryById;
public class GetJournalEntryByIdHandler
{
    private readonly IReadDbContext _readDb;

    public GetJournalEntryByIdHandler(IReadDbContext readDb)
    {
        _readDb = readDb;
    }

    public async Task<MJournal> HandleAsync(
        long id,
        CancellationToken ct = default)
    {
        var result = await _readDb.JournalInfos
            .Where(x => x.EventId == id)
            .Select(x => x.ToDto())
            .FirstOrDefaultAsync(ct);

        return result!;
    }
}
