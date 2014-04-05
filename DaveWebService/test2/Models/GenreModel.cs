﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace daveWebService.Models
{
    public class GenreModel
    {
        public string Genre_01 { get; set; }
        public string Genre_02 { get; set; }
        public string Genre_03 { get; set; }

        
        public string Artist { get; set; }

        public string Album { get; set; }

        public int DiscNumber { get; set; }
    }
}