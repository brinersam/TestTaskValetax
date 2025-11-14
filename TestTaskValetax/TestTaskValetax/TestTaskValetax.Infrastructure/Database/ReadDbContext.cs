using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TestTaskValetax.Application.Interfaces;
using TestTaskValetax.Core.Options;
using TestTaskValetax.Domain.Models;

namespace TestTaskValetax.Infrastructure.Database;
public class ReadDbContext : AppDbContext, IReadDbContext
{
    public ReadDbContext(IOptions<OptionsDatabase> options, DbContextOptions<AppDbContext> dbContextOptions) : base(options, dbContextOptions)
    {
    }

    public IQueryable<Node> Nodes => Set<Node>().AsNoTracking();

    public IQueryable<JournalInfo> JournalInfos => Set<JournalInfo>().AsNoTracking();
}
