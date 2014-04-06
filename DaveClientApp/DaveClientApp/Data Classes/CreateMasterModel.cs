using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaveClientApp.Data_Classes
{
    class CreateMasterModel
    {
        //  Create New Albums, Genres, Tracks
        public MasterModel CreateMaster()
        {
            string album; string artist; int discNumber = 1;    //  Common to all Models
            string label; double albumValue;                    //  Just for AlbumModel
            string genre_01; string genre_02; string genre_03;   //  Just for GenreModel
            int numberOfTracks; string track_01; string track_02; string track_03; string track_04; string track_05; // Just for TrackModel

            Console.WriteLine("In Order to Create an Album, you must provide the following information...");
            Console.WriteLine("Album Title(Required): "); album = Console.ReadLine();
            Console.WriteLine("Album Artist(Required): "); artist = Console.ReadLine();
            Console.WriteLine("Number of Disc's in the Album(Default is 1): "); discNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Number of Tracks(Required): "); numberOfTracks = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Record Label(Required): "); label = Console.ReadLine();
            Console.WriteLine("Value of the Album in Euros(Required): "); albumValue = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("\n");

            Console.WriteLine("Ok, now enter up to 3 genres that best fit the album...");
            Console.WriteLine("Genre 01(Required): "); genre_01 = Console.ReadLine();
            Console.WriteLine("Genre 02(Optional): "); genre_02 = Console.ReadLine();
            Console.WriteLine("Genre 03(Optional): "); genre_03 = Console.ReadLine();
            Console.WriteLine("\n");

            Console.WriteLine("Great! Now enter up to 5 track titles for the album...");
            Console.WriteLine("Track 01 - Title (Required): "); track_01 = Console.ReadLine();
            Console.WriteLine("Track 02 - Title (Optional): "); track_02 = Console.ReadLine();
            Console.WriteLine("Track 03 - Title (Optional): "); track_03 = Console.ReadLine();
            Console.WriteLine("Track 04 - Title (Optional): "); track_04 = Console.ReadLine();
            Console.WriteLine("Track 05 - Title (Optional): "); track_05 = Console.ReadLine();
            Console.WriteLine("\n");

            AlbumModel createdAlbum = this.AddAlbum(album, artist, label, albumValue, discNumber);
            GenreModel createdGenre = this.AddGenre(album, artist, discNumber, genre_01, genre_02, genre_03);
            TrackModel createdTrack = this.AddTrack(album, artist, discNumber, numberOfTracks, track_01, track_02, track_03, track_04, track_05);

            Console.WriteLine("Your Album was created successfully");
            MasterModel newMaster = new MasterModel() { MasterAlbum = createdAlbum, MasterGenre = createdGenre, MasterTrack = createdTrack };
            return newMaster;

        }
        public AlbumModel AddAlbum(string album, string artist, string label, double albumValue, int discNumber = 1)
        {
            AlbumModel al = new AlbumModel() { Album = album, Artist = artist, DiscNumber = discNumber, Label = label, AlbumValue = albumValue };
            return al;
        }
        public GenreModel AddGenre(string album, string artist, int discNumber, string genre_01, string genre_02 = "", string genre_03 = "")
        {
            GenreModel ge = new GenreModel() { Album = album, Artist = artist, DiscNumber = discNumber, Genre_01 = genre_01, Genre_02 = genre_02, Genre_03 = genre_03 };
            return ge;
        }
        public TrackModel AddTrack(string album, string artist, int discNumber, int numberOfTracks, string track_01, string track_02 = "", string track_03 = "", string track_04 = "", string track_05 = "")
        {
            TrackModel tr = new TrackModel() { Album = album, Artist = artist, DiscNumber = discNumber, NumberOfTracks = numberOfTracks, Track_01_Title = track_01, Track_02_Title = track_02, Track_03_Title = track_03, Track_04_Title = track_04, Track_05_Title = track_05 };
            return tr;
        }

    }
}
