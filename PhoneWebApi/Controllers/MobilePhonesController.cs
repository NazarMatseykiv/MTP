using Microsoft.AspNetCore.Mvc;
using PhoneWebAPI.Models;

namespace PhoneWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MobilePhonesController : ControllerBase
{
    private static List<MobilePhone> phones = new List<MobilePhone>
    {
        new MobilePhone { Id = 1, Model = "iPhone 15", Manufacturer = "Apple", Price = 999 },
        new MobilePhone { Id = 2, Model = "Galaxy S24", Manufacturer = "Samsung", Price = 899 }
    };

    [HttpGet]
    public ActionResult<IEnumerable<MobilePhone>> GetPhones()
    {
        return Ok(phones);
    }

    [HttpGet("{id}")]
    public ActionResult<MobilePhone> GetPhone(int id)
    {
        var phone = phones.FirstOrDefault(p => p.Id == id);

        if (phone == null)
            return NotFound();

        return Ok(phone);
    }

    [HttpPost]
    public ActionResult<MobilePhone> AddPhone(MobilePhone phone)
    {
        phones.Add(phone);
        return Ok(phone);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePhone(int id, MobilePhone updatedPhone)
    {
        var phone = phones.FirstOrDefault(p => p.Id == id);

        if (phone == null)
            return NotFound();

        phone.Model = updatedPhone.Model;
        phone.Manufacturer = updatedPhone.Manufacturer;
        phone.Price = updatedPhone.Price;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePhone(int id)
    {
        var phone = phones.FirstOrDefault(p => p.Id == id);

        if (phone == null)
            return NotFound();

        phones.Remove(phone);

        return NoContent();
    }
}