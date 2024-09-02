using System.Net;

using Microsoft.EntityFrameworkCore;

using Serilog;

using UnitedSystems.EventBus.RabbitMQ;
using UnitedSystems.EventBus.Kafka;

using WardrobeOnline.BLL;
using WardrobeOnline.BLL.Models.Settings;
using WardrobeOnline.DAL;
using WardrobeOnline.DAL.Interfaces;
using WardrobeOnline.GRPC;
using WardrobeOnline.WebApi.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Связь с остальными слоями

string connectionString = builder.Configuration["ConnectionString:Postgres"]?.Replace("###","wardrobe") ??"Host=localhost;Port=3000;Database=###;Username=root;Password=tobacco;";

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

if (IsPreferRabbitMQ(builder)) {
    builder.AddRabbitMQEventBus("RabbitMQ");
}
else {
    builder.AddKafkaEventBus();
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureKestrel(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.Services.GetRequiredService<ILogger<Program>>().LogInformation(connectionString);

app.UseAuthorization();

app.MapControllers();
app.MapGRPC();

await SeedDBIfEmpty(app);

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

async Task SeedDBIfEmpty(WebApplication app)
{
    IServiceScopeFactory scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using(var scope = app.Services.CreateScope())
    {
        IWardrobeContext context = scope.ServiceProvider.GetRequiredService<IWardrobeContext>();
        int value = await context.Persons.CountAsync() + await context.Clothes.CountAsync();
        if(value == 0)
        {
            IDBSeeder seeder = scope.ServiceProvider.GetRequiredService<IDBSeeder>();
            await seeder.Seed();
        }
    }
    
}

bool IsPreferRabbitMQ(WebApplicationBuilder builder)
{
    string errorMessage = "PreferRabbitMQOverKafka must be 1,0,true,false";
    bool preferRabbit = false;

    string? value = builder.Configuration["PreferRabbitMQOverKafka"];
    if(value != null) {
        bool converted = int.TryParse(value, out int number);
        if (converted) {
            switch (number) {
                case 0:
                    preferRabbit = false;
                    break;
                case 1:
                    preferRabbit = true;
                    break;
                default:
                    throw new InvalidOperationException(errorMessage);
            }
        }
        else {
            converted = bool.TryParse(value, out bool second);
            if (converted) {
                preferRabbit = second;
            }
            else {
                throw new InvalidOperationException(errorMessage);
            }
        }
    }

    return preferRabbit;
}

static void ConfigureKestrel(WebApplicationBuilder builder)
{
    builder.WebHost.ConfigureKestrel((context, serverOptions) => {
        serverOptions.Listen(IPAddress.Any, 8088, listenOptions => {
            listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1;
        });
        serverOptions.Listen(IPAddress.Any, 8089, listenOptions => {
            listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
        });
    });
}