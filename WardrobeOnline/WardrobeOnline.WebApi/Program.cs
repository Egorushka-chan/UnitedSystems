using Serilog;

using UnitedSystems.EventBus.RabbitMQ;

using WardrobeOnline.BLL;
using WardrobeOnline.BLL.Models.Settings;
using WardrobeOnline.DAL;
using WardrobeOnline.WebApi.Settings;
using WardrobeOnline.GRPC;
using System.Net;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Связь с остальными слоями

string? connectionString = builder.Configuration["ConnectionString:Postgres"]?.Replace("###","wardrobe");

builder.Services.Configure<ImageSetting>(
    builder.Configuration.GetSection("ImageSetting"));

builder.Services.Configure<RedisSetting>(
    builder.Configuration.GetSection("RedisSetting"));

builder.Services.AddDataLayer(connectionString);

AddBusinessLayer(builder);

builder.Services.InjectGRPC();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});


builder.AddRabbitMQEventBus("RabbitMQ");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel((context, serverOptions) => {
    serverOptions.Listen(IPAddress.Any, 8088, listenOptions => {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.Services.GetRequiredService<ILogger<Program>>().LogInformation(connectionString);

app.UseAuthorization();

app.MapControllers();
app.MapGRPC();

app.Run();

void AddBusinessLayer(WebApplicationBuilder builder)
{
    string configuration = builder.Configuration["RedisSetting:Configuration"] ?? new RedisSetting().Configuration;
    string packages = builder.Configuration["RedisSetting:InstanceName"] ?? new RedisSetting().InstanceName;
    builder.Services.AddBusinessLayer(new RedisSetting() {
        Configuration = configuration,
        InstanceName = packages
    });
}