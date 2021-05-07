using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibraryAPI
{
    public class TagsLink
    {
        //Ik plan om die primary key weg te doen, maar weet niet hoe
        public int ID { get; set; }
        public Tag Tag { get; set; }
        public Game Game { get; set; }


    }
}
