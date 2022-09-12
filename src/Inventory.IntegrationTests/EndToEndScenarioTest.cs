using Inventory.Application.Commands.AddPostCommand;
using Inventory.Application.Commands.AddThreadCommand;
using Inventory.Application.Commands.Comment.AddCommentCommand;
using Inventory.Application.Commands.Vote;
using Inventory.Application.Response;
using Inventory.Application.Responses;
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

    private AddThreadCommand AddThread => new ()
    {
        Name = "test",
        Description = "this is description, hello"
    };

    private AddPostCommand Post => new ()
    {
        PostName = "test",
        Description = "this is description, hello",
        ThreadName = AddThread.Name,
        UserName = "test user"
    };

    private AddCommentCommand Comment => new()
    {
        Text = "test",
        PostName = Post.PostName,
        UserName = Post.UserName
    };

    private AddVoteCommand Vote => new()
    {
        PostName = Post.PostName,
        UserName = Post.UserName,
        VoteType = "0"
    };

    [Fact]
    public async Task AddThreadTest()
    {
        CreateBackgroundApp();

        await SendMessageToConsumer(AddThread); 

        var listOdThreads = await Get<List<GetThreadByIdResponse>>("api/thread");

        Assert.NotNull(listOdThreads);
        Assert.Contains(listOdThreads, w => w.Name == AddThread.Name && w.Description == AddThread.Description); 
    }

    [Fact]
    public async Task AddPostTest()
    {
        CreateBackgroundApp();

        await SendMessageToConsumer(AddThread);
        await SendMessageToConsumer(Post);

        var posts = await Get<List<GetPostResponse>>("api/post");

        Assert.NotNull(posts);
        Assert.Contains(posts, w => w.PostName == Post.PostName && w.Description == Post.Description);
    }

    [Fact]
    public async Task AddVoteTest()
    {
        CreateBackgroundApp();

        await SendMessageToConsumer(AddThread);
        await SendMessageToConsumer(Post);
        await SendMessageToConsumer(Vote);

        var posts = await Get<List<GetPostResponse>>("api/post"); 

        Assert.NotNull(posts);
        Assert.Contains(posts, w => w.PostName == Post.PostName && w.Description == Post.Description && w.UpVotes == 1);
    }

    [Fact]
    public async Task AddCommentTest()
    {
        CreateBackgroundApp();

        await SendMessageToConsumer(AddThread);
        await SendMessageToConsumer(Post);
        await SendMessageToConsumer(Comment);

        var listOfComments = await Get<List<CommentResponse>>($"api/comment/by-user/{Comment.UserName}");

        Assert.NotNull(listOfComments);
        Assert.Contains(listOfComments, w => w.UserName == Comment.UserName && w.Text == Comment.Text);
    }

    private async Task<T> Get<T>(string uri)
    {
        var client = app.CreateClient();
        var result = await client.GetAsync(uri);
        var resultMessage = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(resultMessage);
    }

    private async Task SendMessageToConsumer<T>(T t)
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