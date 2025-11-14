namespace TestTaskValetax.Api.Middleware;

public class BufferingMiddleware : IMiddleware
{

    public BufferingMiddleware(HttpContextData httpContextDataService)
    { }


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Request.EnableBuffering();
        await next(context);
    }
}