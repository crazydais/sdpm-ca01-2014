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
        public void Test_GetArtistFromAlbum_Success()
        {
            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            string result = rds.GetArtistFromAlbum("Trouser Jazz");
            Assert.AreEqual(result.ToUpper(), "MR. SCRUFF");
        }

        [TestMethod]
        public void Test_GetArtistFromAlbum_Fail()
        {
            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            string result = rds.GetArtistFromAlbum("abcde");
            Assert.AreEqual(result, "Artist Not Found");
        }

        [TestMethod]
        public void Test_GetAlbumsFromArtist_Success()
        {
            AlbumEntity newAlbum1 = new AlbumEntity() { Artist = "Test Artist", Album = "Album1", Label = "Unsigned", AlbumValue = 1.0, Rating = 2.0 };
            AlbumEntity newAlbum2 = new AlbumEntity() { Artist = "Test Artist", Album = "Album2", Label = "Unsigned", AlbumValue = 3.0, Rating = 4.0 };
            
            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            rds.AddAlbumToAlbumEntity(newAlbum1);
            rds.AddAlbumToAlbumEntity(newAlbum2);

            IEnumerable<String> results = rds.GetAlbumsFromArtist("Test Artist");
            int i = 1;
            foreach(String al in results)
            {
                if (i == 1) { Assert.AreEqual(al, "Album1"); }
                if (i == 2) { Assert.AreEqual(al, "Album2"); }
                ++i; 
            }

            rds.DeleteAlbumFromAlbumEntity("Album1", "Test Artist");
            rds.DeleteAlbumFromAlbumEntity("Album2", "Test Artist");
        }

        [TestMethod]
        public void Test_GetAlbumsFromArtist_Fail()
        {
            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            IEnumerable<String> results = rds.GetAlbumsFromArtist("Test Artist");
            string al = results.First();
            Assert.AreEqual(al, "No Artist Found, with name: Test Artist");
        }

        [TestMethod]
        public void Test_GetAlbumsInOrderOfExpense_Success()
        {
            AlbumEntity test1 = new AlbumEntity(){Artist = "TestArtist1", Album = "TestAlbum1", AlbumValue = 7.50, Label = "TestLabel", Rating = 1.2};
            AlbumEntity test2 = new AlbumEntity(){Artist = "TestArtist2", Album = "TestAlbum2", AlbumValue = 8.60, Label = "TestLabel", Rating = 2.3};
            AlbumEntity test3 = new AlbumEntity(){Artist = "TestArtist3", Album = "TestAlbum3", AlbumValue = 9.70, Label = "TestLabel", Rating = 3.4};

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            rds.AddAlbumToAlbumEntity(test1);
            rds.AddAlbumToAlbumEntity(test2);
            rds.AddAlbumToAlbumEntity(test3);

            IEnumerable<AlbumEntity> result = rds.GetAlbumsInOrderOfExpense(true);
            AlbumEntity temp1 = result.ElementAt(0), temp2 = result.ElementAt(1), temp3 = result.ElementAt(2);
            Assert.IsTrue(temp1.AlbumValue > temp2.AlbumValue && temp2.AlbumValue > temp3.AlbumValue);

            rds.DeleteAlbumFromAlbumEntity("TestAlbum1", "TestArtist1");
            rds.DeleteAlbumFromAlbumEntity("TestAlbum2", "TestArtist2");
            rds.DeleteAlbumFromAlbumEntity("TestAlbum3", "TestArtist3");
        }

        [TestMethod]
        public void Test_GetTopRatedAlbums_Success()
        {
            AlbumEntity test1 = new AlbumEntity(){Artist = "TestArtist1", Album = "TestAlbum1", AlbumValue = 7.50, Label = "TestLabel", Rating = 1.2};
            AlbumEntity test2 = new AlbumEntity(){Artist = "TestArtist2", Album = "TestAlbum2", AlbumValue = 8.60, Label = "TestLabel", Rating = 2.3};
            AlbumEntity test3 = new AlbumEntity(){Artist = "TestArtist3", Album = "TestAlbum3", AlbumValue = 9.70, Label = "TestLabel", Rating = 3.4};

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            rds.AddAlbumToAlbumEntity(test1);
            rds.AddAlbumToAlbumEntity(test2);
            rds.AddAlbumToAlbumEntity(test3);

            IEnumerable<AlbumEntity> result = rds.GetTopRatedAlbums(true);
            AlbumEntity temp1 = result.ElementAt(0), temp2 = result.ElementAt(1), temp3 = result.ElementAt(2);
            Assert.IsTrue(temp1.Rating > temp2.Rating && temp2.AlbumValue > temp3.Rating);

            rds.DeleteAlbumFromAlbumEntity("TestAlbum1", "TestArtist1");
            rds.DeleteAlbumFromAlbumEntity("TestAlbum2", "TestArtist2");
            rds.DeleteAlbumFromAlbumEntity("TestAlbum3", "TestArtist3");
        }
        
        [TestMethod]
        public void Test_GetReport_Success()
        {
            WebServiceController ws = new WebServiceController();

            IEnumerable<String> result = ws.GetCollectionReport(true);
            Assert.IsTrue(result.First<String>().Equals("\tRecord Collection Report"));
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
        public void Test_PostAddAndDeleteGenre_Success()
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


        [TestMethod]
        public void Test_PutGenre_Success()
        {
            GenreEntity newGenre = new GenreEntity() { Artist = "Dave Nolan", Album = "Daveys Hits", Genre_01 = "Electronic", Genre_02 = "Techno", Genre_03 = "House" };

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool addResult = rds.AddGenreToGenreEntity(newGenre);
            Assert.IsTrue(addResult);
            bool putResult = rds.PutUpdateForGenre("Daveys Hits", "Dave Nolan", "genre1", "Pop");
            Assert.IsTrue(putResult);
            bool deleteResult = rds.DeleteGenreFromGenreEntity("Daveys Hits", "Dave Nolan");
            Assert.IsTrue(deleteResult);
        }

        
        [TestMethod]
        public void Test_GetGenre_Success()
        {
            GenreEntity newGenre = new GenreEntity() { Artist = "Dave Nolan", Album = "Daveys Hits", Genre_01 = "Electronic", Genre_02 = "Techno", Genre_03 = "House" };

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool addResult = rds.AddGenreToGenreEntity(newGenre);
            Assert.IsTrue(addResult);

            IEnumerable<GenreEntity> ge = rds.GetGenreFromGenreEntity(true);
            GenreEntity g = ge.First<GenreEntity>();
            Assert.IsNotNull(g.Album);

            bool deleteResult = rds.DeleteGenreFromGenreEntity("Daveys Hits", "Dave Nolan");
            Assert.IsTrue(deleteResult);
        }

        
        [TestMethod]
        public void Test_GetAlbumsFromGenres_Success()
        {
            GenreEntity newGenre = new GenreEntity() { Artist = "Dave Nolan", Album = "Daveys Hits", Genre_01 = "aaa", Genre_02 = "bbb", Genre_03 = "ccc" };

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool addResult = rds.AddGenreToGenreEntity(newGenre);
            Assert.IsTrue(addResult);

            IEnumerable<String> ge = rds.GetAlbumsFromGenres("aaa", "bbb", "ccc");
            String g = ge.First<String>();
            Assert.AreEqual(g, "Daveys Hits");

            bool deleteResult = rds.DeleteGenreFromGenreEntity("Daveys Hits", "Dave Nolan");
            Assert.IsTrue(deleteResult);
        }

//  TRACK ENTITY TESTS

        [TestMethod]
        public void Test_PostAddAndDeleteTrack_Success()
        {
            TrackEntity newTrack = new TrackEntity() { Artist = "Dave Nolan", Album = "Daveys Hits", NumberOfTracks = 5, Track_01_Title = "Track01", Track_02_Title = "Track02", Track_03_Title = "Track03", Track_04_Title = "Track04", Track_05_Title = "Track05" };

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool trackResult = rds.AddTrackToTrackEntity(newTrack);
            Assert.IsTrue(trackResult);
            rds.DeleteTrackFromTrackEntity("Daveys Hits", "Dave Nolan");
        }

                   
        [TestMethod]
        public void Test_PutTrack_Success()
        {
            TrackEntity newTrack = new TrackEntity() { Artist = "Dave Nolan", Album = "Daveys Hits", NumberOfTracks = 2, Track_01_Title = "track1", Track_02_Title = "track2", Track_03_Title = "", Track_04_Title = "", Track_05_Title = "" };

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool addResult = rds.AddTrackToTrackEntity(newTrack);
            Assert.IsTrue(addResult);
            bool putResult = rds.PutUpdateForTrack("Daveys Hits", "Dave Nolan", "TRACK1", "a different title");
            Assert.IsTrue(putResult);
            bool deleteResult = rds.DeleteTrackFromTrackEntity ("Daveys Hits", "Dave Nolan");
            Assert.IsTrue(deleteResult);
        }
        
        [TestMethod]
        public void Test_GetAllTrack_Success()
        {
            TrackEntity newTrack = new TrackEntity() { Artist = "Dave Nolan", Album = "Daveys Hits", NumberOfTracks = 2, Track_01_Title = "track1", Track_02_Title = "track2", Track_03_Title = "", Track_04_Title = "", Track_05_Title = "" };

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool addResult = rds.AddTrackToTrackEntity(newTrack);
            Assert.IsTrue(addResult);

            IEnumerable<TrackEntity> tr = rds.GetTrackFromTrackEntity(true);
            TrackEntity t = tr.First<TrackEntity>();
            Assert.IsNotNull(t.Album);

            bool deleteResult = rds.DeleteTrackFromTrackEntity("Daveys Hits", "Dave Nolan");
            Assert.IsTrue(deleteResult);
        }

        [TestMethod]
        public void Test_GetTrackFromAlbum_Success()
        {
            TrackEntity newTrack = new TrackEntity() { Artist = "Dave Nolan", Album = "Daveys Hits", NumberOfTracks = 2, Track_01_Title = "track1", Track_02_Title = "track2", Track_03_Title = "", Track_04_Title = "", Track_05_Title = "" };

            RecordCollectionDataSource rds = new RecordCollectionDataSource();
            bool addResult = rds.AddTrackToTrackEntity(newTrack);
            Assert.IsTrue(addResult);

            IEnumerable<String> tr = rds.GetTracksFromAlbum("Daveys Hits");
            String t = tr.First<String>();
            Assert.AreEqual(t, "Track 01: track1");

            bool deleteResult = rds.DeleteTrackFromTrackEntity("Daveys Hits", "Dave Nolan");
            Assert.IsTrue(deleteResult);

        }  
    
    
    }
}
