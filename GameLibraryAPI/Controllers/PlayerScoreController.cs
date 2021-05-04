using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/*namespace GameLibraryAPI.Controllers
{
    [Route("api/gamescore")]
    public class GameScoresController : ControllerBase
    {
        private readonly LibraryContext context;
        public GameScoresController(LibraryContext context)
        {
            this.context = context;
        }

        [Route("getall")]
        [HttpGet]
        public List<Tags> GetAll()
        {
            return context.Tags.ToList();
        }
        public IActionResult CreateTag([FromBody] Tags newTag)
        {
            context.Tags.Add(newTag);
            context.SaveChanges();
            return Created("", newTag);
        }

        [HttpPut]
        public IActionResult UpdateTag([FromBody] Tags updatedTag)
        {
            var oldTag = context.Tags.Find(updatedTag.ID);

            if (oldTag == null)
                return NotFound();

            oldTag.TagName = updatedTag.TagName;

            context.SaveChanges();
            return Ok(updatedTag);

        }
    }
}
*/