using RentCar.Application.Dtos.CarDtos;
using RentCar.Domain.Entities;
using RentCar.Persistence.Repositories.CarRepositories;

namespace RentCar.Application.Services.CarServices
{
    public class CarServices : ICarServices
    {
        private readonly ICarRepository _repository;

        public CarServices(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateCar(CreateCarDto dto)
        {
            var value = new Car
            {
                Fuel = dto.Fuel,
                DailyPrice = dto.DailyPrice,
                ImageUrl = dto.ImageUrl,
                IsAvailable = dto.IsAvailable,
                Brand = dto.Brand,
                KM = dto.KM,
                Model = dto.Model,
                Type = dto.Type,
                Year = dto.Year,
            };
            await _repository.CreateCarAsync(value);
        }

        public async Task DeleteCar(int id)
        {
            var value=await _repository.GetByIdCarAsync(id);
            await _repository.DeleteCarAsync(value);
        }

        public async Task<List<ResultCarDto>> GetAllCars()
        {
            var value = await _repository.GetAllCarsAsync();
            var result = value.Select(x => new ResultCarDto
            {
                Id = x.Id,
                Brand = x.Brand,
                Model = x.Model,
                DailyPrice = x.DailyPrice,
                Fuel = x.Fuel,
                ImageUrl = x.ImageUrl,
                IsAvailable = x.IsAvailable,
                KM = x.KM,
                Type = x.Type,
                Year = x.Year,
            }).ToList();
            return result;
        }

        public async Task<GetByIdCarDto> GetByIdCar(int id)
        {
            var value=await _repository.GetByIdCarAsync(id);
            var result = new GetByIdCarDto
            {
                Id=value.Id,
                Year=value.Year,
                Brand=value.Brand,
                DailyPrice=value.DailyPrice,
                Fuel=value.Fuel,
                ImageUrl=value.ImageUrl,
                KM = value.KM,
                Model = value.Model,
                Type = value.Type,
                IsAvailable=value.IsAvailable,
            };
            return result;
        }

        public async Task UpdateCar(UpdateCarDto dto)
        {
            var value = await _repository.GetByIdCarAsync(dto.Id);
            value.KM = dto.KM;
            value.Model = dto.Model;
            value.Type = dto.Type;
            value.Year = dto.Year;
            value.Brand = dto.Brand;
            value.Fuel= dto.Fuel;
            value.ImageUrl= dto.ImageUrl;
            value.IsAvailable=dto.IsAvailable;
            value.DailyPrice=dto.DailyPrice;

            await _repository.UpdateCarAsync(value);
        }
    }
}
