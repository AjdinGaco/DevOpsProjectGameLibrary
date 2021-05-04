using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameLibraryAPI.Controllers
{
    [Route("api/tagslink")]
    public class TagsLinkController : ControllerBase
    {
        private readonly LibraryContext context;
        public TagsLinkController(LibraryContext context)
        {
            this.context = context;
        }
        [Route("getall")]
        [HttpGet]
        public List<TagsLink> GetAll()
        {
            return context.TagsLink.ToList();
        }

        [Route("link/{id1}/{id2}")]
        [HttpGet]
        public IActionResult LinkGametoTagsLink(int id1, int id2)
        {
            // id 1 is de game id, id 2 is de tags id bv 1/1 => warframe link met action
            
            var game = context.Game.Find(id1);
            if (game == null)
                return NotFound();
            var linkedtag = context.Tags.Find(id2);
            if (linkedtag == null)
                return NotFound();

            //TODO check if the link exists


            //Create a new taglink with the given info
            var tagslink = new TagsLink();
            tagslink.GameID = game;
            tagslink.TagID = linkedtag;

            context.TagsLink.Add(tagslink);
            context.SaveChanges();
            return Ok(tagslink);
        }


        [HttpPut]
        public IActionResult UpdateGame([FromBody] TagsLink updateTagsLink)
        {
            var oldTagsLink = context.TagsLink.Find(updateTagsLink.ID);

            if (oldTagsLink == null)
                return NotFound();

            oldTagsLink.GameID = updateTagsLink.GameID;
            oldTagsLink.TagID = updateTagsLink.TagID;

            context.SaveChanges();
            return Ok(updateTagsLink);

        }


    }
}
