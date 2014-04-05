using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace daveWebService.Models
{
    public class GenreModel
    {
        [Required(ErrorMessage = "Please provide a Genre for the Album")]
        public string Genre_01 { get; set; }
        public string Genre_02 { get; set; }
        public string Genre_03 { get; set; }

        
        [Required(ErrorMessage = "Please provide the Artists name for the Album")]
        public string Artist { get; set; }

        [Required(ErrorMessage = "Please provide an Album title")]
        public string Album { get; set; }

        [Required(ErrorMessage = "Please provide the Disc Number of the Album.  Most Albums only have 1 disc")]
        public int DiscNumber { get; set; }
    }
}