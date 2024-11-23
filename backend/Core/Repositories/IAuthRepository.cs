using Core.Models;

namespace Core.Repositories
{
    public interface IAuthRepository
    {

        public Task<User?> GetUserByEmailAndPassword(string email, string password);

    }
}
