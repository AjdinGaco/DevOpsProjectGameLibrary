using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameLibraryAPI.Controllers
{
    [Route("api/gamescore")]
    public class TagsController : ControllerBase
    {
        private readonly LibraryContext context;
        public TagsController(LibraryContext context)
        {
            this.context = context;
        }

        [Route("all")]
        [HttpGet]
        public List<GameScores> GetAll()
        {
            return context.GameScores.ToList();
        }
        public IActionResult CreateGameScore([FromBody] GameScores newGameScore)
        {
            context.GameScores.Add(newGameScore);
            context.SaveChanges();
            return Created("", newGameScore);
        }




    }
}
