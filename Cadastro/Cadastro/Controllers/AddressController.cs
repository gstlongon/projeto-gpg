using Cadastro.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("{id}")]
    public IActionResult GetAddressById(int id)
    {
        var address = _addressService.GetAddressById(id);
        if (address == null) return NotFound();
        return Ok(address);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetAddressesByUserId(int userId)
    {
        var addresses = _addressService.GetAddressesByUserId(userId);
        return Ok(addresses);
    }

    [HttpPost]
    public IActionResult CreateAddress([FromBody] Address address)
    {
        _addressService.CreateAddress(address);
        return Ok(address);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAddress(int id, [FromBody] Address address)
    {
        if (id != address.Id) return BadRequest();
        _addressService.UpdateAddress(address);
        return Ok(address);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAddress(int id)
    {
        _addressService.DeleteAddress(id);
        return Ok();
    }
}