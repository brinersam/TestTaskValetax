namespace TestTaskValetax.Core.HelperModels;
public record EnvelopeErrors<TErr> : Envelope<TErr>
{
    public new List<TErr> Errors { get; }

    private EnvelopeErrors(TErr? result, IEnumerable<TErr> errors)
        : base(result)
    {
        Errors = errors.ToList();
    }

    public static EnvelopeErrors<TErr> Create(IEnumerable<TErr> errors)
    {
        return new EnvelopeErrors<TErr>(default, errors);
    }
}