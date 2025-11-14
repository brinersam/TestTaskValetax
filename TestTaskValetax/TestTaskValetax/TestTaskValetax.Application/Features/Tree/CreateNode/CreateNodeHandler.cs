using CSharpFunctionalExtensions;
using TestTaskValetax.Core.HelperModels;
using TestTaskValetax.Domain.Models;
using TestTaskValetax.Infrastructure.Repositories;

namespace TestTaskValetax.Application.Features.Tree.CreateNode;
public class CreateNodeHandler
{
    private readonly INodeRepository _nodeRepository;

    public CreateNodeHandler(INodeRepository nodeRepository)
    {
        _nodeRepository = nodeRepository;
    }


    public async Task<Result<Node, Error>> HandleAsync(
        CreateNodeCommand cmd,
        CancellationToken ct = default)
    {
        var result = await _nodeRepository.CreateNode(cmd, ct);
        return result;
    }
}
