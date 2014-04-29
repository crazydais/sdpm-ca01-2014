using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace daveWebService.Models
{
    public class AlbumModel
    {
        public string Artist { get; set; }

        public string Album { get; set; }

        public int DiscNumber { get; set; }

        public string Label { get; set; }
        
        public double AlbumValue { get; set; }

    }
}
