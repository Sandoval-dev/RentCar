using RentCar.Application.Dtos.RentedCarDtos;
using RentCar.Application.Dtos.UserDtos;
using RentCar.Domain.Entities;
using RentCar.Persistence.Repositories.CarRepositories;
using RentCar.Persistence.Repositories.RentedCarRepositories;
using RentCar.Persistence.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _repository;
        private readonly IRentedCarRepository _rentedCarRepository;
        private readonly ICarRepository _carRepository;

        public UserServices(IUserRepository repository, IRentedCarRepository rentedCarRepository, ICarRepository carRepository)
        {
            _repository = repository;
            _rentedCarRepository = rentedCarRepository;
            _carRepository = carRepository;
        }
        public async Task CreateUser(CreateUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Phone = dto.Phone,
                Password = dto.Password,
                Role = dto.Role,
                RentedCars= new List<RentedCar>()
            };
            await _repository.CreateUserAsync(user);
        }

        public async Task DeleteUser(int id)
        {
            var value= await _repository.GetByIdUserAsync(id);
            await _repository.DeleteUserAsync(value);
        }

        public async Task<List<ResultUserDto>> GetAllUsers()
        {
            var users=await _repository.GetAllUsersAsync();
            var result = new List<ResultUserDto>();

            foreach (var user in users)
            {
                var newuser = new ResultUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Phone = user.Phone,
                    Password = user.Password,
                    Role = user.Role,

                };
                var usersrents=await _rentedCarRepository.GetRentedCarsByUserId(user.Id);
                var onlyinforent=new List<OnlyInfoRentedCarDto>();
                foreach (var userrent in usersrents)
                {

                    var newonlyinforent=new OnlyInfoRentedCarDto
                    {
                        Id = userrent.Id,
                        CarId = userrent.CarId,
                        StartDate = userrent.StartDate,
                        EndDate = userrent.EndDate,
                        TotalPrice = userrent.TotalPrice,
                        DamagePrice = userrent.DamagePrice,
                        isCompleted = userrent.isCompleted
                    };
                    newonlyinforent.Car = await _carRepository.GetByIdCarAsync(userrent.CarId);
                    onlyinforent.Add(newonlyinforent);
                }
                newuser.RentedCars = onlyinforent;
                result.Add(newuser);
            }
            return result;
        }

        public async Task<GetByIdUserDto> GetByIdUser(int id)
        {
            var value=await _repository.GetByIdUserAsync(id);
            var result = new GetByIdUserDto
            {
                Id = value.Id,
                Name = value.Name,
                Surname = value.Surname,
                Email = value.Email,
                Password = value.Password,
                Phone = value.Phone,
                Role = value.Role,
                RentedCars = new List<RentedCar>()
            };
            return result;
        }

        public async Task UpdateUser(UpdateUserDto dto)
        {
           var user=await _repository.GetByIdUserAsync(dto.Id);
            user.Name = dto.Name;
            user.Surname = dto.Surname;
            user.Email = dto.Email;
            user.Password = dto.Password;
            user.Phone = dto.Phone;
            user.Role = dto.Role;

            await _repository.UpdateUserAsync(user);

        }
    }
}
