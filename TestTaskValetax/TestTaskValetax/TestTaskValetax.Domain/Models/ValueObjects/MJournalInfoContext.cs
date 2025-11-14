using CSharpFunctionalExtensions;

namespace TestTaskValetax.Domain.Models.ValueObjects;
public class MJournalInfoContext : ValueObject
{
    public Dictionary<string, string> Query { get; set; } = new();
    public string Body { get; set; } = string.Empty;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Query;
        yield return Body;
    }
}
