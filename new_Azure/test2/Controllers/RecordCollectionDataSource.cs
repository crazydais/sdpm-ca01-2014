using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.WindowsAzure.Storage.Auth;
//using Microsoft.WindowsAzure.Storage.Table;

using DaveWebService.Entity;
using DaveWebService.Controllers;

namespace DaveWebService.Controllers
{
    public class RecordCollectionDataSource
    {
        // this configuration setting would be in .csdef and .cscfg for a cloud service
        // use the development storage rather than a specific storage account
            //private static String dataConnectionString = "UseDevelopmentStorage=true";
        private static CloudStorageAccount storageAccount;

        // for a real storage account:
        //dataConnectionString = "DefaultEndpointsProtocol=https;AccountName=garyclynch;AccountKey=keyvalue";
            //private static String tableName = "AlbumCollectionTable";
        
        
        
        private RecordCollectionContext context;

    //  Static Constructor - called by the .Net framework at runtime. 
        static RecordCollectionDataSource()
        {
            storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=daveyca1;AccountKey=wULXw8vdCCY+3aLpwP/cOFdPsy4UQ35H43SBOCNVho85BjjKEAMZjR5mJ4sS/CsSrVEa/rJQwLUYfp55wpuKcg==");


            CloudTableClient.CreateTablesFromModel(
                typeof(RecordCollectionContext),
                storageAccount.TableEndpoint.AbsoluteUri,
                storageAccount.Credentials);
            
        }

    //  Instance Constructor
        public RecordCollectionDataSource()
        {
            this.context = new RecordCollectionContext(storageAccount.TableEndpoint.AbsoluteUri, storageAccount.Credentials);
            this.context.RetryPolicy = RetryPolicies.Retry(3, TimeSpan.FromSeconds(1));

        }



