using CSharpFunctionalExtensions;
using TestTaskValetax.Application.Interfaces;
using TestTaskValetax.Core.HelperModels;
using TestTaskValetax.Domain.Models;

namespace TestTaskValetax.Application.Features.Tree.GetTree;
public class GetTreeHandler
{
    private readonly INodeRepository _nodeRepository;

    public GetTreeHandler(INodeRepository nodeRepository)
    {
        _nodeRepository = nodeRepository;
    }

    public async Task<Result<Node, Error>> HandleAsync(
        string treeName,
        CancellationToken ct = default)
    {
        var result = await _nodeRepository.GetTree(treeName, ct);
        return result;
    }
}
