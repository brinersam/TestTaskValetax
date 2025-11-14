using CSharpFunctionalExtensions;
using TestTaskValetax.Application.Interfaces;
using TestTaskValetax.Core.HelperModels;

namespace TestTaskValetax.Application.Features.Tree.RenameNode;
public class RenameNodeHandler
{
    private readonly INodeRepository _nodeRepository;

    public RenameNodeHandler(INodeRepository nodeRepository)
    {
        _nodeRepository = nodeRepository;
    }

    public async Task<UnitResult<Error>> HandleAsync(
        long id,
        string newName,
        CancellationToken ct = default)
    {
        var result = await _nodeRepository.RenameNode(id, newName, ct);
        return Result.Success<Error>();
    }
}
