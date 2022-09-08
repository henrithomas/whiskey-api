using Microsoft.OpenApi.Models;
using WhiskeyApi.Models;
using WhiskeyApi.API;
using WhiskeyApi.DataContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Whiskeys") ?? "Data Source=Whiskeys.db";
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// builder.Services.AddDbContext<WhiskeyDb>(opt => opt.UseInMemoryDatabase("Whiskey"));
builder.Services.AddSqlite<WhiskeyDb>(connectionString);
var app = builder.Build();

app.RegisterWhiskeyAPI();

app.Run();