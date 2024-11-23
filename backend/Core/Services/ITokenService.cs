using Core.Models;

namespace Core.Services
{
    public interface ITokenService
    {

        public string CreateUserToken(User user);

    }
}
