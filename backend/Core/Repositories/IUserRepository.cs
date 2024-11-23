using Core.Models;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task DeleteUser(User user);
        Task<User?> GetUser(string userId);
        Task UpdateUser(User user);

    }
}
