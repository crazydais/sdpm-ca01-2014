using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DaveWebService.Controllers;
using DaveWebService.Models;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        //  Setup Vars
        

        [TestMethod]
        public void Test_Success_PostAddAlbum()
        {
            AlbumModel newAlbum = new AlbumModel() { Artist = "Dave Nolan", Album = "Daveys Hits", DiscNumber = 1, Label = "Unsigned", AlbumValue = 12.50 };
            GenreModel newGenre = new GenreModel() { Album = "Album", Artist = "Dave Nolan", DiscNumber = 1, Genre_01 = "Techno", Genre_02 = "House", Genre_03 = "Electronic" };
            TrackModel newTrack = new TrackModel() { Album = "Album", Artist = "Dave Nolan", DiscNumber = 1, NumberOfTracks = 5, Track_01_Title = "Track01", Track_02_Title = "Track02", Track_03_Title = "Track03", Track_04_Title = "Track04", Track_05_Title = "Track05" };
            MasterModel mm = new MasterModel(newAlbum, newGenre, newTrack);
            MasterModel mm2 = new MasterModel();

            RecordCollectionController rc = new RecordCollectionController();
            mm2 = rc.PostAddAlbum(mm);
            Assert.AreEqual(mm.MasterAlbum.Album.ToString(), mm2.MasterAlbum.Album.ToString());
            Assert.AreEqual(mm.MasterGenre.Genre_01.ToString(), mm2.MasterGenre.Genre_01.ToString());
            Assert.AreEqual(mm.MasterTrack.Track_01_Title.ToString(), mm2.MasterTrack.Track_01_Title.ToString());
        }

        [TestMethod]
        public void Test_Fail_PostAddAlbum()
        {
            AlbumModel newAlbum = new AlbumModel() { Artist = "", Album = "Daveys Hits", DiscNumber = 1, Label = "Unsigned", AlbumValue = 12.50 };
            GenreModel newGenre = new GenreModel() { Album = "Album", Artist = "Dave Nolan", DiscNumber = 1, Genre_01 = "Techno", Genre_02 = "House", Genre_03 = "Electronic" };
            TrackModel newTrack = new TrackModel() { Album = "Album", Artist = "Dave Nolan", DiscNumber = 1, NumberOfTracks = 5, Track_01_Title = "Track01", Track_02_Title = "Track02", Track_03_Title = "Track03", Track_04_Title = "Track04", Track_05_Title = "Track05" };
            MasterModel mm = new MasterModel(newAlbum, newGenre, newTrack);
            MasterModel mm2 = new MasterModel();
            mm.MasterAlbum.Album = "";

            RecordCollectionController rc = new RecordCollectionController();
            mm2 = rc.PostAddAlbum(mm);
            Assert.AreNotSame("", mm2.MasterAlbum.Album);
        }
    }
}
