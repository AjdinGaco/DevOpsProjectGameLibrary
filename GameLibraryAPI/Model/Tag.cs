using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibraryAPI
{
    public class Tag
    {
        public int ID { get; set; }

        [Required]
        public string TagName { get; set; }
    }
}
