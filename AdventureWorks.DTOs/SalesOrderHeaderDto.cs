namespace AdventureWorks.DTOs;

public class SalesOrderHeaderDto
{
    public int SalesOrderId { get; set; }
    public string? SalesOrderNumber { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal? TotalDue { get; set; }
    public string? Comment { get; set; }
    public string? PurchaseOrderNumber { get; set; }
}

