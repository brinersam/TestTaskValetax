using System.Net;
using TestTaskValetax.Application.Interfaces;
using TestTaskValetax.Core.Framework.Exceptions;
using TestTaskValetax.Core.HelperModels;
using TestTaskValetax.Domain.Models;

namespace TestTaskValetax.Api.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    private readonly IJournalRepository _journalRepository;
    private readonly HttpContextData _contextData;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        IJournalRepository journalRepository,
        HttpContextData contextData,
        ILogger<ExceptionMiddleware> logger)
    {
        _journalRepository = journalRepository;
        _contextData = contextData;
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var baseEx = ex;
            while (baseEx.InnerException is not null)
                baseEx = baseEx.InnerException;

            _logger.LogError("Exception was caught!: {error} {stacktrace}", ex.Message, ex.StackTrace);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var entryIdRes = await _journalRepository.AddAsync(new JournalInfo()
            {
                Context = new() {Body = _contextData.Body, Query = _contextData.QueryKv! },
                CreatedAt = DateTime.UtcNow,
                Trace = ex.StackTrace!
            });

            object error = null!;

            if (baseEx is SecureException secureEx)
                error ??= Error.Secure(new ErrorData() { Message = secureEx.Message }, entryIdRes.Value);

            error ??= Error.Generic(entryIdRes.Value);
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}