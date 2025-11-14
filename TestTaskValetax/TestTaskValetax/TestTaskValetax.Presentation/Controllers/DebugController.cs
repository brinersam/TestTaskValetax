using Microsoft.AspNetCore.Mvc;
using TestTaskValetax.Core.Framework;
using TestTaskValetax.Core.Framework.Exceptions;
using TestTaskValetax.Domain.Models;

namespace TestTaskValetax.Presentation.Controllers;
public class DebugController : AppController
{
    [HttpPost("GetNotImplementedException")]
    public Task<IActionResult> ThrowNotImplementedException(
        [FromBody] Node node,
        [FromQuery] string someString,
        [FromQuery] int someInt,
        [FromQuery] bool someBool,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    [HttpPost("GetSecureException")]
    public Task<IActionResult> ThrowSecureException(
        [FromBody] Node node,
        [FromQuery] string someString,
        [FromQuery] int someInt,
        [FromQuery] bool someBool,
        CancellationToken cancellationToken = default)
    {
        throw new SecureException();
    }
}
