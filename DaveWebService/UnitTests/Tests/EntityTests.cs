using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.StorageClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DaveWebService.Controllers;
using DaveWebService.Entity;

namespace UnitTests
{
    [TestClass]
    public class EntityTests
    {
  
//  ALBUM ENTITY TESTS
       
        [TestMethod]
        public void Test_GetAllAlbums_Success()
        {
            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            IEnumerable<AlbumEntity> result = rds.GetAlbumFromAlbumEntity(true);
            foreach(AlbumEntity al in result)
            {
                Assert.IsInstanceOfType(al.Album, typeof(string));
            }  
        }

        [TestMethod]
        public void Test_PostAddAlbumAndDeleteAlbum_Success()
        {
            AlbumEntity newAlbum = new AlbumEntity() { Artist = "Dave Nolan", Album = "Daveys Hits", Label = "Unsigned", AlbumValue = 12.50, Rating = 4.1 };

            RecordCollectionDataSource rds = new RecordCollectionDataSource(); 
            bool addResult = rds.AddAlbumToAlbumEntity(newAlbum);
            Assert.IsTrue(addResult);
            bool deleteResult = rds.DeleteAlbumFromAlbumEntity("Daveys Hits", "Dave Nolan");
            Assert.IsTrue(deleteResult);
        }

        [TestMethod]
        public void Test_PutUpdateForAlbumAndDelete_Success()
        {
            AlbumEntity newAlbum = new AlbumEntity() { Artist = "Dave Nolan", Album = "Daveys Hits", Label = "Unsigned", AlbumValue = 12.50, Rating = 4.1 };

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool addResult = rds.AddAlbumToAlbumEntity(newAlbum);
            Assert.IsTrue(addResult);
            bool putResult = rds.PutUpdateForAlbum("Daveys Hits", "Dave Nolan", "LABEL", "unknown");
            Assert.IsTrue(putResult);
            bool deleteResult = rds.DeleteAlbumFromAlbumEntity("Daveys Hits", "Dave Nolan");
            Assert.IsTrue(deleteResult);
        }

        [TestMethod]
        public void Test_PostAddAlbum_Fail()
        {
            AlbumEntity newAlbum = new AlbumEntity();

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool albumResult = rds.AddAlbumToAlbumEntity(newAlbum);
            Assert.IsFalse(albumResult);
        }

        [TestMethod]
        public void Test_DeleteAlbum_Fail()
        {
            string arg1 = null, arg2 = null;
            RecordCollectionDataSource rds = new RecordCollectionDataSource(); 
            bool deleteResult = rds.DeleteAlbumFromAlbumEntity(arg1, arg2);
            Assert.IsFalse(deleteResult);
        }

//  GENRE ENTITY TESTS

        [TestMethod]
        public void Test_PostAddGenre_Success()
        {
            GenreEntity newGenre = new GenreEntity() { Artist = "Dave Nolan", Album = "Daveys Hits", Genre_01 = "Techno", Genre_02 = "House", Genre_03 = "Electronic" };

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool genreResult = rds.AddGenreToGenreEntity(newGenre);
            Assert.IsTrue(genreResult);
            rds.DeleteGenreFromGenreEntity("Daveys Hits", "Dave Nolan");
        }

        [TestMethod]
        public void Test_PostAddGenre_Fail()
        {
            GenreEntity newGenre = new GenreEntity();

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool genreResult = rds.AddGenreToGenreEntity(newGenre);
            Assert.IsFalse(genreResult);
        }

//  TRACK ENTITY TESTS

        [TestMethod]
        public void Test_PostAddTrack_Success()
        {
            TrackEntity newTrack = new TrackEntity() { Artist = "Dave Nolan", Album = "Daveys Hits", NumberOfTracks = 5, Track_01_Title = "Track01", Track_02_Title = "Track02", Track_03_Title = "Track03", Track_04_Title = "Track04", Track_05_Title = "Track05" };

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool trackResult = rds.AddTrackToTrackEntity(newTrack);
            Assert.IsTrue(trackResult);
            rds.DeleteTrackFromTrackEntity("Daveys Hits", "Dave Nolan");
        }

        [TestMethod]
        public void Test_PostAddTrack_Fail()
        {
            TrackEntity newTrack = new TrackEntity();

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool trackResult = rds.AddTrackToTrackEntity(newTrack);
            Assert.IsFalse(trackResult);
        }
    }
}
