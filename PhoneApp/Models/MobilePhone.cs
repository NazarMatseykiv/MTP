namespace PhoneApp.Models;

public class MobilePhone
{
    public int Id { get; set; }
    public string Model { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public decimal Price { get; set; }
}