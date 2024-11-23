namespace ApiCadastro.Repositories
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using ApiCadastro.Models;

    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
    }

}
