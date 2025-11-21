using JugendApp.Api.Identity;

namespace JugendApp.Api.Auth
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(ApplicationUser user);
    }

}
