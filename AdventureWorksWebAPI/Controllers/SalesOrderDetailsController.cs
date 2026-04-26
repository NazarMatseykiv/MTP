using AdventureWorks.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesOrderDetailsController : ControllerBase
{
    private readonly AdventureWorksDbContext _context;

    public SalesOrderDetailsController(AdventureWorksDbContext context)
    {
        _context = context;
    }

    [HttpGet("by-order/{salesOrderId}")]
    public async Task<ActionResult<IEnumerable<SalesOrderDetailDto>>> GetByOrderId(int salesOrderId)
    {
        var details = await _context.SalesOrderDetails
            .Where(x => x.SalesOrderId == salesOrderId)
            .OrderBy(x => x.SalesOrderDetailId)
            .Select(x => new SalesOrderDetailDto
            {
                SalesOrderId = x.SalesOrderId,
                SalesOrderDetailId = x.SalesOrderDetailId,
                OrderQty = x.OrderQty,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                UnitPriceDiscount = x.UnitPriceDiscount,
                LineTotal = x.LineTotal
            })
            .ToListAsync();

        return Ok(details);
    }
}

