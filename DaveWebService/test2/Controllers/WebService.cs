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
    public class WebServiceController : ApiController
    {
        
     //  DELETE Methods - Remove Album, Genre, and Track entities from respective tables
        public string DeleteAlbum(string albumToDelete, string byArtist)
        {
            string result = "";
            try
            {
                var recordDataSource = new RecordCollectionDataSource();
                
                recordDataSource.DeleteGenreFromGenreEntity(albumToDelete, byArtist);
                recordDataSource.DeleteTrackFromTrackEntity(albumToDelete, byArtist);
                recordDataSource.DeleteAlbumFromAlbumEntity(albumToDelete, byArtist);
                result = "Album Deleted successfuly";
            }
            catch (Exception)
            {
                result = "Album was not deleted.  An exception was thrown";
            }

            return result;
        }

     //  PUT Methods - Update an entity
        public string PutUpdateForAlbum(string entityType, string albumToUpdate, string byArtist, string parameterToUpdate, string newValue)
        {
            string result = "";
            var recordDataSource = new RecordCollectionDataSource();

            //  If the parameter being updated is 'album' or 'artist', then all three entities will need to be updated.
            if (parameterToUpdate.ToUpper().Equals("ALBUM") || parameterToUpdate.ToUpper().Equals("ARTIST"))
            {
                try
                {
                    recordDataSource.PutUpdateForAlbum(albumToUpdate, byArtist, parameterToUpdate, newValue);
                    recordDataSource.PutUpdateForGenre(albumToUpdate, byArtist, parameterToUpdate, newValue);
                    recordDataSource.PutUpdateForTrack(albumToUpdate, byArtist, parameterToUpdate, newValue);

                    result = "Update was successful";
                }
                catch (Exception)
                {
                    result = "Updating album was unsuccessful. An exception was thrown";
                }
            }
            //  Otherwise, update one of the specific entities.
            else
            {
                if (entityType.ToUpper().Equals("ALBUMENTITY"))
                {
                    try
                    {
                        recordDataSource.PutUpdateForAlbum(albumToUpdate, byArtist, parameterToUpdate, newValue);
                        result = "Update was successful";
                    }
                    catch (Exception)
                    {
                        result = "Updating album was unsuccessful. An exception was thrown";
                    }
                }
                else if (entityType.ToUpper().Equals("GENREENTITY"))
                {
                    try
                    {
                        recordDataSource.PutUpdateForGenre(albumToUpdate, byArtist, parameterToUpdate, newValue);
                        result = "Update was successful";
                    }
                    catch (Exception)
                    {
                        result = "Updating album was unsuccessful. An exception was thrown";
                    }
                }
                else if (entityType.ToUpper().Equals("TRACKENTITY"))
                {
                    try
                    {
                        recordDataSource.PutUpdateForTrack(albumToUpdate, byArtist, parameterToUpdate, newValue);
                        result = "Update was successful";
                    }
                    catch (Exception)
                    {
                        result = "Updating album was unsuccessful. An exception was thrown";
                    }
                }
            }

            return result;
        }
                
     // POST Methods - Create/Add Data
        public string PostAddAlbum(AlbumEntity addAlbum, bool album)
        {
            string result = "";
            var recordDataSource = new RecordCollectionDataSource();
            recordDataSource.AddAlbumToAlbumEntity(addAlbum);
            result = "Album Added Successfully";

            return result;
        }
        public string PostAddGenre(GenreEntity addGenre, bool genre)
        {
            string result = "";
            var recordDataSource = new RecordCollectionDataSource();
            //  Check to see if the Entities are valid, they will at least need to have values for
            //  Album title, Genre, and a Track Title
            if (addGenre.Album != "")
            {
                var genreEnteries = recordDataSource.GetGenreFromGenreEntity(true);
                bool foundAlbum = false;
                foreach (var entry in genreEnteries)
                {
                    if (entry.Album.ToUpper().Equals(addGenre.Album.ToUpper())) { foundAlbum = true; }
                }
                //  If we don't have the album, then add it...
                if (foundAlbum == false)
                {
                    recordDataSource.AddGenreToGenreEntity(addGenre);
                    result = "Genre entity Added Successfully";
                }
            }
            else
            {
                result = "Failed To Add Genre Entity. Genre Entity Already Exists";
            }

            return result;

        }
        public string PostAddTrack(TrackEntity addTrack, bool track)
        {
            string result = "";
            var recordDataSource = new RecordCollectionDataSource();
            //  Check to see if the Entities are valid, they will at least need to have values for
            //  Album title, Genre, and a Track Title
            if (addTrack.Album != "")
            {
                var trackEnteries = recordDataSource.GetTrackFromTrackEntity(true);
                bool foundAlbum = false;
                foreach (var entry in trackEnteries)
                {
                    if (entry.Album.ToUpper().Equals(addTrack.Album.ToUpper())) { foundAlbum = true; }
                }
                //  If we don't have the album, then add it...
                if (foundAlbum == false)
                {
                    recordDataSource.AddTrackToTrackEntity(addTrack);
                    result = "Track entity Added Successfully";
                }
            }
            else
            {
                result = "Failed To Add track Entity. Track Entity Already Exists";
            }

            return result;
        }

     // GET Methods - Read Data
        
        /// <summary>
        /// Show all the albums that are currently stored in the Albums table.
        /// </summary>
        /// <param name="showCollection">Boolean Type: true, false</param>
        /// <returns>Returns an IEnumerable'AlbumEntity' - of all the albums in the Album Table</returns>
        public IEnumerable<AlbumEntity> GetAllAlbums(bool showCollection)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetAlbumFromAlbumEntity(showCollection);          // 200 OK, listings serialized in response body
        }
        
        /// <summary>
        /// Show the artist's name for the album.
        /// </summary>
        /// <param name="albumName">String type: Album Title</param>
        /// <returns>Returns a string - of the artists name</returns>
        public string GetArtistFromAlbum(string getArtistFromAlbumName)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetArtistFromAlbum(getArtistFromAlbumName);
        }
        
        /// <summary>
        /// Show all the albums by a certain artist.
        /// </summary>
        /// <param name="artistName">String Type: Artist's Name or Band/Group Title</param>
        /// <returns>Returns an IEnumerable'String' - of all the albums written by that artist</returns>
        public IEnumerable<String> GetAlbumsFromArtist(string getAlbumsFromArtistName)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetAlbumsFromArtist(getAlbumsFromArtistName);
        }
        public IEnumerable<String> GetTrackNamesFromAlbum(string getTracksFromAlbumName)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetTracksFromAlbum(getTracksFromAlbumName);
        }
        public IEnumerable<String> GetAlbumsFromGenre(string forGenre01, string forGenre02 = "", string forGenre03 = "")
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetAlbumsFromGenres(forGenre01, forGenre02, forGenre03);
        }
        public IEnumerable<AlbumEntity> GetAlbumsInOrderOfExpense(bool showHighestValueAlbumsFirst)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetAlbumsInOrderOfExpense(showHighestValueAlbumsFirst);
        }
        public IEnumerable<AlbumEntity> GetTopRatedAlbums(bool showTopRatedAlbums)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetTopRatedAlbums(showTopRatedAlbums);
        }
    }
}
