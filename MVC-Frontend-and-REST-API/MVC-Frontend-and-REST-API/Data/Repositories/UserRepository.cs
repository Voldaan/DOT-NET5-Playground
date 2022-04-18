using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Data.Repositories
{
    public class UserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
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
    }
}
