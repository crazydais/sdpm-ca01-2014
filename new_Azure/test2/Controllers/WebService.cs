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
        /// <summary>
        /// DeleteAlbum: Deletes an Album entity from the Album Table.
        /// </summary>
        /// <param name="albumToDelete">String: Identifies the Album to delete.</param>
        /// <param name="byArtist">String: Indentifies the Artist of the album</param>
        /// <returns>String: Returns a string used for testing.</returns>
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
        /// <summary>
        /// PutUpdateForAlbum: Updates a member variable of an entity in one of the Azure tables.
        /// </summary>
        /// <param name="entityType">String: Identifies which table to update (ie, Album, Genre, or Track).</param>
        /// <param name="albumToUpdate">String: Identifies the album to update.</param>
        /// <param name="byArtist">String: Identifies the artist of the album.</param>
        /// <param name="parameterToUpdate">Identifies the member varible to update (ie, Album, Artist, Label, Value, Rating).</param>
        /// <param name="newValue"></param>
        /// <returns>String: Returns a string used for testing.</returns>
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
        /// <summary>
        /// PostAddAlbum: Adds an Album entity to the Album table.
        /// </summary>
        /// <param name="addAlbum">AlbumEntity: An instance of an albumEntity</param>
        /// <param name="album">Bool: Confirms the adding of the new album entity</param>
        /// <returns>String: Returns a string used for testing.</returns>
        public string PostAddAlbum(AlbumEntity addAlbum, bool album)
        {
            string result = "";
            var recordDataSource = new RecordCollectionDataSource();
            recordDataSource.AddAlbumToAlbumEntity(addAlbum);
            result = "Album Added Successfully";

            return result;
        }
        
        /// <summary>
        /// PostAddGenre: Adds a Genre entity to the Genre table.
        /// </summary>
        /// <param name="addGenre">GenreEntity: An instance of a genreEntity</param>
        /// <param name="genre">Bool: Confirms the adding of the new genre entity.</param>
        /// <returns>String: Returns a string used for testing.</returns>
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
        
        /// <summary>
        /// PostAddTrack: Adds a Track entity to the Track table.
        /// </summary>
        /// <param name="addTrack">TrackEntity: An instance of a trackEntity.</param>
        /// <param name="track">Bool: Confirms the adding of the new track entity.</param>
        /// <returns>String: Returns a string used for testing.</returns>
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
        /// GetAllAlbums: Show all the albums that are currently stored in the Albums table.
        /// </summary>
        /// <param name="showCollection">Bool: true, false</param>
        /// <returns>Returns an IEnumerable'AlbumEntity' - of all the albums in the Album Table</returns>
        public IEnumerable<AlbumEntity> GetAllAlbums(bool showCollection)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetAlbumFromAlbumEntity(showCollection);          // 200 OK, listings serialized in response body
        }
        
        /// <summary>
        /// GetArtistFromAlbum: Show the artist's name for the album.
        /// </summary>
        /// <param name="albumName">String: Album Title</param>
        /// <returns>Returns a string - of the artists name</returns>
        public string GetArtistFromAlbum(string getArtistFromAlbumName)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetArtistFromAlbum(getArtistFromAlbumName);
        }
        
        /// <summary>
        /// GetAlbumsFromArtist: Show all the albums by a certain artist.
        /// </summary>
        /// <param name="artistName">String: Artist's Name or Band/Group Title</param>
        /// <returns>Returns an IEnumerable'String' - of all the albums written by that artist</returns>
        public IEnumerable<String> GetAlbumsFromArtist(string getAlbumsFromArtistName)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetAlbumsFromArtist(getAlbumsFromArtistName);
        }
        
        /// <summary>
        /// GetTrackNamesFromAlbum: Gets a list of all the tracks for a given album title.
        /// </summary>
        /// <param name="getTracksFromAlbumName">String: The title of the album.</param>
        /// <returns>IEnumerable(String): List of all tracks in the queried album.</returns>
        public IEnumerable<String> GetTrackNamesFromAlbum(string getTracksFromAlbumName)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetTracksFromAlbum(getTracksFromAlbumName);
        }
        
        /// <summary>
        /// GetAlbumsFromGenre: Gets all the albums which are associated with the given genre's.
        /// </summary>
        /// <param name="forGenre01">String: Genre to query</param>
        /// <param name="forGenre02">String: Genre to query</param>
        /// <param name="forGenre03">String: Genre to query</param>
        /// <returns>IEnumerable(String): List of all the albums that match the query.</returns>
        public IEnumerable<String> GetAlbumsFromGenre(string forGenre01, string forGenre02, string forGenre03)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetAlbumsFromGenres(forGenre01, forGenre02, forGenre03);
        }
        
        /// <summary>
        /// GetAlbumsInOrderOfExpense: Gets a list of all the albums in order of their value, from high to low.
        /// </summary>
        /// <param name="showHighestValueAlbumsFirst">Bool: Confirm query</param>
        /// <returns>IEnumberable(String): A list of all the albums in order of their value, from high to low.</returns>
        public IEnumerable<AlbumEntity> GetAlbumsInOrderOfExpense(bool showHighestValueAlbumsFirst)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetAlbumsInOrderOfExpense(showHighestValueAlbumsFirst);
        }
        
        /// <summary>
        /// GetTopRatedAlbums: Gets a list of all the albums in order of their rating, from high to low.
        /// </summary>
        /// <param name="showTopRatedAlbums">Bool: Confirms the query.</param>
        /// <returns>IEnumberable(String): A list of all the albums in order of their rating, from high to low.</returns>
        public IEnumerable<AlbumEntity> GetTopRatedAlbums(bool showTopRatedAlbums)
        {
            var recordDataSource = new RecordCollectionDataSource();
            return recordDataSource.GetTopRatedAlbums(showTopRatedAlbums);
        }
        
        /// <summary>
        /// GetCollectionReport: Retruns a report of about the users record collection.
        /// </summary>
        /// <param name="showRecordCollectionStatistics">Bool: Confirms the query</param>
        /// <returns>IEnumberable(String): A report summary of the record collection.</returns>
        public IEnumerable<String> GetCollectionReport(bool showRecordCollectionStatistics)
        {
            var rds = new RecordCollectionDataSource();
            List<String> Report = new List<String>();

            //  Title...
            Report.Add(String.Format("\tRecord Collection Report"));
            Report.Add(String.Format("- - - - - - - - - - - - - - - - - - - - - - - - - - - - \n"));
            
            //  Number of Albums...
            var albums = this.GetAllAlbums(true);
            int numOfAlbums = albums.Count<AlbumEntity>();
            Report.Add(String.Format("Total number of albums:\t {0} ", numOfAlbums));
            Report.Add(String.Format("\n"));

            //  Highest Valued Album...
            var highestValue = rds.GetAlbumsInOrderOfExpense(true);
            var value = highestValue.First<AlbumEntity>();
            Report.Add(String.Format("The most expensive album is:\t {0} ", value.ToString()));
            Report.Add(String.Format("\n"));

            //  Highest Rated Album...
            var highestRating = rds.GetTopRatedAlbums(true);
            var rating = highestRating.First<AlbumEntity>();
            Report.Add(String.Format("The highest rated album is:\t {0} ",rating.ToString()));
            Report.Add(String.Format("\n"));

            return Report;
        }
    }
}
