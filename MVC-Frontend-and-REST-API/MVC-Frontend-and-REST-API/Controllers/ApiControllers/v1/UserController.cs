using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Frontend_and_REST_API.Helperclasses;
using MVC_Frontend_and_REST_API.Logic;
using MVC_Frontend_and_REST_API.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Controllers.ApiControllers.v1
{
    public class UserController : Controller
    {
        private readonly UserLogic _logic;

        public UserController(UserLogic logic)
        {
            _logic = logic;
        }

        [HttpPost(ApiRoutes.User.RegisterV1)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                //Check if User exists
                bool userExists = await _logic.CheckIfUserExists(registerModel.Email);

                if (!userExists)
                {
                    bool saved = await _logic.RegisterAsync(registerModel);
                    if (saved) return Ok("New user registered");
                    return BadRequest("Failed to register user");
                }

                return BadRequest("User already exists");
            }
                
            return BadRequest("Invalid information provided");
        }

        [HttpPost(ApiRoutes.User.LoginV1)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestModel loginRequest)
        {
            if (ModelState.IsValid)
            {
                LoginResponseModel response = await _logic.LoginAsync(loginRequest);
                if (response.LoggedIn) return Ok(response);
                return BadRequest("Failed to log in");
            }

            return BadRequest("Invalid information provided");
        }

        [HttpPost(ApiRoutes.User.RefreshTokenV1)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenRequestModel refreshTokenRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _logic.RefreshTokenAsync(refreshTokenRequest);

                return Ok(response);
            }

            return BadRequest("Invalid information provided");
        }

        [HttpPost(ApiRoutes.User.GetRole)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetRoleAsync()
        {
            string userId = HttpContext.GetUserID();

            if (userId != string.Empty)
            {
                string role = await _logic.GetRoleAsync(userId);

                if(role != string.Empty) return Ok(role);
            }

            return BadRequest("User has no role"); ;
        }
    }
}
