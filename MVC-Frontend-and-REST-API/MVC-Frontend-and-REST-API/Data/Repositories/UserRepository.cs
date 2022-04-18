using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MVC_Frontend_and_REST_API.Helperclasses;
using MVC_Frontend_and_REST_API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Data.Repositories
{
    public class UserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public UserRepository(UserManager<IdentityUser> userManager, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        public async Task<bool> RegisterAsync(IdentityUser identityUser, string password)
        {
            IdentityResult identityResult = await _userManager.CreateAsync(identityUser, password);
            await _userManager.AddToRoleAsync(identityUser, "User");
            //No confirmation needed for add to role, as the user gets issued a role whenever the user tries to access a role-locked feature while not having a role but having a token.

            if (!identityResult.Succeeded) return false;
            return true;
        }

        public async Task<IdentityUser> Search(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel loginRequest)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(loginRequest.Email);
            //LoginModel login = new LoginModel();

            if (await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                string token = GenerateToken(user);
                return new LoginResponseModel { LoggedIn = true, Token = token, Username = user.UserName };
            }

            return new LoginResponseModel { LoggedIn = false };
        }

        public string GenerateToken(IdentityUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("Id", user.Id),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
