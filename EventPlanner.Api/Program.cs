using EventPlanner.Api.Services;
using EventPlanner.Api.Services.Interfaces;
using EventPlanner.Storage;
using EventPlanner.Storage.Repository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("LocalMySQL");
var serverVersion = new MySqlServerVersion(Microsoft.EntityFrameworkCore.ServerVersion.AutoDetect(connectionString));
builder.Services.AddDbContext<EventPlannerDbContext>(options =>
    options.UseMySql(connectionString, serverVersion, options=>options.MigrationsAssembly("EventPlanner.Storage")));

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});

builder.Services.AddMongoDB<AuditDBContext>(
    new MongoClient(builder.Configuration.GetConnectionString("MongolDb")), "EventPlanner");


builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ITaakService, TaakService>();
builder.Services.AddScoped<ILocatieService, LocatieService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IRapportService, RapportService>();

builder.Services.AddScoped<IOverzichtRapportRepository, OverzichtRapportRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
