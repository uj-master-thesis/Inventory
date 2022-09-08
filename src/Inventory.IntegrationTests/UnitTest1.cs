using Inventory.Application.Commands.AddPostCommand;
using Inventory.Application.Commands.AddThreadCommand;
using Inventory.Application.Response;
using Inventory.Consumer;
using Inventory.Infractracture.DbConfiguration.EntityFramework;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Inventory.IntegrationTests;

public class EndToEndScenarioTest
{
    private WebApplicationFactory<Program> app;

    private AddThreadCommand Thread => new AddThreadCommand()
    {
        Name = "test",
        Description = "this is description, hello"
    };

    private AddPostCommand Post => new AddPostCommand()
    {
        PostName = "test",
        Description = "this is description, hello",
        ThreadName = Thread.Name,
        UserName = "test user"
    };

    [Fact]
    public async Task AddThreadTest()
    {
        CreateBackgroundApp();
        var client = app.CreateClient();
        await Add(Thread); 
        var result = await client.GetAsync("api/thread");
        var resultMessage = await result.Content.ReadAsStringAsync(); 
        var listOdThreads = JsonConvert.DeserializeObject<List<GetThreadByIdResponse>>(resultMessage);
        Assert.NotNull(listOdThreads);
        Assert.Contains(listOdThreads, w => w.Name == Thread.Name && w.Description == Thread.Description); 
    }

    [Fact]
    public async Task AddPostTest()
    {
        CreateBackgroundApp();
        var client = app.CreateClient();
        await Add(Thread);
        await Add(Post);
        var result = await client.GetAsync("api/post");
        var resultMessage = await result.Content.ReadAsStringAsync();
        var listOdThreads = JsonConvert.DeserializeObject<List<GetPostResponse>>(resultMessage);
        Assert.NotNull(listOdThreads);
        Assert.Contains(listOdThreads, w => w.PostName == Post.PostName && w.Description == Thread.Description);
    }

    private async Task Add<T>(T t)
    {
        var serviceSender = app.Services.GetService<ISenderMessage>();
        var wrapThread = new DataWrapper<T>()
        {
            Data = t
        };
        var settings = new JsonSerializerSettings();
        settings.ContractResolver = new LowercaseContractResolver();
        var message = JsonConvert.SerializeObject(wrapThread, settings);
        await serviceSender.SendAsync(message);
    }

    private void CreateBackgroundApp()
    {
        if (app == null)
        {
            app = new WebApplicationFactory<Program>()
                        .WithWebHostBuilder(builder =>
                        {
                            builder.ConfigureServices(services =>
                            {
                                var descriptor = services.SingleOrDefault(
                                d => d.ServiceType == typeof(DbContextOptions<InventoryWriteContext>));

                                services.Remove(descriptor);

                                services.AddDbContext<InventoryWriteContext>(options =>
                                {
                                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                                });
                            });
                        });
        }
        var context = app.Services.GetService<InventoryWriteContext>();
        context.Database.EnsureDeleted(); 
    }

    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return $"{char.ToLower(propertyName[0])}{propertyName[1..]}";
        }
    }
    private class DataWrapper<T>
    {
        public T Data { get; set; }
    }
}