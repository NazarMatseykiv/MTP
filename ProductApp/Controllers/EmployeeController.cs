using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApp.Data;
using ProductApp.Models;

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employee.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            _context.Employee.Add(employee);

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            _context.Entry(employee).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
                return NotFound();

            _context.Employee.Remove(employee);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}