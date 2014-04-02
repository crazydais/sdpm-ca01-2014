using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test2.Models
{
    public class AlbumModel
    {
        public string Album { get; set; }       //  Title name of the Album
        public string Artist { get; set; }      //  Name of the Artist
        public int DiscNumber { get; set; }
        public string Label { get; set; }
        public double AlbumValue { get; set; }

    }
}
