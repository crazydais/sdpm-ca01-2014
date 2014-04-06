using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace daveWebService.Models
{
    public class MasterModel
    {
        public AlbumModel MasterAlbum { get; set; }
        public GenreModel MasterGenre { get; set; }
        public TrackModel MasterTrack { get; set; }
    }
}