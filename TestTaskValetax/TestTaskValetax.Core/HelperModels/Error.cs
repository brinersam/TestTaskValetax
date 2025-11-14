namespace TestTaskValetax.Core.HelperModels;

public class Error
{
    public long Id { get; }

    public string Type { get; }

    public object Data { get; }

    public Error(string type, long id = default)
    {
        Type = type;
        Id = id;
    }

    public static Error<T> Secure<T>(T data, long id = default)
    {
        return new Error<T>(ErrorTypes.Secure, data);
    }

    public static Error<ErrorData> Generic(long id = default)
    {
        return new Error<ErrorData>
        (
            ErrorTypes.Exception,
            new() { Message = $"Internal server error ID = {id}" },
            id
        );
    }
}

public class Error<T> : Error
{
    public new T Data { get; }

    internal Error(string type, T data, long id = default) : base(type, id)
    {
        Data = data;
    }
}

public static class ErrorTypes
{
    public const string Exception = nameof(Exception);

    public const string Secure = nameof(Secure);
}   

public class ErrorData
{
    public string Message { get; init; } = null!;
}