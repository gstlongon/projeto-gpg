using Core.DTOs;
using Core.Models;

namespace Core.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(string name, string email, string phoneNumber, string password, string number, string street, string city, string state, string postalcode, string country);
        Task<User> GetUserOrThrowException(string userId);
        Task<User> UpdateUser(string userId, string? name, string? email, string? phoneNumber, string? password, string street, string number, string city, string state, string postalcode, string country);
        Task DeleteUser(string userId);

    }
}
