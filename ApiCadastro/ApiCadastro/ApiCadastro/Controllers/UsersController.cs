
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApiCadastro.Models;
using ApiCadastro.Services;

namespace ApiCadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            var result = await _userService.RegisterAsync(userDto);
            if (result == "Email já está em uso.")
                return BadRequest(result);

            return Ok(result);
        }

    
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserDto userDto)
        {
            var token = await _userService.AuthenticateAsync(userDto);
            if (token == null)
                return Unauthorized("Email ou senha incorretos.");

            return Ok(new { Token = token });
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            return Ok(user);
        }
    }
}
