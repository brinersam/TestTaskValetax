using Microsoft.AspNetCore.Mvc;
using TestTaskValetax.Application.Features.Tree.CreateNode;
using TestTaskValetax.Application.Features.Tree.DeleteNode;
using TestTaskValetax.Application.Features.Tree.GetTree;
using TestTaskValetax.Application.Features.Tree.RenameNode;
using TestTaskValetax.Core.Framework;
using TestTaskValetax.Core.HelperModels;

namespace TestTaskValetax.Presentation.Controllers;

public class TreeController : AppController
{
    [HttpGet()]
    public async Task<IActionResult> GetTree(
        [FromQuery] string treeName,
        [FromServices] GetTreeHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.HandleAsync(treeName, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPost()]
    public async Task<IActionResult> CreateNode(
        [FromQuery] string treeName,
        [FromQuery] long? parentNodeId,
        [FromQuery] string nodeName,
        [FromServices] CreateNodeHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateNodeCommand(treeName, parentNodeId, nodeName);
        var result = await handler.HandleAsync(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpDelete("{nodeId:long}")]
    public async Task<IActionResult> DeleteNode(
        [FromRoute] long nodeId,
        [FromServices] DeleteNodeHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.HandleAsync(nodeId, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPatch("{nodeId:long}")]
    public async Task<IActionResult> RenameNode(
        [FromRoute] long nodeId,
        [FromQuery] string newNodeName,
        [FromServices] RenameNodeHandler handler,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.HandleAsync(nodeId, newNodeName, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok();
    }
}

