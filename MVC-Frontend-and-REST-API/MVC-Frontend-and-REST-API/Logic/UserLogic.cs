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
            IdentityUser response = await _userRepository.Search(email);

            if (response != null) return true;
            return false;
        }
    }
}
