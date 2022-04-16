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

        public ArtistController(ArtistLogic logic)
        {
            _logic = logic;
        }

        [HttpGet(ApiRoutes.Artist.ArtistsV1)]
        public IActionResult Get()
        {
            var response = _logic.Read();

            if(response.Count > 0) return new JsonResult(response);

            return BadRequest("Couldn't find any artists");
        }

        [HttpPost(ApiRoutes.Artist.ArtistsV1)]
        public IActionResult Post(Artist model)
        {
            if (ModelState.IsValid)
            {
                bool saved = _logic.Create(model);
                if(saved) return Ok("Created new artist");
            }
            
            return BadRequest("Failed to create artist");
        }

        [HttpPut(ApiRoutes.Artist.ArtistsV1)]
        public IActionResult Put(Artist model)
        {
            if (ModelState.IsValid && model.Id != Guid.Empty)
            {
                bool saved = _logic.Update(model);
                if (saved) return Ok("Artist updated");
            }

            return BadRequest("Failed to update the artist");
        }

        [HttpDelete(ApiRoutes.Artist.ArtistsV1)]
        public IActionResult Delete(Guid id)
        {
            if(id != Guid.Empty)
            {
                bool saved = _logic.Delete(id);
                if (saved) return Ok("Artist removed");
            }

            return BadRequest("Failed to remove the artist");
        }
    }
}
