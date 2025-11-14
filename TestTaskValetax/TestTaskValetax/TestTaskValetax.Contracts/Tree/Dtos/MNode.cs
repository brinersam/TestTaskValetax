namespace TestTaskValetax.Contracts.Tree.Dtos;
public record MNode(long Id, string Name, List<MNode> Children);
