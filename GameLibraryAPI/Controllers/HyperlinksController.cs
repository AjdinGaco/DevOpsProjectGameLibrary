using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibraryAPI
{
    [Route("api")]
    public class HyperlinksController : ControllerBase
    {






        // HYPERLINKS
        [Route("/all")]
        [HttpGet]
        public List<Link> GetAll()
        {
            string scheme = "https://";
            string îp = "localhost:44389";

            List<Link> HyperLinks = new List<Link>();
/*            HyperLinks.Add(new Link() { method = "GET"}, hyperlink = )*/




            return HyperLinks;
        }

    }
    public class Link
    {
        public string method;
        public string hyperlink;


    }
}
