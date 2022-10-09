namespace Whiskey.Data.Models;

public abstract class Spiritus
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Country { get; set; }
    public float Percentage { get; set; }
    public float Cost { get; set; }
    public bool IsOpen { get; set; }
    public bool IsEmpty { get; set; }
    public string? URL { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }

}