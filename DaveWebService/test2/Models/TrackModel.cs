using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace daveWebService.Models
{
    public class TrackModel
    {
        [Required(ErrorMessage = "Please provide a tack title for track 01")]
        public string Track_01_Title { get; set; }
        
        [Required(ErrorMessage = "Please provide a tack title for track 02")]
        public string Track_02_Title { get; set; }
        
        [Required(ErrorMessage = "Please provide a tack title for track 03")]
        public string Track_03_Title { get; set; }
        
        [Required(ErrorMessage = "Please provide a tack title for track 04")]
        public string Track_04_Title { get; set; }
        
        [Required(ErrorMessage = "Please provide a tack title for track 05")]
        public string Track_05_Title { get; set; }

       
        
        [Required(ErrorMessage = "Please provide the number of tracks in the album")]
        public int NumberOfTracks { get; set; }

        [Required(ErrorMessage = "Please provide the Artists name for the Album")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Please provide an Album title")]
        public string Album { get; set; }

        [Required(ErrorMessage = "Please provide the Disc Number of the Album.  Most Albums only have 1 disc")]
        public int DiscNumber { get; set; }
    }
}