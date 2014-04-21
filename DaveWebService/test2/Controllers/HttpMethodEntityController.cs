using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DaveWebService.Entity;
using DaveWebService.Controllers;

namespace DaveWebService.Controllers
{
    public class HttpMethodEntityController : ApiController
    {
        // POST Methods - Create/Add Data
        public void PostAddAlbum(AlbumEntity album, GenreEntity genre, TrackEntity track)
        {   
            var recordDataSource = new RecordCollectionDataSource();
            recordDataSource.AddAlbumToAlbumEntity(album);
            
            //  Check to see if the Entities are valid, they will at least need to have values for
            //  Album title, Genre, and a Track Title
            if (album.Album != "" && genre.Genre_01 != "" && track.Track_01_Title != "")
            {
                var albumEnteries = recordDataSource.GetAlbumFromAlbumEntity();
                var genreEnteries = recordDataSource.GetGenreFromGenreEntity();
                var trackEnteries = recordDataSource.GetTrackFromTrackEntity();
                bool foundAlbum = false, foundGenre = false, foundTrack = false;
                foreach (var entry in albumEnteries)
                {
                    if (entry.Album.ToUpper().Equals(album.Album.ToUpper())) { foundAlbum = true; }
                }
                foreach (var entry in genreEnteries)
                {
                    if (entry.Album.ToUpper().Equals(genre.Album.ToUpper())) { foundGenre = true; }
                }
                foreach (var entry in trackEnteries)
                {
                    if (entry.Album.ToUpper().Equals(track.Album.ToUpper())) { foundTrack = true; }
                }
                //  If we don't have the album, then add it...
                if (foundAlbum == false || foundGenre == false || foundTrack == false)
                {
                    recordDataSource.AddAlbumToAlbumEntity(album);
                    recordDataSource.AddGenreToGenreEntity(genre);
                    recordDataSource.AddTrackToTrackEntity(track);
                }
            }
            else
            {
                    //  Failed to add
            }
        }


        // GET Methods - Read Data
        public IEnumerable<AlbumEntity> GetAllAlbums()
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetAlbumFromAlbumEntity();                                                   // 200 OK, listings serialized in response body
        }
        public string GetArtistFromAlbum(string albumName)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetArtistFromAlbum(albumName);
        }
        public IEnumerable<String> GetAlbumsFromArtist(string artistName)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetAlbumsFromArtist(artistName);
        }
        //public IEnumerable<String> GetTrackNamesFromAlbum(string albumName, int discNumber = 0, bool getTracks = true)
        //{
          
        //}
        //public IEnumerable<String> GetAlbumsFromGenre(string genre01, string genre02 = "", string genre03 = "")
        //{
          
        //}
    }
}
