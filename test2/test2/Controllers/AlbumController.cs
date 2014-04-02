using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using test2.Models;

namespace test2.Controllers
{
    public class AlbumController : ApiController
    {        
        static List <AlbumModel>albums = new List <AlbumModel>() 
        { 
            new AlbumModel { Album = "EP-01", Artist = "Dave Nolan", Label = "Unsigned", AlbumValue = 10.50, DiscNumber = 1 }, 
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
  

        //
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
            // LINQ query, find matching ticker (case-insensitive) or default value (null) if none matching
            AlbumModel album = albums.FirstOrDefault(a => a.Album.ToUpper() == albumName.ToUpper());
            if (album == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);       // translated into a http response status code 404
            }
            return album.Artist;                                               // 200 OK, price serialized in response body
        }
        public IEnumerable<String> GetAlbumFromArtist(string artistName)
        {
            List<String> albumList = new List<String>();
            AlbumModel album = albums.FirstOrDefault(a => a.Artist.ToUpper() == artistName.ToUpper());
            if (album == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);       // translated into a http response status code 404
            }
            if (album.Artist.Equals(artistName))
            {
                albumList.Add(album.Album);
            }
            else
            {
                albumList.Add("");
            }

            return albumList;
        }
    }
}
