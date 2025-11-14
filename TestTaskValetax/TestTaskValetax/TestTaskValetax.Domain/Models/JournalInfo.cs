
using TestTaskValetax.Domain.Models.ValueObjects;

namespace TestTaskValetax.Domain.Models;
public class JournalInfo
{
    public long EventId { get; set; }

    public DateTime CreatedAt { get; init; }

    public MJournalInfoContext Context { get; init; } = null!;

    public string Trace { get; init; } = string.Empty;
}