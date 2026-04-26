using Dorm.DTOs;
using DormWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class SettlementsController : ControllerBase
{
    private readonly DormContext _context;

    public SettlementsController(DormContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SettlementDto>>> GetAll()
    {
        var data = await _context.Settlements
            .Include(x => x.Student)
            .Include(x => x.Room)
            .Select(x => new SettlementDto
            {
                Id = x.Id,
                StudentName = x.Student.FullName,
                RoomNumber = x.Room.RoomNumber,
                CheckInDate = x.CheckInDate,
                CheckOutDate = x.CheckOutDate
            })
            .ToListAsync();

        return Ok(data);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SettlementDto dto)
    {
        var entity = await _context.Settlements
            .Include(x => x.Student)
            .Include(x => x.Room)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return NotFound();

        entity.CheckOutDate = dto.CheckOutDate;

        await _context.SaveChangesAsync();
        return NoContent();
    }
}