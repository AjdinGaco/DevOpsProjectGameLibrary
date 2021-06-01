﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibraryAPI
{

    //This is meant to send to client 
    public class FullGameInfo
    {
        public Game game { get; set; }
        public List<Tag> tags { get; set; }
    }
}
