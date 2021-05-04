using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibraryAPI
{
    public class FullGameInfo
    {
        public Game game { get; set; }
        public List<Tags> tags { get; set; }
    }
}
