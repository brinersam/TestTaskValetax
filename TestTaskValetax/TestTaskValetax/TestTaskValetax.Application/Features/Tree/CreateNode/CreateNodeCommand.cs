namespace TestTaskValetax.Application.Features.Tree.CreateNode;
public record CreateNodeCommand(
    string treeName,
    long? parentNodeId,
    string nodeName);
