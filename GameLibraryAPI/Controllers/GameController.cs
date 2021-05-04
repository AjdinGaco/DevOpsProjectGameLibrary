using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameLibraryAPI.Controllers
{
    [Route("api")]
    public class GameController : ControllerBase
    {
        private readonly LibraryContext context;
        public GameController(LibraryContext context)
        {
            this.context = context;
        }



        //Get games based on stuff
        [HttpGet]
        public List<Game> GetAllGames(string title, string dev, string tags,string sort, int? page, int length, string dir = "asc")
        {
            IQueryable<Game> query = context.Game;


            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(d => d.Title == title);
            if (!string.IsNullOrWhiteSpace(dev))
                query = query.Where(d => d.Developers == dev);
            // TODO TAGS

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort)
                {  
                    case "title":
                        if (dir == "asc")
                            query = query.OrderBy(d => d.Title);
                        else if (dir == "desc")
                            query = query.OrderByDescending(d => d.Title);
                        break;
                    case "score":
                        if (dir == "asc")
                            query = query.OrderBy(d => d.GameScores.GeneralScore);
                        else if (dir == "desc")
                            query = query.OrderByDescending(d => d.GameScores.GeneralScore);
                        break;
                }
            }


            if (page.HasValue)
                query = query.Skip(page.Value * length);
            query.Take(length);


            return query.ToList();
        }


        //TODO I WILL PROB REMOVE THIS LATER
        [Route("getall")]
        [HttpGet]
        public List<Game> GetAll()
        {
            return context.Game.ToList();
        }

        [Route("getfull/{id}")]
        [HttpPost]
        public IActionResult GetGameInfo(int id)
        {
            FullGameInfo fullgame = new FullGameInfo();
            fullgame.game = context.Game
                .Include(d => d.GameScores)
                .SingleOrDefault(d => d.ID == id);

            IQueryable<TagsLink> tagquery = context.TagsLink;
            fullgame.tags = new List<Tags>();
            tagquery = tagquery.Where(d => d.GameID.ID == id);
            foreach (TagsLink i in tagquery)
            {
                //TODO
                fullgame.tags.Add(new Tags());
/*                fullgame.tags.Add(context.Tags.Find(i.TagID.ID));*/

            }



            return Ok(fullgame);
        }
        [Route("delete/{id}")]
        [HttpDelete]
        public IActionResult DeleteGame(int id)
        {
            var game = context.Game.Find(id);
            if (game == null)
                return NotFound();
            context.Game.Remove(game);
            context.SaveChanges();
            return NoContent();

        }
        [HttpPost]
        public IActionResult CreateGame([FromBody] Game newGame)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            context.Game.Add(newGame);
            context.SaveChanges();
            return Created("", newGame);
        }
        [HttpPut]
        public IActionResult UpdateGame([FromBody] Game updatedGame)
        {
            if (context.Game.Find(updatedGame.ID) == null)
                return NotFound();
            var oldGame = context.Game.Find(updatedGame.ID);

            if (oldGame == null)
                return NotFound();

            oldGame.Title = updatedGame.Title;
            oldGame.Developers = updatedGame.Developers;

            context.SaveChanges();

            return Ok(oldGame);

        }
    }
}
