using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameLibraryAPI.Controllers
{
    [Route("api/tags")]
    public class PlayerScoreController : ControllerBase
    {
        private readonly LibraryContext context;
        public PlayerScoreController(LibraryContext context)
        {
            this.context = context;
        }

        [Route("all")]
        [HttpGet]
        public List<Tag> GetAll()
        {
            return context.Tag.ToList();
        }
        [HttpPost]
        public IActionResult CreateTag([FromBody] Tag newTag)
        {
            //Validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            context.Tag.Add(newTag);
            context.SaveChanges();
            return Created("", newTag);
        }


        [HttpPut]
        public IActionResult UpdateTag([FromBody] Tag updatedTag)
        {
            //Validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var oldTag = context.Tag.Find(updatedTag.ID);
            if (oldTag == null)
                return NotFound();

            oldTag.TagName = updatedTag.TagName;

            context.SaveChanges();
            return Ok(updatedTag);

        }
    }
}
