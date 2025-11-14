using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Text;
using TestTaskValetax.Application.Features.Journal.GetEntries;
using TestTaskValetax.Application.Interfaces;
using TestTaskValetax.Core.HelperModels;
using TestTaskValetax.Domain.Models;
using TestTaskValetax.Infrastructure.Database;

namespace TestTaskValetax.Infrastructure.Repositories;
public class JournalRepository : IJournalRepository
{
    private readonly AppDbContext _dbContext;

    public JournalRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<long, Error>> AddAsync(JournalInfo journalInfo, CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.JournalInfos.AddAsync(journalInfo, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return journalInfo.EventId;
    }

    public async Task<Result<long, Error>> Save(JournalInfo journalInfo, CancellationToken cancellationToken = default)
    {
        _dbContext.JournalInfos.Attach(journalInfo);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return journalInfo.EventId;
    }

    public async Task<Result<long, Error>> Delete(JournalInfo journalInfo, CancellationToken cancellationToken = default)
    {
        _dbContext.JournalInfos.Remove(journalInfo);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return journalInfo.EventId;
    }

    public async Task<(List<JournalInfo> data, int totalCount)> GetFiltered(GetJournalEntriesQuery query, CancellationToken cancellationToken = default)
    {
        var parameters = new List<NpgsqlParameter>();

        var filterSql = new StringBuilder("(1 = 1)");
        if (query.Filter is not null)
        {
            if (String.IsNullOrWhiteSpace(query.Filter.Search) == false)
            {
                filterSql.Append($" AND (j.context::text ILIKE @search OR j.trace ILIKE @search)");
                parameters.Add(new NpgsqlParameter("search", $"%{query.Filter.Search}%"));
            }

            if (query.Filter.From is not null)
            {
                filterSql.Append($" AND j.created_at >= @from");
                parameters.Add(new NpgsqlParameter("from", query.Filter.From));

            }

            if (query.Filter.To is not null)
            {
                filterSql.Append($" AND j.created_at <= @to");
                parameters.Add(new NpgsqlParameter("to", query.Filter.To));
            }
        }

        var countSql =
            $"""
            SELECT COUNT (*) FROM
                "JournalInfos" j
            WHERE
                {filterSql.ToString()}
            group by j.event_id
            """;

        var totalCount = await _dbContext.JournalInfos
            .FromSqlRaw(countSql, parameters.ToArray())
            .CountAsync(cancellationToken);

        var sql =
            $"""
            SELECT * FROM
                "JournalInfos" j
            WHERE
                {filterSql.ToString()}
            ORDER BY j.event_id
            LIMIT @limit
            OFFSET @offset
            """;

        parameters.Add(new NpgsqlParameter("limit", query.Take));
        parameters.Add(new NpgsqlParameter("offset", query.Skip));

        var dbQuery = await _dbContext.JournalInfos
            .FromSqlRaw(sql, parameters.ToArray())
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return (dbQuery, totalCount);
    }
}