using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaveClientApp.Data_Classes
{
    class MasterModel
    {
        public AlbumModel MasterAlbum { get; set; }
        public GenreModel MasterGenre { get; set; }
        public TrackModel MasterTrack { get; set; }
    }
}
