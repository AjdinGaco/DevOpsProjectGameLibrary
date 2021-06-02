using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameLibraryAPI.Controllers
{
    [Route("dev")]
    public class DevController : ControllerBase
    {
        private readonly LibraryContext context;
        public DevController(LibraryContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public List<Developer> GetAll()
        {
            return context.Developers.ToList();
        }

        [HttpPost]
        public IActionResult CreateDev([FromBody] Developer newDev)
        {
            context.Developers.Add(newDev);
            context.SaveChanges();
            return Created("", newDev);
        }

        [Route("remove/{id}")]
        [HttpDelete]
        public IActionResult RemoveDev(int id)
        {

            var foundDev = context.Developers.Find(id);
            if (foundDev == null)
                return NotFound();

            IQueryable<Game> gamesquery = context.Game;
            gamesquery = gamesquery.Where(d => d.Developer == foundDev);
            foreach (Game item in gamesquery)
            {
                item.Developer = null;
            }



            context.Developers.Remove(foundDev);
            context.SaveChanges();
            return Ok();

        }

    }
}
