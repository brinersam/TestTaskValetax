using CSharpFunctionalExtensions;
using TestTaskValetax.Application.Features.Tree.CreateNode;
using TestTaskValetax.Core.HelperModels;
using TestTaskValetax.Domain.Models;

namespace TestTaskValetax.Infrastructure.Repositories;

public interface INodeRepository
{
    Task<Node> CreateNode(CreateNodeCommand cmd, CancellationToken cancellationToken = default);
    Task<long> DeleteNode(long nodeId, CancellationToken cancellationToken = default);
    Task<Result<Node, Error>> GetTree(string nodeName, CancellationToken cancellationToken = default);
    Task<long> RenameNode(long id, string newName, CancellationToken cancellationToken = default);
}