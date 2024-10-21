using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User loginModel)
    {

        var user = _userService.ValidateUser(loginModel.Email, loginModel.Password);

        if (user == null)
            return Unauthorized("Usuário ou senha inválidos.");

        return Ok(new
        {
            Message = "Login bem-sucedido",
            User = new
            {
                user.Id,
                user.Email,
                user.Role
            }
        });
    }
}