using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ScentifyWebApp.Models.Actors;
using ScentifyWebApp.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ScentifyWebApp.Services.Implementations
{
    public class AppTokenService : IAppTokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppTokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public byte[] GenerateRandomBytes()
        {
            var key = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }

        public async Task SignInAsync(AppUser appUser, int expireMinutes)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName, appUser.FullName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, appUser.Username),
                new Claim(ClaimTypes.Role, appUser.RoleName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = true, // You can set this to false if you want a session cookie
                ExpiresUtc = DateTime.UtcNow.AddMinutes(expireMinutes) // Set the expiration time for the cookie
            };

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                authenticationProperties);

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"));
            //var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var token = new JwtSecurityToken(
            //    "https://yourauthorizationserver.com",
            //    "https://yourapiresource.com",
            //    claims,
            //    expires: DateTime.Now.AddMinutes(30),
            //    signingCredentials: credentials
            //);
            //return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}