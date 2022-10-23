using Whiskey.Data.Repositories;
using Whiskey.Data.Contracts;
using Whiskey.API.Endpoints;
using Whiskey.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("whiskeyConnection"); 
builder.Services.AddRazorPages();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// builder.Services.AddDbContext<WhiskeyDb>(opt => opt.UseInMemoryDatabase("Whiskey"));
builder.Services.AddSqlite<WhiskeyDb>(connectionString);
// builder.Services.AddDbContext<WhiskeyDb>(options => {
//     options.UseSqlite(connectionString);
// });

// add repositories
builder.Services.AddScoped<IWhiskeyRepository, WhiskeyRepository>();
builder.Services.AddScoped<IGinRepository, GinRepository>();

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

app.MapWhiskeyAPI();
app.MapGinAPI();

app.Run();