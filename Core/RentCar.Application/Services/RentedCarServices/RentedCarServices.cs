using Microsoft.EntityFrameworkCore;
using RentCar.Application.Dtos.RentedCarDtos;
using RentCar.Domain.Entities;
using RentCar.Persistence.Repositories.CarRepositories;
using RentCar.Persistence.Repositories.RentedCarRepositories;
using RentCar.Persistence.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Services.RentedCarServices
{
    public class RentedCarServices : IRentedCarServices
    {
        private readonly IRentedCarRepository _repository;
        private readonly ICarRepository _carRepository;
        private readonly IUserRepository _userRepository;

        public RentedCarServices(IRentedCarRepository repository, ICarRepository carRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _carRepository = carRepository;
            _userRepository = userRepository;
        }

        public async Task CreateRentedCar(CreateRentedCarDto dto)
        {
            var rentedCar = new RentedCar
            {
                UserId=dto.UserId,
                CarId = dto.CarId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                TotalPrice = dto.TotalPrice,
                DamagePrice = dto.DamagePrice,
                isCompleted = dto.isCompleted

            };
            await _repository.CreateRentedCarAsync(rentedCar);
        }

        public async Task DeleteRentedCar(int id)
        {
           var value = await _repository.GetByIdRentedCarAsync(id);
            await _repository.DeleteRentedCarAsync(value);
        }

        public async Task<List<ResultRentedCarDto>> GetAllRentedCars()
        {
            var value = await _repository.GetAllRentedCarsAsync();
            var users = await _userRepository.GetAllUsersAsync();
            var cars = await _carRepository.GetAllCarsAsync();
            var result=new List<ResultRentedCarDto>();
            //var result = rentedCars.Select(x=>new ResultRentedCarDto
            //{
            //    UserId = x.UserId,
            //    CarId = x.CarId,
            //    StartDate = x.StartDate,
            //    EndDate = x.EndDate,
            //    TotalPrice = x.TotalPrice,
            //    DamagePrice = x.DamagePrice,
            //    isCompleted = x.isCompleted
            //}).ToList();
            foreach (var rentedCar in value)
            {
                var user=await _userRepository.GetByIdUserAsync(rentedCar.UserId);
                var car=await _carRepository.GetByIdCarAsync(rentedCar.CarId);
                var newRentedCar=new ResultRentedCarDto
                {
                    UserId = rentedCar.UserId,
                    CarId = rentedCar.CarId,
                    StartDate = rentedCar.StartDate,
                    EndDate = rentedCar.EndDate,
                    TotalPrice = rentedCar.TotalPrice,
                    DamagePrice = rentedCar.DamagePrice,
                    isCompleted = rentedCar.isCompleted
                };
                //newRentedCar.User = user;
                newRentedCar.Car = car;
                result.Add(newRentedCar);
            }
            return result;
        }

        public async Task<GetByIdRentedCarDto> GetByIdRentedCar(int id)
        {
            var value = await _repository.GetByIdRentedCarAsync(id);
            var result = new GetByIdRentedCarDto
            {
                Id = value.Id,
                UserId = value.UserId,
                CarId = value.CarId,
                StartDate = value.StartDate,
                EndDate = value.EndDate,
                TotalPrice = value.TotalPrice,
                DamagePrice = value.DamagePrice,
                isCompleted = value.isCompleted
            };
            return result;
        }

        public async Task UpdateRentedCar(UpdateRentedCarDto dto)
        {
            var value=await _repository.GetByIdRentedCarAsync(dto.Id);
            value.UserId = dto.UserId;
            value.CarId = dto.CarId;
            value.StartDate = dto.StartDate;
            value.EndDate = dto.EndDate;
            value.TotalPrice = dto.TotalPrice;
            value.DamagePrice = dto.DamagePrice;
            value.isCompleted = dto.isCompleted;

            await _repository.UpdateRentedCarAsync(value);
        }
    }
}
