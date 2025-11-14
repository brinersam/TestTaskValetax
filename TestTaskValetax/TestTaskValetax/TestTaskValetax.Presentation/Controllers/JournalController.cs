using Microsoft.AspNetCore.Mvc;
using TestTaskValetax.Application.Features.Journal.GetEntries;
using TestTaskValetax.Application.Features.Journal.GetEntryById;
using TestTaskValetax.Contracts.Journal.Requests;
using TestTaskValetax.Core.Framework;
using TestTaskValetax.Core.HelperModels;

namespace TestTaskValetax.Presentation.Controllers;

public class JournalController : AppController
{
    [HttpPost("getRange")]
    public async Task<IActionResult> GetJournalEntires(
        [FromQuery] int skip,
        [FromQuery] int take,
        [FromBody] VJournalFilter? filter,
        [FromServices] GetJournalEntriesHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetJournalEntriesQuery(skip, take, new(filter.From, filter.To, filter.Search));
        var result = await handler.HandleAsync(query, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpGet("getSingle")]
    public async Task<IActionResult> GetJournalEntryById(
        [FromQuery] long id,
        [FromServices] GetJournalEntryByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.HandleAsync(id, cancellationToken);
        return Ok(result);
    }
}
