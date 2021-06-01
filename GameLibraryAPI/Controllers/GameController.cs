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

        // THE WHOLE OF GRUD
        //Get games based on input title, devs, tags, sort page
        [HttpGet]
        public List<Game> FindGames(string title, string dev, string tags, string sort, int? page, int length, string dir = "asc")
        {
            IQueryable<Game> query = context.Game;
            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(d => d.Title == title)
                    .Include(d => d.Developer)
                    .Include(d => d.GameScores);
            if (!string.IsNullOrWhiteSpace(dev))
                query = query.Where(d => d.Developer.DevName == dev)
                    .Include(d => d.Developer)
                    .Include(d => d.GameScores);

            //Can't search multiple tags yet, TODO or give up 
            IQueryable<TagsLink> tagquery = context.TagLink;
            if (!string.IsNullOrWhiteSpace(tags))
                tagquery = tagquery.Where(d => d.Tag.TagName == tags);

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


        //Will give the whole library of games and extra information about scores and Developer
        [Route("all")]
        [HttpGet]
        public List<Game> GetAll()
        {
            var gamesinfo = context.Game
                .Include(d => d.GameScores)
                .Include(d => d.Developer)
                .ToList();


            return gamesinfo;
        }

        [Route("full/{id}")]
        [HttpGet]
        public IActionResult GetGameInfo(int id)
        {
            FullGameInfo fullgame = new FullGameInfo();
            fullgame.game = context.Game
                .Include(d => d.GameScores)
                .Include(d => d.Developer)
                .SingleOrDefault(d => d.ID == id);

            IQueryable<TagsLink> tagquery = context.TagLink;
            fullgame.tags = new List<Tag>();
            tagquery = tagquery.Where(d => d.Game.ID == id);
            foreach (TagsLink i in tagquery)
            {
                fullgame.tags.Add(i.Tag);
            }
            return Ok(fullgame);
        }


        //Deletes the id
        [Route("delete/{id}")]
        [HttpDelete]
        public IActionResult DeleteGame(int id)
        {
            //Validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var game = context.Game.Find(id);
            if (game == null)
                return NotFound();
            context.Game.Remove(game);
            context.SaveChanges();
            return Ok();

        }
        [HttpPost]
        public IActionResult CreateGame([FromBody] Game newGame)
        {
            //Validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            context.Game.Add(newGame);
            context.SaveChanges();
            return Created("", newGame);
        }
        [HttpPut]
        public IActionResult UpdateGame([FromBody] Game updatedGame)
        {
            //Validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (context.Game.Find(updatedGame.ID) == null)
                return NotFound();
            var oldGame = context.Game.Find(updatedGame.ID);
            if (oldGame == null)
                return NotFound();
            oldGame.Title = updatedGame.Title;
            oldGame.Developer = updatedGame.Developer;
            context.SaveChanges();
            return Ok(oldGame);

        }
    }
}
