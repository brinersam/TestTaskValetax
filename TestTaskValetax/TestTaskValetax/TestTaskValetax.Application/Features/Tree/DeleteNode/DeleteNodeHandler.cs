using CSharpFunctionalExtensions;
using TestTaskValetax.Application.Interfaces;
using TestTaskValetax.Core.HelperModels;

namespace TestTaskValetax.Application.Features.Tree.DeleteNode;
public class DeleteNodeHandler
{
    private readonly INodeRepository _nodeRepository;

    public DeleteNodeHandler(INodeRepository nodeRepository)
    {
        _nodeRepository = nodeRepository;
    }

    public async Task<Result<long, Error>> HandleAsync(
        long id,
        CancellationToken ct = default)
    {
        var result = await _nodeRepository.DeleteNode(id, ct);
        return result;
    }
}
