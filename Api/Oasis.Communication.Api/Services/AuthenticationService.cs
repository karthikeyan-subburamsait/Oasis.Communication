using Microsoft.IdentityModel.Tokens;
using Oasis.Communication.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Oasis.Communication.Api.Services
{
    public interface IAuthenticationService
    {
        public Task<bool> HasUserExists(string username, string password);
        public Task<string> GetAccessToken(string username, string password);
        public string ValidateAccessToken(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        public readonly string userNameFromDB = "karthik";
        public readonly string passwordFromDB = "karthik123";
        private readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GetAccessToken(string username, string password)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, username),
                                new Claim(ClaimTypes.NameIdentifier, password),
                            };

            foreach (var role in UserRoles.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public Task<bool> HasUserExists(string username, string password)
        {
            return Task.FromResult(username.Equals(userNameFromDB, StringComparison.OrdinalIgnoreCase) && password.Equals(password, StringComparison.OrdinalIgnoreCase));
        }

        public string ValidateAccessToken(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
