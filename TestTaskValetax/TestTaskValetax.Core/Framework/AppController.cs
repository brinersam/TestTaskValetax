using Microsoft.AspNetCore.Mvc;
using TestTaskValetax.Core.HelperModels;

namespace TestTaskValetax.Core.Framework;

[ApiController]
[Route("api/user/[controller]")]
public abstract class AppController : ControllerBase
{
    public BadRequestObjectResult BadRequest<T>(T[] error)
    {
        return new BadRequestObjectResult(Envelope.Error([error]));
    }

    public override BadRequestObjectResult BadRequest(object? error)
    {
        return BadRequest([error]);
    }

    public override OkObjectResult Ok(object? value)
    {
        return new OkObjectResult(Envelope.Ok(value));
    }
}