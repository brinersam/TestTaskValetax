namespace TestTaskValetax.Domain.Models;
public class Node
{
    public long Id { get; set; }

    public long? ParentId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Path { get; set; } = string.Empty;

    public List<Node> Children { get; set; } = [];
}