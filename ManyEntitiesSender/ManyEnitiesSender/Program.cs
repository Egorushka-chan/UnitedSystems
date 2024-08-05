using ManyEntitiesSender.BLL;
using ManyEntitiesSender.BLL.Settings;
using ManyEntitiesSender.DAL;
using ManyEntitiesSender.Middleware;
using ManyEntitiesSender.PL.Settings;
using ManyEntitiesSender.RAL;

using UnitedSystems.EventBus.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddOptions(builder);

AddDataAccessLayer(builder);
AddRedisLayer(builder);
AddBusinessLayer(builder);

builder.AddRabbitMQEventBus("RabbitMQ");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.UseMyCachingValidation();
app.UseMyCaching();

app.MapControllers();

app.Run();


void AddDataAccessLayer(WebApplicationBuilder builder)
{
    string? connectionString = builder.Configuration["ConnectionString:Postgres"]?.Replace("###","package");
    if (string.IsNullOrEmpty(connectionString)) {
        connectionString = "Host=localhost;Port=3000;Database=packages;Username=root;Password=tobacco";
    }
    builder.Services.InjectDAL(connectionString);
}

void AddBusinessLayer(WebApplicationBuilder builder)
{
    builder.Services.InjectBLL();
}

void AddRedisLayer(WebApplicationBuilder builder)
{
    builder.Services.InjectRAL();
}


void AddOptions(WebApplicationBuilder builder)
{
    builder.Services.Configure<PackageSettings>(
        builder.Configuration.GetSection("PackageSettings"));

    builder.Services.Configure<RedisSettings>(
        builder.Configuration.GetSection("Redis"));

    // RabbitMQ
    builder.Services.Configure<BrokerSettings>(
        builder.Configuration.GetSection("Broker:RabbitMQ"));
}