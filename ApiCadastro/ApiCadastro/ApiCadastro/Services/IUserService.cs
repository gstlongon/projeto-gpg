namespace ApiCadastro.Services
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using ApiCadastro.Models;

    public interface IUserService
    {
        Task<string> RegisterAsync(UserDto userDto);
        Task<string> AuthenticateAsync(UserDto userDto);
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
    }

}
