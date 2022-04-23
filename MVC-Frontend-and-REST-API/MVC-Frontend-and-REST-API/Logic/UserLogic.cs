using Microsoft.AspNetCore.Identity;
using MVC_Frontend_and_REST_API.Data.Repositories;
using MVC_Frontend_and_REST_API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Logic
{
    public class UserLogic
    {
        private UserRepository _userRepository;

        public UserLogic(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterAsync(RegisterModel registerModel)
        {
            var identityUser = new IdentityUser
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName
            };

            return await _userRepository.RegisterAsync(identityUser, registerModel.Password);
        }

        public async Task<bool> CheckIfUserExists(string email)
        {
            IdentityUser response = await _userRepository.SearchAsync(email);

            if (response != null) return true;
            return false;
        }

        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel loginRequest)
        {
            return await _userRepository.LoginAsync(loginRequest);
        }

        public async Task<RefreshTokenResponseModel> RefreshTokenAsync(RefreshTokenRequestModel refreshTokenRequest)
        {
            return await _userRepository.RefreshTokenAsync(refreshTokenRequest);
        }

        public async Task<string> GetRoleAsync(string id)
        {
            IList<string> roles = await _userRepository.SearchByIdAsync(id);

            if (roles.Count >= 1) return roles[0];
            else {
                //Create role for the user
                string role = await _userRepository.AddRoleToUserAsync(id);

                if(role != String.Empty) return role;
            }

            return string.Empty;
        }
    }
}
