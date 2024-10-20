using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        _userService.CreateUser(user);
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        if (id != user.Id) return BadRequest();
        _userService.UpdateUser(user);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        _userService.DeleteUser(id);
        return Ok();
    }
}