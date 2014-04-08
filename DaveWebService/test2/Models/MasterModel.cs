using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaveWebService.Models
{
    public class MasterModel
    {
        public AlbumModel MasterAlbum { get; set; }
        public GenreModel MasterGenre { get; set; }
        public TrackModel MasterTrack { get; set; }

        public MasterModel()
        {

        }

        public MasterModel(AlbumModel al, GenreModel ge, TrackModel tr)
        {
            MasterAlbum = al;
            MasterGenre = ge;
            MasterTrack = tr;
        }

    }
}