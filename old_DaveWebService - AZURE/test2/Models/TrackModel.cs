using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace daveWebService.Models
{
    public class TrackModel
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public int DiscNumber { get; set; }
        public int NumberOfTracks { get; set; }

        public string Track_01_Title { get; set; }      
        public string Track_02_Title { get; set; }        
        public string Track_03_Title { get; set; }       
        public string Track_04_Title { get; set; }      
        public string Track_05_Title { get; set; }
    }
}