using Serilog;

using UnitedSystems.EventBus.RabbitMQ;

using WardrobeOnline.BLL;
using WardrobeOnline.BLL.Models.Settings;
using WardrobeOnline.DAL;
using WardrobeOnline.WebApi.Settings;
using WardrobeOnline.GRPC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Связь с остальными слоями

string? connectionString = builder.Configuration["ConnectionString:Postgres"]?.Replace("###","wardrobe");

builder.Services.Configure<ImageSetting>(
    builder.Configuration.GetSection("ImageSetting"));

builder.Services.Configure<RedisSetting>(
    builder.Configuration.GetSection("RedisSetting"));

builder.Services.AddDataLayer(connectionString);
builder.Services.AddBusinessLayer(new ImageSetting() {
    Path = builder.Configuration["ImageSetting:Path"],
    Type = builder.Configuration["ImageSetting:Type"]
}, new RedisSetting() {
    Configuration = builder.Configuration["RedisSetting:Configuration"],
    InstanceName = builder.Configuration["RedisSetting:InstanceName"]
});

builder.Services.InjectGRPC();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.AddRabbitMQEventBus("RabbitMQ");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.Services.GetRequiredService<ILogger<Program>>().LogInformation(connectionString);

app.UseAuthorization();

app.MapControllers();
app.MapGRPC();

app.Run();