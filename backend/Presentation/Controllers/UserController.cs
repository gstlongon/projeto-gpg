using Core.Models;
using Core.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.IO;
using System.Xml.Linq;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            try
            {
                var user = await _userService.GetUserOrThrowException(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return BadRequest(new { message = "The userDTO field is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userService.CreateUser(userDTO.Name, userDTO.Email, userDTO.PhoneNumber, userDTO.Password, userDTO.Street, userDTO.Number, userDTO.City, userDTO.State, userDTO.PostalCode, userDTO.Country);
                return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserDTO userDTO)
        {
            try
            {
                var updatedUser = await _userService.UpdateUser(
                    userId,
                    userDTO.Name,
                    userDTO.Email,
                    userDTO.PhoneNumber,
                    userDTO.Password,
                    userDTO.Street,
                    userDTO.Number,
                    userDTO.City,
                    userDTO.State,
                    userDTO.PostalCode,
                    userDTO.Country
                );
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }



        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
