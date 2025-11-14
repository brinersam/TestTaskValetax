using TestTaskValetax.Domain.Models;

namespace TestTaskValetax.Application.Interfaces;

public interface IReadDbContext
{
    IQueryable<Node> Nodes { get; }
    IQueryable<JournalInfo> JournalInfos { get; }
}