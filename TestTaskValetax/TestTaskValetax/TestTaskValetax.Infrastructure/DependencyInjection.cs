using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TestTaskValetax.Application.Interfaces;
using TestTaskValetax.Core.Options;
using TestTaskValetax.Infrastructure.Database;
using TestTaskValetax.Infrastructure.Repositories;

namespace TestTaskValetax.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceProvider)
    {
        return serviceProvider
            .AddDbContext<AppDbContext>((sp, opts) =>
            {
                var dboptions = sp.GetRequiredService<IOptions<OptionsDatabase>>().Value;
                AppDbContext.ConfigureDb(opts, dboptions);
            })
            .AddScoped<IReadDbContext, ReadDbContext>()
            .AddScoped<IJournalRepository, JournalRepository>()
            .AddScoped<INodeRepository, NodeRepository>();
    }
}
