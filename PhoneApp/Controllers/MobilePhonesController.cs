using Microsoft.AspNetCore.Mvc;
using PhoneApp.Models;

namespace PhoneApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MobilePhonesController : ControllerBase
{
    private static readonly MobilePhone[] Phones =
    {
        new MobilePhone { Id = 1, Model = "iPhone 15", Manufacturer = "Apple", Price = 999 },
        new MobilePhone { Id = 2, Model = "Galaxy S24", Manufacturer = "Samsung", Price = 899 },
        new MobilePhone { Id = 3, Model = "Xiaomi 14", Manufacturer = "Xiaomi", Price = 699 }
    };

    [HttpGet]
    public ActionResult<IEnumerable<MobilePhone>> GetAllPhones()
    {
        return Ok(Phones);
    }

    [HttpGet("{id}")]
    public ActionResult<MobilePhone> GetPhone(int id)
    {
        var phone = Phones.FirstOrDefault(p => p.Id == id);

        if (phone == null)
            return NotFound();

        return Ok(phone);
    }
}