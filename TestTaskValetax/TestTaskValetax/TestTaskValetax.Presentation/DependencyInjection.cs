using Microsoft.Extensions.DependencyInjection;
using TestTaskValetax.Application.Features.Journal.GetEntries;
using TestTaskValetax.Application.Features.Journal.GetEntryById;
using TestTaskValetax.Application.Features.Tree.CreateNode;
using TestTaskValetax.Application.Features.Tree.DeleteNode;
using TestTaskValetax.Application.Features.Tree.GetTree;
using TestTaskValetax.Application.Features.Tree.RenameNode;

namespace TestTaskValetax.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection serviceProvider)
    {
        return serviceProvider
            .AddScoped<GetJournalEntriesHandler>()
            .AddScoped<DeleteNodeHandler>()
            .AddScoped<GetTreeHandler>()
            .AddScoped<CreateNodeHandler>()
            .AddScoped<RenameNodeHandler>()
            .AddScoped<GetJournalEntryByIdHandler>();
    }
}
