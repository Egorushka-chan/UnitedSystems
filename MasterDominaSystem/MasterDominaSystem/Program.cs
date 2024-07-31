using MasterDominaSystem.RMQL.Models.Settings;

using MasterDominaSystem.BLL;
using MasterDominaSystem.RMQL;
using MasterDominaSystem.DAL;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

AddOptions(builder);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration["ConnectionString:Postgres"] ?? throw new InvalidCastException("Cannot start without valid connection string");
connectionString = connectionString.Replace("###", "wardrobe");

// Подтягиваем библиотеки
builder.Services
    .InjectBLL()
    .InjectRMQL()
    .InjectDAL(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddOptions(WebApplicationBuilder builder)
{
    builder.Services.Configure<BrokerSettings>(
        builder.Configuration.GetSection("Broker:RabbitMQ"));
}