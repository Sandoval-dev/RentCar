using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Dtos.UserDtos;
using RentCar.Application.Services.UserServices;

namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _services;

        public UserController(IUserServices services)
        {
            _services = services;
        }

        [HttpGet("getallusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _services.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("getbyiduser")]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            var result = await _services.GetByIdUser(id);
            return Ok(result);
        }

        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            await _services.CreateUser(dto);
            return Ok("User created");
        }

        [HttpPut("updateuser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dto)
        {
            await _services.UpdateUser(dto);
            return Ok("User updated");
        }

        [HttpDelete("deleteuser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _services.DeleteUser(id);
            return Ok("User deleted");
        }
    }
}
