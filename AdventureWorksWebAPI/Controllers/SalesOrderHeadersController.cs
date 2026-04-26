using AdventureWorks.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesOrderHeadersController : ControllerBase
{
    private readonly AdventureWorksDbContext _context;

    public SalesOrderHeadersController(AdventureWorksDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalesOrderHeaderDto>>> GetAll()
    {
        var orders = await _context.SalesOrderHeaders
            .OrderBy(x => x.SalesOrderId)
            .Select(x => new SalesOrderHeaderDto
            {
                SalesOrderId = x.SalesOrderId,
                SalesOrderNumber = x.SalesOrderNumber,
                OrderDate = x.OrderDate,
                TotalDue = x.TotalDue,
                Comment = x.Comment,
                PurchaseOrderNumber = x.PurchaseOrderNumber
            })
            .ToListAsync();

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SalesOrderHeaderDto>> GetById(int id)
    {
        var order = await _context.SalesOrderHeaders
            .Where(x => x.SalesOrderId == id)
            .Select(x => new SalesOrderHeaderDto
            {
                SalesOrderId = x.SalesOrderId,
                SalesOrderNumber = x.SalesOrderNumber,
                OrderDate = x.OrderDate,
                TotalDue = x.TotalDue,
                Comment = x.Comment,
                PurchaseOrderNumber = x.PurchaseOrderNumber
            })
            .FirstOrDefaultAsync();

        if (order == null)
            return NotFound();

        return Ok(order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SalesOrderHeaderDto dto)
    {
        var order = await _context.SalesOrderHeaders.FindAsync(id);

        if (order == null)
            return NotFound();

        order.Comment = dto.Comment;
        order.PurchaseOrderNumber = dto.PurchaseOrderNumber;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}

