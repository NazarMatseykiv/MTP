namespace PhoneWebAPI.Models;

public class MobilePhone
{
    public int Id { get; set; }
    public string Model { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public decimal Price { get; set; }
}