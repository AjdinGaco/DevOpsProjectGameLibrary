using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameLibraryAPI.Controllers
{
    [Route("game/tag")]
    public class TagsController : ControllerBase
    {
        private readonly LibraryContext context;
        public TagsController(LibraryContext context)
        {
            this.context = context;
        }

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

        [Route("remove/{id}")]
        [HttpDelete]
        public IActionResult RemoveTag(int id)
        {

            var foundtag = context.Tag.Find(id);
            if (foundtag == null)
                return NotFound();

            IQueryable<TagsLink> tagslinkquery = context.TagLink;
            tagslinkquery = tagslinkquery.Where(d => d.Tag == foundtag);
            foreach (TagsLink item in tagslinkquery)
            {
                context.TagLink.Remove(item);
            }



            context.Tag.Remove(foundtag);
            context.SaveChanges();
            return Ok();

        }

    }
}
