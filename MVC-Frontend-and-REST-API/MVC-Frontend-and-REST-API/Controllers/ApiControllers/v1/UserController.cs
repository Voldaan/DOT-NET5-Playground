using Microsoft.AspNetCore.Mvc;
using MVC_Frontend_and_REST_API.Helperclasses;
using MVC_Frontend_and_REST_API.Logic;
using MVC_Frontend_and_REST_API.Models.ViewModels;
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

        [HttpPost(ApiRoutes.User.UsersV1)]
        public async Task<IActionResult> Create([FromBody] RegisterModel registerModel)
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
    }
}
