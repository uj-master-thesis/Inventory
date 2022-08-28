using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Application.Interfaces.WriteRepositories;
using Inventory.Infractracture.DbConfiguration.Dapper;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Inventory.Infractracture.ReadRepositories;
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

        services.AddSingleton(config)
                .AddSingleton<InventoryReadContext>()
                .AddSqlServer<InventoryWriteContext>(config.ConectionString)
                .AddScoped<IPostWriteRepository, PostWriteRepository>()
                .AddScoped<IThreadWriteRepository, ThreadWriteRepository>()
                .AddScoped<IReadPostRepository, PostReadRepository>()
                .AddScoped<IReadThreadRepository, ThreadReadRepository>();

        return services;
    }                                                                                               
}
