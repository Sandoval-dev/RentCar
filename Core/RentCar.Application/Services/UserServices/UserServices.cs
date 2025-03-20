using RentCar.Application.Dtos.UserDtos;
using RentCar.Domain.Entities;
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

        public UserServices(IUserRepository repository)
        {
            _repository = repository;
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
            var result = users.Select(x => new ResultUserDto
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                Phone = x.Phone,
                Password = x.Password,
                Role = x.Role,
                RentedCars = new List<RentedCar>()
            }).ToList();
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
