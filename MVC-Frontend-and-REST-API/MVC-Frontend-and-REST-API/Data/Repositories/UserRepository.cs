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

            if (await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                TokenGenerationResult token = await GenerateTokenAsync(user);
                return new LoginResponseModel { LoggedIn = true, Token = token.Token, RefreshToken = token.RefreshToken.Token.ToString(), Username = user.UserName };
            }

            return new LoginResponseModel { LoggedIn = false };
        }

        public async Task<TokenGenerationResult> GenerateTokenAsync(IdentityUser user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var expdate = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime);

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

            RefreshToken refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddMonths(6)
            };

            await _dataContext.RefreshTokens.AddAsync(refreshToken);
            _dataContext.SaveChanges();

            return new TokenGenerationResult { Token = tokenHandler.WriteToken(token), RefreshToken = refreshToken };
        }

        public async Task<RefreshTokenResponseModel> RefreshTokenAsync(RefreshTokenRequestModel refreshTokenRequest)
        {
            ClaimsPrincipal validatedToken = GetClaimsPrincipal(refreshTokenRequest.Token);

            var invalidTokenResponse = new RefreshTokenResponseModel
            {
                Message = "Invalid token",
                TerminateSession = true
            };

            if (validatedToken == null)
            {
                //invalid token error
                return invalidTokenResponse;
                
            }

            var expirationDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expirationDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expirationDateUnix);

            if(expirationDateUtc > DateTime.UtcNow)
            {
                //Token hasn't expired yet
                return new RefreshTokenResponseModel
                {
                    Message = "Token hasn't expired yet",
                    TerminateSession = false
                };
            }

            string jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            RefreshToken storedRefreshToken = await _dataContext.RefreshTokens.SingleOrDefaultAsync(x => x.Token == new Guid(refreshTokenRequest.RefreshToken));

            if (storedRefreshToken == null)
            {
                //Refresh token doesn't exist error. Logout user
                return invalidTokenResponse;
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpirationDate)
            {
                //Refresh token has expired. Logout user
                return invalidTokenResponse;
            }

            if (storedRefreshToken.Invalidated)
            {
                //Refresh token has been invalidated. Logout user
                return invalidTokenResponse;
            }

            if (storedRefreshToken.Used)
            {
                //Refresh token has been used. Logout user
                return invalidTokenResponse;
            }

            if (storedRefreshToken.JwtId != jti)
            {
                //Refresh token doesn't match Jwt
                return invalidTokenResponse;
            }

            storedRefreshToken.Used = true;
            _dataContext.RefreshTokens.Update(storedRefreshToken);
            await _dataContext.SaveChangesAsync();

            IdentityUser user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "Id").Value);
            TokenGenerationResult token = await GenerateTokenAsync(user);

            return new RefreshTokenResponseModel
            {
                Token = token.Token,
                RefreshToken = token.RefreshToken.Token.ToString(),
                TerminateSession = false
            };
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
