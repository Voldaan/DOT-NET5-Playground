using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Models.ViewModels
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public bool LoggedIn { get; set; }
    }
}
