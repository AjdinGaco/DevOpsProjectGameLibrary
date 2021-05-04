using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibraryAPI
{
    public class GameScores
    {
        //All this can and will change in the future once i get a good grasp on what to score really
        public int ID { get; set; }
        [Range(0,10)]
        public int GeneralScore { get; set; }
        [Range(0, 10)]
        public int Action { get; set; }
        [Range(0, 10)]
        public int Replayability { get; set; }
        [Range(0, 10)]
        public int Fun { get; set; }

    }
}
