namespace TestTaskValetax.Core.HelperModels;
public record Envelope
{
    public static EnvelopeErrors<T> Error<T>(IEnumerable<T> errors)
    {
        return EnvelopeErrors<T>.Create(errors);
    }

    public static Envelope<T> Ok<T>(T? result)
    {
        return new Envelope<T>(result);
    }
}

public record Envelope<T>
{
    public T? Result { get; }

    public DateTime? TimeGenerated { get; }

    public object? Errors { get; }

    internal Envelope(T? result)
    {
        Result = result;
        TimeGenerated = DateTime.UtcNow;
    }
}