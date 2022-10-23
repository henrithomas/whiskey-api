namespace Whiskey.Data.Models;

public class WhiskeyEntity : Spiritus
{
    public string? Aroma { get; set; }
    public string? Taste { get; set; }
    public string? Finish { get; set; }
}

public class WhiskeyDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? Aroma { get; set; }
    public string? Taste { get; set; }
    public string? Finish { get; set; }
    public float Percentage { get; set; }
    public float Cost { get; set; }
    public bool IsOpen { get; set; }
    public bool IsEmpty { get; set; }
    public string? URL { get; set; }
    public WhiskeyDTO() { }
    public WhiskeyDTO(WhiskeyEntity whiskey) =>
        (Id, Name, Country, Aroma, Taste, Finish, Percentage, Cost, IsOpen, IsEmpty, URL) = 
        (
            whiskey.Id, 
            whiskey.Name, 
            whiskey.Country, 
            whiskey.Aroma, 
            whiskey.Taste, 
            whiskey.Finish, 
            whiskey.Percentage, 
            whiskey.Cost, 
            whiskey.IsOpen, 
            whiskey.IsEmpty, 
            whiskey.URL
        );
}
