using Inventory.Application;
using Inventory.Consumer;
using Inventory.WebApi;
using Inventory.Infractracture.DbConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using System.Reflection;
using Inventory.Infractracture.DbConfiguration.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureWebApi();
builder.Services.InitializeInfrasctracture(builder.Configuration);

builder.Services.ConfigureConsumer(builder.Configuration);
builder.Services.ConfigureApplication();
builder.Services.InitializeInfrasctracture(builder.Configuration);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = false);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var servicesProvider = builder.Services.BuildServiceProvider();
var context = servicesProvider.GetRequiredService<InventoryWriteContext>();
context.Database.Migrate(); 

app.Run();


