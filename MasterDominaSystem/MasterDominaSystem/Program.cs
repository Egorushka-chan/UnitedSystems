using MasterDominaSystem.BLL;
using MasterDominaSystem.DAL;
using MasterDominaSystem.RMQL;
using MasterDominaSystem.GRPC;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();

string connectionString = builder.Configuration["ConnectionString:Postgres"] ?? throw new InvalidCastException("Cannot start without valid connection string");
connectionString = connectionString.Replace("###", "mdm");

// Подтягиваем библиотеки
builder.Services
    .InjectBLL()
    .InjectDAL(connectionString);

builder.InjectRMQL("RabbitMQ")
    .InjectGRPC();

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