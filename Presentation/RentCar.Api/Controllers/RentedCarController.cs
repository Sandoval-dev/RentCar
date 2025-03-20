using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Dtos.RentedCarDtos;
using RentCar.Application.Services.RentedCarServices;

namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentedCarController : ControllerBase
    {
        private readonly IRentedCarServices _services;

        public RentedCarController(IRentedCarServices services)
        {
            _services = services;
        }

        [HttpGet("getallrentedcars")]
        public async Task<IActionResult> GetAllCars()
        {
            var result= await _services.GetAllRentedCars();
            return Ok(result);
        }

        [HttpGet("getbyidrentedcar")]
        public async Task<IActionResult> GetByIdRentedCar(int id)
        {
            var result = await _services.GetByIdRentedCar(id);
            return Ok(result);
        }

        [HttpPost("createrentedcar")]
        public async Task<IActionResult> CreateRentedCar(CreateRentedCarDto dto)
        {
            await _services.CreateRentedCar(dto);
            return Ok("Rented car created");
        }

        [HttpPut("updaterentedcar")]
        public async Task<IActionResult> UpdateRentedCar(UpdateRentedCarDto dto)
        {
            await _services.UpdateRentedCar(dto);
            return Ok("Rented car updated");
        }

        [HttpDelete("deleterentedcar")]
        public async Task<IActionResult> DeleteRentedCar(int id)
        {
            await _services.DeleteRentedCar(id);
            return Ok("Rented car deleted");
        }
    }
}
