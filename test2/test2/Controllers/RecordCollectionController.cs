using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using test2.Models;

namespace test2.Controllers
{
    public class RecordCollectionController : ApiController
    {        
        static List <AlbumModel>albums = new List <AlbumModel>() 
        { 
            new AlbumModel { Album = "No Album Found", Artist = "No Artist Found", Label = "No Label Found", AlbumValue = 0, DiscNumber = 0 },
            new AlbumModel { Album = "EP-01", Artist = "Dave Nolan", Label = "Unsigned", AlbumValue = 10.50, DiscNumber = 1 },
            new AlbumModel { Album = "AI:TM", Artist = "Dave Nolan", Label = "Unsigned", AlbumValue = 0.00, DiscNumber = 1 },
            new AlbumModel { Album = "Computer World", Artist = "Kraftwerk", Label = "EMI", AlbumValue = 12.00, DiscNumber = 1 }, 
            new AlbumModel { Album = "Trouser Jazz", Artist = "Mr. Scruff", Label = "Ninja Tune", AlbumValue = 11.25, DiscNumber = 1 },
        };

        static List<TrackModel>tracks = new List<TrackModel>()
        {
            new TrackModel {NumberOfTracks = 3, Track_01_Title = "M01234", Track_02_Title = "Dub Track", Track_03_Title = "It's Raining", Artist = "Dave Nolan", Album =  "EP-01", DiscNumber = 1 },
            new TrackModel {NumberOfTracks = 3, Track_01_Title = "Computer World", Track_02_Title = "Pocket Calculator", Track_03_Title = "Numbers", Artist = "Kraftwerk", Album = "Computer World", DiscNumber = 1},
            new TrackModel {NumberOfTracks = 2, Track_01_Title = "	Here We Go", Track_02_Title = "Sweetsmoke", Artist = "Mr. Scruff", Album = "Trouser Jazz", DiscNumber = 1}
        };

        static List<GenresModel> genres = new List<GenresModel>()
        {
            new GenresModel {Artist = "Dave Nolan", Album = "EP-01", DiscNo = 1, Genre_01 = "Deep House", Genre_02 = "Tech House"},
            new GenresModel {Artist = "Kraftwerk", Album = "Computer World", DiscNo = 1, Genre_01 = "Electronic"},
            new GenresModel {Artist = "Mr. Scruff", Album = "Trouser Jazz", DiscNo = 1, Genre_01 = "Funk", Genre_02 = "Future Jazz"}
        };

        // GET: /Album/
        public IEnumerable<AlbumModel> GetAllAlbums()
        {
            return albums;                                                   // 200 OK, listings serialized in response body
        }
        /// <summary>
        /// This method returns the artists name when the title of the album is given
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GetArtistFromAlbum(string albumName)
        {
            bool foundMatch = false;
            string artistList = "";     //  This will return the artist name belonging to the album name that was provided

            foreach(AlbumModel al in albums)
            {
                if(al.Album.ToUpper().Equals(albumName.ToUpper()))
                {
                    foundMatch = true;
                    artistList = al.Artist;
                }      
            }
            if (!foundMatch)
            {
                artistList = "No Album Found, with name: " + albumName;
            }

            return artistList;                                               
        }


        //public IEnumerable<String> GetAlbumFromArtist(string artistName)
        public IEnumerable<String> GetAlbumFromArtist(string artistName)
        {
            bool foundMatch = false;
            List<String> albumList = new List<String>();    //  This will return a list of Albums associated with the Artist Name that was provided.

            foreach (AlbumModel al in albums)
            {
                if (al.Artist.ToUpper().Equals(artistName.ToUpper()))
                {
                    foundMatch = true;
                    albumList.Add(al.Album);
                }
            }
            if (!foundMatch)
            {
                albumList.Add("No Artist Found, with name: " + artistName);
            }

            return albumList;
        }
    }
}
