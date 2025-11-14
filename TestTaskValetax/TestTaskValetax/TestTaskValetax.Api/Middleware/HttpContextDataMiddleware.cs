namespace TestTaskValetax.Api.Middleware;

public class HttpContextDataMiddleware : IMiddleware
{
    private readonly HttpContextData _httpContextData;

    public HttpContextDataMiddleware(HttpContextData httpContextDataService)
    {
        _httpContextData = httpContextDataService;
    }


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        foreach (var queryParam in context.Request.Query)
            _httpContextData.QueryKv[queryParam.Key] = queryParam.Value;

        await ReadBodyAsync(context);

        await next(context);
    }

    private async Task ReadBodyAsync(HttpContext context)
    {
        var reader = new StreamReader(context.Request.Body, leaveOpen: true);

        _httpContextData.Body = await reader.ReadToEndAsync();

        context.Request.Body.Seek(0, SeekOrigin.Begin);
        reader.Dispose();
    }
}