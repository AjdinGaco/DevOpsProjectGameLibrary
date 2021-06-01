using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameLibraryAPI.Controllers
{
    [Route("api/tag")]
    public class TagsController : ControllerBase
    {
        private readonly LibraryContext context;
        public TagsController(LibraryContext context)
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
            context.Tag.Add(newTag);
            context.SaveChanges();
            return Created("", newTag);
        }

    }
}
