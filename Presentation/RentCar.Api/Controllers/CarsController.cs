﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Dtos.CarDtos;
using RentCar.Application.Services.CarServices;

namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarServices _services;

        public CarsController(ICarServices services)
        {
            _services = services;
        }

        [HttpGet("getallcars")]
        public async Task<IActionResult> GetAllCars() 
        {
            var result = await _services.GetAllCars();
            return Ok(result);
        }

        [HttpGet("getbyidcar")]
        public async Task<IActionResult> GetByIdCar(int id)
        {
            var result=await _services.GetByIdCar(id);
            return Ok(result);
        }

        [HttpPost("createcar")]
        public async Task<IActionResult> CreateCar(CreateCarDto dto)
        {
            await _services.CreateCar(dto);
            return Ok("Car created");
        }

        [HttpPut("updatecar")]
        public async Task<IActionResult> UpdateCar(UpdateCarDto dto)
        {
            await _services.UpdateCar(dto);
            return Ok("Car updated");
        }

        [HttpDelete("deletecar")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            await _services.DeleteCar(id);
            return Ok("Car deleted");
        }

    }
}
