using Core.Models;
using Core.Repositories;
using Core.Services;
using Core.DTOs;
using System.Threading.Tasks;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        private async Task<User?> GetUser(string userId)
        {
            return await _userRepository.GetUser(userId);
        }

        public async Task<User> GetUserOrThrowException(string userId)
        {
            User? user = await GetUser(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return user;
        }

        public async Task<User> CreateUser(string name, string email, string phoneNumber, string password, string street, string number, string city, string state, string postalcode, string country)
        {
           

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber,
                Password = password,
                Street = street,
                Number = number,
                City = city,
                State = state,
                PostalCode = postalcode,
                Country = country,
            };

            await _userRepository.AddUser(user);

            return user;
        }

        public async Task<User> UpdateUser(string userId, string? name, string? email, string? phoneNumber, string? password,string street, string number, string city, string state, string postalcode, string country)

        {
            var user = await GetUserOrThrowException(userId);

            user.Name = name;
            user.Email = email;
            user.PhoneNumber = phoneNumber;
            user.Password = password;
            user.Street = street;
            user.Number = number;
            user.City = city;
            user.State = state;
            user.PostalCode = postalcode;
            user.Country = country;

            await _userRepository.UpdateUser(user);

            return user;
        }


        public async Task DeleteUser(string userId)
        {
            var user = await GetUserOrThrowException(userId);

            await _userRepository.DeleteUser(user);
        }

        public Task<User> CreateUser(string name, string email, string phoneNumber, string password)
        {
            throw new NotImplementedException();
        }
        
    }
}