    //  Album Entity Queries
        public bool DeleteAlbumFromAlbumEntity(string albumToDelete, string byArtist)
        {
            bool result = false;

            if (albumToDelete != null && byArtist != null)
            {
                try
                {
                    foreach (AlbumEntity al in from als in this.context.AlbumEntity where als.PartitionKey == byArtist select als)
                    {
                        if (al.Album.ToUpper().Equals(albumToDelete.ToUpper()))
                        {
                            this.context.DeleteObject(al);
                            this.context.SaveChanges();
                            result = true;
                        }
                    }
                }
                catch (Exception) { }
            }
            return result;
        }
        public bool PutUpdateForAlbum(string albumToUpdate, string byArtist, string parameterToUpdate, string newValue)
        {
            bool outcome = false;

            if (albumToUpdate != null && byArtist != null && parameterToUpdate != null && newValue != null)
            {
                try
                {
                    var result = from al in this.context.AlbumEntity where al.PartitionKey == byArtist select al;
                    AlbumEntity entityToUpdate = result.FirstOrDefault<AlbumEntity>();

                    if (entityToUpdate.Album.ToUpper().Equals(albumToUpdate.ToUpper()))
                    {
                        if (parameterToUpdate.ToUpper().Equals("ALBUM")) { AlbumEntity updatedAlbum = new AlbumEntity { Album = newValue, Artist = entityToUpdate.Artist, AlbumValue = entityToUpdate.AlbumValue, Label = entityToUpdate.Label, Rating = entityToUpdate.Rating }; this.DeleteAlbumFromAlbumEntity(entityToUpdate.Album, entityToUpdate.Artist); this.context.SaveChanges(); this.AddAlbumToAlbumEntity(updatedAlbum); this.context.SaveChanges(); }
                        if (parameterToUpdate.ToUpper().Equals("ARTIST")) { AlbumEntity updatedAlbum = new AlbumEntity { Album = entityToUpdate.Album, Artist = newValue, AlbumValue = entityToUpdate.AlbumValue, Label = entityToUpdate.Label, Rating = entityToUpdate.Rating }; this.DeleteAlbumFromAlbumEntity(entityToUpdate.Album, entityToUpdate.Artist); this.context.SaveChanges(); this.AddAlbumToAlbumEntity(updatedAlbum); this.context.SaveChanges(); }
                        if (parameterToUpdate.ToUpper().Equals("LABEL")) { entityToUpdate.Label = newValue; }
                        if (parameterToUpdate.ToUpper().Equals("VALUE")) { entityToUpdate.AlbumValue = Convert.ToDouble(newValue); }
                        if (parameterToUpdate.ToUpper().Equals("RATING")) { entityToUpdate.Rating = Convert.ToDouble(newValue); }

                        if (parameterToUpdate.ToUpper().Equals("LABEL") || parameterToUpdate.ToUpper().Equals("VALUE") || parameterToUpdate.ToUpper().Equals("RATING"))
                        {
                            this.context.UpdateObject(entityToUpdate);
                            this.context.SaveChanges();
                            outcome = true;
                        }
                    }
                }
                catch (Exception) { }
            }
            return outcome;
        }
        public bool AddAlbumToAlbumEntity(AlbumEntity newAlbum)
        {
            bool result = false;

            if (newAlbum.Album != null)
            {
                try
                {
                    this.context.AddObject("AlbumEntity", newAlbum);
                    this.context.SaveChanges();
                    result = true;
                }
                catch(Exception) { }
            }
            return result;
        }
        public IEnumerable<AlbumEntity> GetAlbumFromAlbumEntity(bool showAllAlbums)
        {
            var results = from al in this.context.AlbumEntity select al;
            return results;
        }
        public string GetArtistFromAlbum(string artistFromAlbum)
        {
            string artist = "Artist Not Found";

            foreach (var al in this.context.AlbumEntity)
            {
                if (al.Album.ToUpper().Equals(artistFromAlbum.ToUpper()) && al.Album != null)
                {
                    artist = al.Artist;
                }
            }

            return artist;
        }
        public IEnumerable<String> GetAlbumsFromArtist(string allAlbumsByArtist)
        {
            List<String> albumList = new List<String>();

            bool foundMatch = false;
            foreach (var al in this.context.AlbumEntity)
            {
                if (al.Artist.ToUpper().Equals(allAlbumsByArtist.ToUpper()) && al.Artist != null)
                {
                    foundMatch = true;
                    albumList.Add(al.Album);
                }
            }
            if (!foundMatch)
            {
                albumList.Add("No Artist Found, with name: " + allAlbumsByArtist);
            }

            return albumList;
        }
        public IEnumerable<AlbumEntity> GetAlbumsInOrderOfExpense(bool showAllAlbumsHighLow)
        {
            List<AlbumEntity> sortedList = new List<AlbumEntity>();
            AlbumEntity temp = new AlbumEntity();
            foreach (AlbumEntity al in this.context.AlbumEntity)
            {
                if (sortedList.Count() < 1)
                {
                    sortedList.Add(al);
                }
                else
                {
                    sortedList.Add(al);
                    for (int i = 1; i < sortedList.Count(); ++i)
                    {
                        //  Check that  ...     && i+1 is still less than the list size, otherwise there'll be an out of bounds execption && perform swap if the value last is higher than the previous value.
                        if (sortedList[sortedList.Count() - i].AlbumValue > sortedList[sortedList.Count() - (i+1)].AlbumValue)
                        {
                            temp = sortedList[sortedList.Count() - (i+1)];
                            sortedList[sortedList.Count() - (i+1)] = sortedList[sortedList.Count() - i];
                            sortedList[sortedList.Count() - i] = temp;
                        }
                    }
                }
            }
            return sortedList;
        }
        public IEnumerable<AlbumEntity> GetTopRatedAlbums(bool showTopRatedAlbums)
        {
            List<AlbumEntity> sortedList = new List<AlbumEntity>();
            AlbumEntity temp = new AlbumEntity();
            foreach (AlbumEntity al in this.context.AlbumEntity)
            {
                if (sortedList.Count() < 1)
                {
                    sortedList.Add(al);
                }
                else
                {
                    sortedList.Add(al);
                    for (int i = 1; i < sortedList.Count(); ++i)
                    {
                        //  Check that  ...     && i+1 is still less than the list size, otherwise there'll be an out of bounds execption && perform swap if the value last is higher than the previous value.
                        if (sortedList[sortedList.Count() - i].Rating > sortedList[sortedList.Count() - (i + 1)].Rating)
                        {
                            temp = sortedList[sortedList.Count() - (i + 1)];
                            sortedList[sortedList.Count() - (i + 1)] = sortedList[sortedList.Count() - i];
                            sortedList[sortedList.Count() - i] = temp;
                        }
                    }
                }
            }
            return sortedList;
        }

