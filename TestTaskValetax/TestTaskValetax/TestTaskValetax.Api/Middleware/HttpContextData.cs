namespace TestTaskValetax.Api.Middleware;

public class HttpContextData
{
    public required string Body { get; set; } = string.Empty;
    public required Dictionary<string, string> QueryKv { get; set; } = new();
}
