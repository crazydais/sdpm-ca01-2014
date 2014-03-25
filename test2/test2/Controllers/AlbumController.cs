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
        static List<AlbumModel> albums = new List<AlbumModel>() 
        { 
            new AlbumModel { Title = "EP-01", Artist = "Dave Nolan", Genre = "Tech House" }, 
            new AlbumModel { Title = "Computer World", Artist = "Kraftwerk", Genre = "Electronic"   }, 
            new AlbumModel { Title = "Trouser Jazz", Artist = "Mr. Scruff", Genre = "Future Jazz"   },
            new AlbumModel { Title = "I Care Because You Do", Artist = "Aphex Twin", Genre = "Electronica"   } 
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
        public string GetAlbumArtistByTitle(string title)
        {
            // LINQ query, find matching ticker (case-insensitive) or default value (null) if none matching
            AlbumModel album = albums.FirstOrDefault(a => a.Title.ToUpper() == title.ToUpper());
            if (album == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);       // translated into a http response status code 404
            }
            return album.Artist;                                               // 200 OK, price serialized in response body
        } 
    }
}
