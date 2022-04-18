using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVC_Frontend_and_REST_API.Helperclasses;
using MVC_Frontend_and_REST_API.Models.DataModels;
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
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly DataContext _dataContext;

        public UserRepository(UserManager<IdentityUser> userManager, JwtSettings jwtSettings, TokenValidationParameters tokenValidationParameters = null, DataContext dataContext = null)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _tokenValidationParameters = tokenValidationParameters;
            _dataContext = dataContext;
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
                //string refreshToken
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
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                //ExpirationDate = DateTime.UtcNow.AddMinutes(5)
                ExpirationDate = DateTime.UtcNow.AddMonths(6)
            };

            //await _dataContext.RefreshTokens.AddAsync(refreshToken);
            //save to db


            return tokenHandler.WriteToken(token);
        }

        public async Task<string> RefreshTokenAsync(RefreshTokenRequestModel refreshTokenRequest)
        {
            ClaimsPrincipal validatedToken = GetClaimsPrincipal(refreshTokenRequest.Token);
            IdentityUser user1 = await Search(refreshTokenRequest.Email);
            RefreshToken storedRefreshToken = await _dataContext.RefreshTokens.SingleOrDefaultAsync(x => x.UserId == user1.Id);

            if (validatedToken == null)
            {
                //invalid token error
            }

            var expirationDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expirationDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expirationDateUnix);//.Subtract(_jwtSettings.TokenLifetime);

            if(expirationDateUtc > DateTime.UtcNow)
            {
                //Token hasn't expired yet
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            //var storedRefreshToken = _dataContext.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken); //Needs to be an async fucntion

            if(storedRefreshToken == null)
            {
                //Refresh token doesn't exist error. Logout user
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpirationDate)
            {
                //Refresh token has expired. Logout user
            }

            if (storedRefreshToken.Invalidated)
            {
                //Refresh token has been invalidated. Logout user
            }

            if (storedRefreshToken.Used)
            {
                //Refresh token has been used. Logout user
            }

            if (storedRefreshToken.JwtId != jti)
            {
                //Refresh token doesn't match Jwt
            }

            storedRefreshToken.Used = true;
            _dataContext.RefreshTokens.Update(storedRefreshToken);
            await _dataContext.SaveChangesAsync();

            IdentityUser user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "Id").Value);
            return GenerateToken(user);
        }

        private ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
