using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibraryAPI
{
    public class Game
    {
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        public Developer Devs { get; set; }

        [JsonIgnore]
        public GameScores GameScores { get; set; }
    }
}
