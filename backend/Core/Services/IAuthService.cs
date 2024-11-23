using System.Security.Claims;

namespace Core.Services
{
    public interface IAuthService
    {

        public Task<string> SignIn(string email, string password);

        public string? GetAuthenticatedUserId(ClaimsPrincipal User);

    }
}
