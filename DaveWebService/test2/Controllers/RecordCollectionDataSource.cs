using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

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
            storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

            CloudTableClient.CreateTablesFromModel(
                typeof(RecordCollectionContext),
                storageAccount.TableEndpoint.AbsoluteUri,
                storageAccount.Credentials);
        }

       //   Instance Constructor
        public RecordCollectionDataSource()
        {
            this.context = new RecordCollectionContext(storageAccount.TableEndpoint.AbsoluteUri, storageAccount.Credentials);
            this.context.RetryPolicy = RetryPolicies.Retry(3, TimeSpan.FromSeconds(1));

        }

        //  Album Entity Queries
        public void AddAlbumToAlbumEntity(AlbumEntity newAlbum)
        {
            this.context.AddObject("AlbumEntity", newAlbum);
            this.context.SaveChanges();
        }
        public IEnumerable<AlbumEntity> GetAlbumFromAlbumEntity()
        {
            var results = from al in this.context.AlbumEntity select al;
            return results;
        }
        public string GetArtistFromAlbum(string album)
        {
            string artist = "Artist Not Found";

            foreach (var al in this.context.AlbumEntity)
            {
                if(al.Album.ToUpper().Equals(album.ToUpper()) && al.Album != null)
                {
                    artist = al.Artist;
                }
            }

            return artist;
        }
        public IEnumerable<String> GetAlbumsFromArtist(string artist)
        {
            List<String> albumList = new List<String>();

            bool foundMatch = false;
            foreach (var al in this.context.AlbumEntity)
            {
                if (al.Artist.ToUpper().Equals(artist.ToUpper()) && al.Artist != null)
                {
                    foundMatch = true;
                    albumList.Add(al.Album);
                }
            }
            if (!foundMatch)
            {
                albumList.Add("No Artist Found, with name: " + artist);
            }

            return albumList;
        }

        //  Genre Entity Queries
        public void AddGenreToGenreEntity(GenreEntity newGenre)
        {
            this.context.AddObject("GenreEntity", newGenre);
            this.context.SaveChanges();
        }
        public IEnumerable<GenreEntity> GetGenreFromGenreEntity()
        {
            var results = from ge in this.context.GenreEntity
                          select ge;
            return results;
        }

        //  Track Entity Queries
        public void AddTrackToTrackEntity(TrackEntity newTrack)
        {
            this.context.AddObject("TrackEntity", newTrack);
            this.context.SaveChanges();
        }
        public IEnumerable<TrackEntity> GetTrackFromTrackEntity()
        {
            var results = from tr in this.context.TrackEntity
                          select tr;
            return results;
        }
    }
}