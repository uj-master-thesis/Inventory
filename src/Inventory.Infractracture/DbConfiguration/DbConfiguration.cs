using Inventory.Application.Interfaces.ReadRepositories;
using Inventory.Application.Interfaces.WriteRepositories;
using Inventory.Infractracture.DbConfiguration.Dapper;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Inventory.Infractracture.ReadRepositories;
using Inventory.Infractracture.WriteRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                .AddDbContext<InventoryWriteContext>(options =>
                {
                    options.UseSqlServer(config.ConectionString);
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                })
                .AddScoped<IPostWriteRepository, PostWriteRepository>()
                .AddScoped<IThreadWriteRepository, ThreadWriteRepository>()
                .AddScoped<ICommentWriteRepository, CommentWriteRepository>()
                .AddScoped<IReadPostRepository, PostReadRepository>()
                .AddScoped<IReadThreadRepository, ThreadReadRepository>()
                .AddScoped<IVoteWriteRepository, VoteWriteRepository>()
                .AddScoped<IReadCommentRepository, CommentReadRepository>();

        return services;
    }                                                                                               
}
