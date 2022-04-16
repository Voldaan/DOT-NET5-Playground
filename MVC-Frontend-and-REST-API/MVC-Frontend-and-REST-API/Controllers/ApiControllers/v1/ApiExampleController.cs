using Microsoft.AspNetCore.Mvc;
using MVC_Frontend_and_REST_API.Helperclasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Controllers.ApiControllers.v1
{
    public class ApiExampleController : Controller
    {
        [HttpGet(ApiRoutes.Example.IndexV1)]
        public IActionResult Index()
        {
            //return View();
            return Ok("Api call received");
        }
    }
}
