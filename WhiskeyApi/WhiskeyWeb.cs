using WhiskeyApi.DataContext;
using WhiskeyApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WhiskeyApi.API
{
    public static class WhiskeyAPI
    {
        public static void RegisterWhiskeyAPI(this WebApplication app)
        {
            // app.MapGet(
            //     "/", 
            //     () => "Hello, World!"
            // ); 

            app.MapGet(
                "/whiskey", 
                async (WhiskeyDb db) =>
                await db.Whiskeys.Select(x => new WhiskeyDTO(x)).ToListAsync()
            );

            app.MapGet(
                "/whiskey/{id}", 
                async (int id, WhiskeyDb db) =>
                await db.Whiskeys.FindAsync(id)
                    is Whiskey whiskey
                        ? Results.Ok(new WhiskeyDTO(whiskey))
                        : Results.NotFound()
            );

            app.MapPost(
                "/whiskey", 
                async (WhiskeyDTO whiskeyDTO, WhiskeyDb db) =>
                {
                    var whiskey = new Whiskey
                    {
                        Name = whiskeyDTO.Name,
                        Country = whiskeyDTO.Country,
                        Aroma = whiskeyDTO.Aroma,
                        Taste = whiskeyDTO.Taste,
                        Finish = whiskeyDTO.Finish,
                        Percentage = whiskeyDTO.Percentage,
                        Cost = whiskeyDTO.Cost,
                        IsOpen = whiskeyDTO.IsOpen,
                        IsEmpty = whiskeyDTO.IsEmpty,
                        URL = whiskeyDTO.URL
                        
                    };

                    db.Whiskeys.Add(whiskey);
                    await db.SaveChangesAsync();

                    return Results.Created($"/whiskey/{whiskey.Id}", new WhiskeyDTO(whiskey));
                }
            );

            app.MapPut(
                "/whiskey/{id}", 
                async (int id, WhiskeyDTO whiskeyDTO, WhiskeyDb db) =>
                {
                    var whiskey = await db.Whiskeys.FindAsync(id);

                    if (whiskey is null) return Results.NotFound();

                    whiskey.Name = whiskeyDTO.Name;
                    whiskey.Country = whiskeyDTO.Country;
                    whiskey.Aroma = whiskeyDTO.Aroma;
                    whiskey.Taste = whiskeyDTO.Taste;
                    whiskey.Finish = whiskeyDTO.Finish;
                    whiskey.Percentage = whiskeyDTO.Percentage;
                    whiskey.Cost = whiskeyDTO.Cost;
                    whiskey.IsOpen = whiskeyDTO.IsOpen;
                    whiskey.IsEmpty = whiskeyDTO.IsEmpty;
                    whiskey.URL = whiskeyDTO.URL;

                    await db.SaveChangesAsync();

                    return Results.Ok(whiskeyDTO);
                }
            );

            app.MapDelete(
                "/whiskey/{id}", 
                async (int id, WhiskeyDb db) =>
                {
                    if (await db.Whiskeys.FindAsync(id) is Whiskey whiskey)
                    {
                        db.Whiskeys.Remove(whiskey);
                        await db.SaveChangesAsync();
                        return Results.Ok(new WhiskeyDTO(whiskey));
                    }

                    return Results.NotFound();
                }
            );
        }
    }
}