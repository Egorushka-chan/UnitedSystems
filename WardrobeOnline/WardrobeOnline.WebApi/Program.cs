using Serilog;

using WardrobeOnline.BLL;
using WardrobeOnline.BLL.Models.Settings;
using WardrobeOnline.DAL;
using WardrobeOnline.WebApi.Settings;
using WardrobeOnline.BPL;
using WardrobeOnline.BPL.Abstractions;
using UnitedSystems.CommonLibrary.Models.WardrobeOnline.Messages.Headers;

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

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.InjectBPL();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.Services.GetRequiredService<ILogger<Program>>().LogInformation(connectionString);

//if (app.Configuration["ImageSetting:Type"] == "local")
//{
//    string? path = app.Configuration["ImageSetting:Path"];
//    if (!Path.Exists(path))
//    {
//        path = Path.Combine(Directory.GetCurrentDirectory(), "Images");
//    }

    //app.UseStaticFiles(new StaticFileOptions()
    //{
    //    FileProvider = new PhysicalFileProvider(path),
    //    RequestPath = new PathString("/images"),
    //    ServeUnknownFileTypes = true
    //});
//}

app.UseAuthorization();

app.MapControllers();

IMDMSender broker = app.Services.GetRequiredService<IMDMSender>();
broker.Send("Приложение запущено", MessageHeaderFromWO.AppStarting);

app.Run();

broker.Send("Приложение остановлено", MessageHeaderFromWO.AppClose);