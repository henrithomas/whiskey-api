using Microsoft.OpenApi.Models;
using WhiskeyApi.Models;
using WhiskeyApi.API;
using WhiskeyApi.DataContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Whiskeys") ?? "Data Source=Whiskeys.db";
builder.Services.AddRazorPages();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// builder.Services.AddDbContext<WhiskeyDb>(opt => opt.UseInMemoryDatabase("Whiskey"));
builder.Services.AddSqlite<WhiskeyDb>(connectionString);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();

app.RegisterWhiskeyAPI();

app.Run();