using Microsoft.AspNetCore.Mvc;
using MVC_Frontend_and_REST_API.Helperclasses;
using MVC_Frontend_and_REST_API.Logic;
using MVC_Frontend_and_REST_API.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Frontend_and_REST_API.Controllers.ApiControllers.v1
{
    public class ArtistController : Controller
    {
        private readonly ArtistLogic _logic;

        public ArtistController()//ArtistLogic logic)
        {
            //_logic = logic;
        }

        [HttpGet(ApiRoutes.Artist.ArtistsV1)]
        public IActionResult Get()
        {
            //_logic.Read();
            return Ok("Get call received");
        }

        [HttpPost(ApiRoutes.Artist.ArtistsV1)]
        public IActionResult Post(Artist model)
        {
            //_logic.Create(model);
            return Ok("Post call received");
        }

        [HttpPut(ApiRoutes.Artist.ArtistsV1)]
        public IActionResult Put(Artist model)
        {
            //_logic.Update(model);
            return Ok("Put call received");

        }

        [HttpDelete(ApiRoutes.Artist.ArtistsV1)]
        public IActionResult Delete(Guid id)
        {
            //_logic.Delete(id);
            return Ok("Delete call received");
        }
    }
}
