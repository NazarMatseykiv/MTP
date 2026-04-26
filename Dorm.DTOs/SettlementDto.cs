namespace Dorm.DTOs;

public class SettlementDto
{
    public int Id { get; set; }
    public string StudentName { get; set; }
    public string RoomNumber { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
}