using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TestTaskValetax.Core.Options;
using TestTaskValetax.Domain.Models;

namespace TestTaskValetax.Infrastructure.Database;
public class AppDbContext : DbContext
{
    private readonly OptionsDatabase _options;

    public DbSet<Node> Nodes => Set<Node>();
    public DbSet<JournalInfo> JournalInfos => Set<JournalInfo>();

    public AppDbContext(
        IOptions<OptionsDatabase> options,
        DbContextOptions<AppDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
        _options = options.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        ConfigureDb(optionsBuilder, _options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("ltree");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public static void ConfigureDb(DbContextOptionsBuilder optionsBuilder, OptionsDatabase options)
    {
        optionsBuilder.UseNpgsql(options.ConnectionString);
        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        optionsBuilder.EnableSensitiveDataLogging();
    }
}