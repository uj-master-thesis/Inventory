using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inventory.WebApi;

public static class WebApi
{
    public static void ConfigureWebApi(this IServiceCollection services) 
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