    //  Genre Entity Queries
        public bool DeleteGenreFromGenreEntity(string albumToDelete, string byArtist)
        {
            bool result = false;
            foreach (GenreEntity ge in from ges in this.context.GenreEntity where ges.PartitionKey == byArtist select ges)
            {
                if (ge.Album.ToUpper().Equals(albumToDelete.ToUpper()))
                {
                    this.context.DeleteObject(ge);
                    this.context.SaveChanges();
                    result = true;
                }
            }
            return result;
        }
        public bool PutUpdateForGenre(string albumToUpdate, string byArtist, string parameterToUpdate, string newValue)
        {
            bool wasSuccessful = false;
            var result = from ge in this.context.GenreEntity where ge.PartitionKey == byArtist select ge;
            GenreEntity entityToUpdate = result.FirstOrDefault<GenreEntity>();

            if (entityToUpdate.Album.ToUpper().Equals(albumToUpdate.ToUpper()))
            {
                if (parameterToUpdate.ToUpper().Equals("ALBUM")) { GenreEntity updatedAlbum = new GenreEntity { Album = newValue, Artist = entityToUpdate.Artist, Genre_01 = entityToUpdate.Genre_01, Genre_02 = entityToUpdate.Genre_02, Genre_03 = entityToUpdate.Genre_03 }; this.DeleteGenreFromGenreEntity(entityToUpdate.Album, entityToUpdate.Artist); this.context.SaveChanges(); this.AddGenreToGenreEntity(updatedAlbum); this.context.SaveChanges(); }
                if (parameterToUpdate.ToUpper().Equals("ARTIST")) { GenreEntity updatedAlbum = new GenreEntity { Album = entityToUpdate.Album, Artist = newValue, Genre_01 = entityToUpdate.Genre_01, Genre_02 = entityToUpdate.Genre_02, Genre_03 = entityToUpdate.Genre_03 }; this.DeleteGenreFromGenreEntity(entityToUpdate.Album, entityToUpdate.Artist); this.context.SaveChanges(); this.AddGenreToGenreEntity(updatedAlbum); this.context.SaveChanges(); }
                if (parameterToUpdate.ToUpper().Equals("GENRE1")) { entityToUpdate.Genre_01 = newValue; }
                if (parameterToUpdate.ToUpper().Equals("GENRE2")) { entityToUpdate.Genre_02 = newValue; }
                if (parameterToUpdate.ToUpper().Equals("GENRE3")) { entityToUpdate.Genre_03 = newValue; }

                if (parameterToUpdate.ToUpper().Equals("GENRE1") || parameterToUpdate.ToUpper().Equals("GENRE2") || parameterToUpdate.ToUpper().Equals("GENRE3"))
                {
                    this.context.UpdateObject(entityToUpdate);
                    this.context.SaveChanges();
                    wasSuccessful = true;
                }
            }
            return wasSuccessful;
        }
        public bool AddGenreToGenreEntity(GenreEntity newGenre)
        {
            bool result = false;

            if(newGenre.Album != null)
            {
                try
                {
                    this.context.AddObject("GenreEntity", newGenre);
                    this.context.SaveChanges();
                    result = true;
                }
                catch(Exception) { }
            }

            return result;
        }
        public IEnumerable<GenreEntity> GetGenreFromGenreEntity(bool showAllGenres)
        {
            var results = from ge in this.context.GenreEntity select ge;
            return results;
        }
        public IEnumerable<string> GetAlbumsFromGenres(string genre01, string genre02, string genre03)
        {
            bool foundMatch = false;
            List<String> albumList = new List<String>();

            foreach (var ge in this.context.GenreEntity)
            {
                if (ge.Genre_02 != null && ge.Genre_03 != null)     //  If a GenreModel object contains a Genre_02 and Genre_03 property, then carry out this search.  If this check was not there, the program would crash as it would be check a string on a NULL property.
                {
                    //  Check all genre args against all genres in each position within the Azure Table
                    if (
                        ge.Genre_01.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre03.ToUpper()) ||
                        ge.Genre_02.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_02.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_02.ToUpper().Equals(genre03.ToUpper()) ||
                        ge.Genre_03.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_03.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_03.ToUpper().Equals(genre03.ToUpper())
                       )
                    {
                        foundMatch = true;
                        albumList.Add(ge.Album);
                    }
                }
                else if (ge.Genre_02 != null && ge.Genre_03 == null)
                {
                    if (
                        ge.Genre_01.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre03.ToUpper()) ||
                        ge.Genre_02.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_02.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_02.ToUpper().Equals(genre03.ToUpper())
                       )
                    {
                        foundMatch = true;
                        albumList.Add(ge.Album);
                    }

                }
                else if (ge.Genre_02 == null && ge.Genre_03 != null)
                {
                    if (
                        ge.Genre_01.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre03.ToUpper()) ||
                        ge.Genre_03.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_03.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_03.ToUpper().Equals(genre03.ToUpper())
                       )
                    {
                        foundMatch = true;
                        albumList.Add(ge.Album);
                    }

                }
                else
                {
                    if (ge.Genre_01.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre03.ToUpper()))
                    {
                        foundMatch = true;
                        albumList.Add(ge.Album);
                    }

                }
            }
            if (!foundMatch)
            {
                if (genre02.Equals("") && genre03.Equals(""))
                {
                    albumList.Add("No Genre(s) Found: " + genre01);
                }
                else if (!genre02.Equals("") && genre03.Equals(""))
                {
                    albumList.Add("No Genre(s) Found: " + genre01 + ", " + genre02);
                }
                else if (genre02.Equals("") && !genre03.Equals(""))
                {
                    albumList.Add("No Genre(s) Found: " + genre01 + ", " + genre03);
                }
                else
                {
                    albumList.Add("No Genre(s) Found: " + genre01 + ", " + genre02 + ", " + genre03);
                }
            }

            return albumList;
        }

    //  Track Entity Queries
        public bool DeleteTrackFromTrackEntity(string albumToDelete, string byArtist)
        {
            bool result = false;
            foreach (TrackEntity tr in from trs in this.context.TrackEntity where trs.PartitionKey == byArtist select trs)
            {
                if (tr.Album.ToUpper().Equals(albumToDelete.ToUpper()))
                {
                    this.context.DeleteObject(tr);
                    this.context.SaveChanges();
                    result = true;
                }
            }
            return result;
        }
        public bool PutUpdateForTrack(string albumToUpdate, string byArtist, string parameterToUpdate, string newValue)
        {
            bool wasSuccessful = false;
            var result = from tr in this.context.TrackEntity where tr.PartitionKey == byArtist select tr;
            TrackEntity entityToUpdate = result.FirstOrDefault<TrackEntity>();

            if (entityToUpdate.Album.ToUpper().Equals(albumToUpdate.ToUpper()))
            {
                if (parameterToUpdate.ToUpper().Equals("ALBUM")) { TrackEntity updatedAlbum = new TrackEntity { Album = newValue, Artist = entityToUpdate.Artist, NumberOfTracks = entityToUpdate.NumberOfTracks, Track_01_Title = entityToUpdate.Track_01_Title, Track_02_Title = entityToUpdate.Track_02_Title, Track_03_Title = entityToUpdate.Track_03_Title, Track_04_Title = entityToUpdate.Track_04_Title, Track_05_Title = entityToUpdate.Track_05_Title }; this.DeleteTrackFromTrackEntity(entityToUpdate.Album, entityToUpdate.Artist); this.context.SaveChanges(); this.AddTrackToTrackEntity(updatedAlbum); this.context.SaveChanges(); }
                if (parameterToUpdate.ToUpper().Equals("ARTIST")) { TrackEntity updatedAlbum = new TrackEntity { Album = entityToUpdate.Album, Artist = newValue, NumberOfTracks = entityToUpdate.NumberOfTracks, Track_01_Title = entityToUpdate.Track_01_Title, Track_02_Title = entityToUpdate.Track_02_Title, Track_03_Title = entityToUpdate.Track_03_Title, Track_04_Title = entityToUpdate.Track_04_Title, Track_05_Title = entityToUpdate.Track_05_Title }; this.DeleteTrackFromTrackEntity(entityToUpdate.Album, entityToUpdate.Artist); this.context.SaveChanges(); this.AddTrackToTrackEntity(updatedAlbum); this.context.SaveChanges(); }
                if (parameterToUpdate.ToUpper().Equals("NUMOFTRACKS"))  { entityToUpdate.NumberOfTracks = Convert.ToInt32(newValue); }
                if (parameterToUpdate.ToUpper().Equals("TRACK1"))       { entityToUpdate.Track_01_Title = newValue; }
                if (parameterToUpdate.ToUpper().Equals("TRACK2"))       { entityToUpdate.Track_02_Title = newValue; }
                if (parameterToUpdate.ToUpper().Equals("TRACK3"))       { entityToUpdate.Track_03_Title = newValue; }
                if (parameterToUpdate.ToUpper().Equals("TRACK4"))       { entityToUpdate.Track_04_Title = newValue; }
                if (parameterToUpdate.ToUpper().Equals("TRACK5"))       { entityToUpdate.Track_05_Title = newValue; }

                if (parameterToUpdate.ToUpper().Equals("NUMOFTRACKS") || parameterToUpdate.ToUpper().Equals("TRACK1") || parameterToUpdate.ToUpper().Equals("TRACK2") || parameterToUpdate.ToUpper().Equals("TRACK3") || parameterToUpdate.ToUpper().Equals("TRACK4") || parameterToUpdate.ToUpper().Equals("TRACK5"))
                {
                    this.context.UpdateObject(entityToUpdate);
                    this.context.SaveChanges();
                    wasSuccessful = true;
                }
            }
            return wasSuccessful;
        }
        public bool AddTrackToTrackEntity(TrackEntity newTrack)
        {
            bool result = false;
            if(newTrack.Album != null)
            {
                try
                {
                    this.context.AddObject("TrackEntity", newTrack);
                    this.context.SaveChanges();
                    result = true;
                }
                catch (Exception) { }
            }
            return result;
        }
        public IEnumerable<TrackEntity> GetTrackFromTrackEntity(bool showAllTracks)
        {
            var results = from tr in this.context.TrackEntity select tr;
            return results;
        }
        public IEnumerable<string> GetTracksFromAlbum(string album)
        {
            bool foundMatch = false;
            List<String> trackList = new List<String>();

            foreach (TrackEntity tr in this.context.TrackEntity)
            {
                if (tr.Album.ToUpper().Equals(album.ToUpper()) && tr.Album != null)
                {
                    foundMatch = true;
                    if (tr.Track_01_Title != null) { trackList.Add("Track 01: " + tr.Track_01_Title); }
                    if (tr.Track_02_Title != null) { trackList.Add("Track 02: " + tr.Track_02_Title); }
                    if (tr.Track_03_Title != null) { trackList.Add("Track 03: " + tr.Track_03_Title); }
                    if (tr.Track_04_Title != null) { trackList.Add("Track 04: " + tr.Track_04_Title); }
                    if (tr.Track_05_Title != null) { trackList.Add("Track 05: " + tr.Track_05_Title); }
                }
            }
            if(foundMatch == false)
            {
                trackList.Add("No Album found by the name of " + album);
            }

            return trackList;
        }
    }
}