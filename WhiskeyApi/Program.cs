using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<WhiskeyDb>(opt => opt.UseInMemoryDatabase("Whiskey"));
var app = builder.Build();

app.MapGet("/", () => "Hello, World!"); 

app.MapGet("/whiskey", async (WhiskeyDb db) =>
    await db.Whiskeys.Select(x => new WhiskeyDTO(x)).ToListAsync());

app.MapGet("/whiskey/{id}", async (int id, WhiskeyDb db) =>
    await db.Whiskeys.FindAsync(id)
        is Whiskey whiskey
            ? Results.Ok(new WhiskeyDTO(whiskey))
            : Results.NotFound());

app.MapPost("/whiskey", async (WhiskeyDTO whiskeyDTO, WhiskeyDb db) =>
{
    var whiskey = new Whiskey
    {
        IsOpen = whiskeyDTO.IsOpen,
        IsEmpty = whiskeyDTO.IsEmpty,
        Name = whiskeyDTO.Name
    };

    db.Whiskeys.Add(whiskey);
    await db.SaveChangesAsync();

    return Results.Created($"/whiskey/{whiskey.Id}", new WhiskeyDTO(whiskey));
});

app.MapPut("/whiskey/{id}", async (int id, WhiskeyDTO whiskeyDTO, WhiskeyDb db) =>
{
    var whiskey = await db.Whiskeys.FindAsync(id);

    if (whiskey is null) return Results.NotFound();

    whiskey.Name = whiskeyDTO.Name;
    whiskey.IsOpen = whiskeyDTO.IsOpen;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/whiskey/{id}", async (int id, WhiskeyDb db) =>
{
    if (await db.Whiskeys.FindAsync(id) is Whiskey whiskey)
    {
        db.Whiskeys.Remove(whiskey);
        await db.SaveChangesAsync();
        return Results.Ok(new WhiskeyDTO(whiskey));
    }

    return Results.NotFound();
});

app.Run();

public class Whiskey
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsOpen { get; set; }
    public bool IsEmpty { get; set; }
    public string? Secret { get; set; }
}

public class WhiskeyDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsOpen { get; set; }
    public bool IsEmpty { get; set; }
    public WhiskeyDTO() { }
    public WhiskeyDTO(Whiskey whiskey) =>
    (Id, Name, IsOpen, IsEmpty) = (whiskey.Id, whiskey.Name, whiskey.IsOpen, whiskey.IsEmpty);
}


class WhiskeyDb : DbContext
{
    public WhiskeyDb(DbContextOptions<WhiskeyDb> options)
        : base(options) { }

    public DbSet<Whiskey> Whiskeys => Set<Whiskey>();
}