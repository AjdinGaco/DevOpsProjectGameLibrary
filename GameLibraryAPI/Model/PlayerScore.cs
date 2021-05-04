using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibraryAPI
{
    public class PlayerScore
    {
        public int ID { get; set; }
        public string PlayerID { get; set; }
        [JsonIgnore]
        public Game Game { get; set; }
        public int Score { get; set; }
    }
}
