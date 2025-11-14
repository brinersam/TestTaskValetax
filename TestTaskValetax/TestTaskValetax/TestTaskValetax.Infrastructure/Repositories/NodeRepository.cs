using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TestTaskValetax.Application.Features.Tree.CreateNode;
using TestTaskValetax.Core.Framework.Exceptions;
using TestTaskValetax.Core.HelperModels;
using TestTaskValetax.Domain.Models;
using TestTaskValetax.Infrastructure.Database;

namespace TestTaskValetax.Infrastructure.Repositories;
public class NodeRepository : INodeRepository
{
    private readonly AppDbContext _dbContext;

    public NodeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Node, Error>> GetTree(string nodeName, CancellationToken cancellationToken = default)
    {
        Node node = null!;

        var parameters = new List<NpgsqlParameter>();
        var sql =
            $"""
            WITH p AS (
                SELECT path
                FROM "Nodes"
                WHERE name = @nodename
            )
            SELECT n.*
            FROM "Nodes" n
            JOIN p t ON n.path <@ t.path
            """;
        parameters.Add(new NpgsqlParameter("nodename", nodeName));

        var nodes = await _dbContext.Nodes
            .FromSqlRaw(sql, parameters.ToArray())
            .AsNoTracking()
            .Select(x => new { key = x.Id, value = x })
            .ToDictionaryAsync(x => x.key, y => y.value, cancellationToken);

        if (nodes.Count <= 0)
        {
            node = new Node() { Name = nodeName, Path = nodeName };
            await _dbContext.Nodes.AddAsync(node, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        foreach (var value in nodes.Values)
        {
            if (value.ParentId is not null && nodes.ContainsKey((long)value.ParentId))
                nodes[(long)value.ParentId].Children.Add(value);
        }

        node ??= nodes.Values.FirstOrDefault(x => x.Name == nodeName)!;

        return node;
    }

    public async Task<long> DeleteNode(long nodeId, CancellationToken cancellationToken = default)
    {
        var node = await _dbContext.Nodes.FirstOrDefaultAsync(x => x.Id == nodeId, cancellationToken);
        if (node is null) return nodeId;

        _dbContext.Nodes.Remove(node);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return nodeId;
    }

    public async Task<Node> CreateNode(CreateNodeCommand cmd, CancellationToken cancellationToken = default)
    {
        try
        {
            var parentNode = await _dbContext.Nodes.FirstOrDefaultAsync(x => x.Id == cmd.parentNodeId, cancellationToken);

            var newNode = new Node()
            {
                Name = cmd.nodeName,
                ParentId = parentNode?.Id,
                Path = String.IsNullOrWhiteSpace(parentNode?.Path) ? cmd.nodeName : $"{parentNode?.Path}.{cmd.nodeName}"
            };

            await _dbContext.Nodes.AddAsync(newNode, cancellationToken);


            await _dbContext.SaveChangesAsync(cancellationToken);

            return newNode;
        }
        catch (Exception ex)
        {
            throw new SecureException(ex, "Failed to create node!");
        }
    }

    public async Task<long> RenameNode(long id, string newName, CancellationToken cancellationToken = default)
    {
        try
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var parameters = new List<NpgsqlParameter>();

            var targetNode = _dbContext.Nodes.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (targetNode is null)
                throw new SecureException("No specified node exists!");

            var sql =
                """
                WITH target AS(
                    SELECT * 
                    FROM "Nodes"
                    WHERE
                        id = @id
                )
                UPDATE "Nodes" n
                SET 
                    path = CASE 
                	    WHEN n.id = target.id THEN
                		    subpath(n.path, 0, nlevel(n.path)-1) || @newname::ltree
                	    ELSE
                	        subpath(n.path, 0, nlevel(target.path)-1) || @newname::ltree || subpath(n.path, nlevel(target.path))
                    END,
                    name = CASE 
                        WHEN n.id = target.id THEN
                		    @newname
                	    ELSE
                            n.name
                	END
                FROM 
                    target
                WHERE
                    n."path" <@ target.path;
                """;
            parameters.Add(new NpgsqlParameter("id", id));
            parameters.Add(new NpgsqlParameter("newname", newName));

            var dbQuery = await _dbContext.Database
                .ExecuteSqlRawAsync(sql, parameters.ToArray());
            await transaction.CommitAsync(cancellationToken);
            return id;

        }
        catch (Exception ex)
        {
            throw new SecureException(ex, "Failed to rename node!");
        }
    }
}
