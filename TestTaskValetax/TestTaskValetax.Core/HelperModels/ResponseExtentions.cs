using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestTaskValetax.Core.HelperModels;
public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
        => ToResponse([error]);

    public static ActionResult ToResponse(this Error[] errors)
    {
        return new ObjectResult(Envelope.Error(errors))
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}