using Inventory.Application.Interfaces;
using Inventory.Infractracture.WriteRepositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inventory.Infractracture.DbConfiguration;

public static class DbConfiguration
{
    public static IServiceCollection InitializeInfrasctracture(this IServiceCollection services, IConfiguration configuration)
    {
        var config = configuration.GetSection(nameof(DatabaseConfiguration)).Get<DatabaseConfiguration>();

        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddSqlServer<InventoryContext>(config.ConectionString)
                .AddScoped(typeof(IPostWriteRepository), typeof(PostWriteRepository))
                .AddScoped(typeof(IThreadWriteRepository), typeof(ThreadWriteRepository));
        return services;
    }                                                                                               
}
