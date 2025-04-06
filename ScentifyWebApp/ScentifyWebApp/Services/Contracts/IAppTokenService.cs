using ScentifyWebApp.Models.Actors;

namespace ScentifyWebApp.Services.Contracts
{
    public interface IAppTokenService
    {
        byte[] GenerateRandomBytes();

        Task SignInAsync(AppUser appUser, int expireMinutes);

        Task Logout();
    }
}
