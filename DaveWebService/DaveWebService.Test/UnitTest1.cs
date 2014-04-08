using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DaveWebService.Models;
using DaveWebService.Controllers;

namespace DaveWebService.Test
{
    [TestClass]
    public class UnitTest1
    {
        static List<AlbumModel> albums = new List<AlbumModel>() 
        { 
            new AlbumModel { Album = "EP-01", Artist = "Dave Nolan", Label = "Unsigned", AlbumValue = 10.50, DiscNumber = 1 },
            new AlbumModel { Album = "AI:TM", Artist = "Dave Nolan", Label = "Unsigned", AlbumValue = 0.00, DiscNumber = 1 },
            new AlbumModel { Album = "Computer World", Artist = "Kraftwerk", Label = "EMI", AlbumValue = 12.00, DiscNumber = 1 }, 
            new AlbumModel { Album = "Trouser Jazz", Artist = "Mr. Scruff", Label = "Ninja Tune", AlbumValue = 11.25, DiscNumber = 1 },
        };

        static List<TrackModel> tracks = new List<TrackModel>()
        {
            new TrackModel {NumberOfTracks = 3, Track_01_Title = "M01234", Track_02_Title = "Dub Track", Track_03_Title = "It's Raining", Artist = "Dave Nolan", Album =  "EP-01", DiscNumber = 1 },
            new TrackModel {NumberOfTracks = 3, Track_01_Title = "dave01", Track_02_Title = "dave2", Track_03_Title = "dave3", Artist = "Dave Nolan", Album =  "EP-01", DiscNumber = 2 },
            new TrackModel {NumberOfTracks = 3, Track_01_Title = "Computer World", Track_02_Title = "Pocket Calculator", Track_03_Title = "Numbers", Artist = "Kraftwerk", Album = "Computer World", DiscNumber = 1},
            new TrackModel {NumberOfTracks = 2, Track_01_Title = "	Here We Go", Track_02_Title = "Sweetsmoke", Artist = "Mr. Scruff", Album = "Trouser Jazz", DiscNumber = 1}
        };

        static List<GenreModel> genres = new List<GenreModel>()
        {
            new GenreModel {Artist = "Dave Nolan", Album = "EP-01", DiscNumber = 1, Genre_01 = "Deep House", Genre_02 = "Tech House"},
            new GenreModel {Artist = "Kraftwerk", Album = "Computer World", DiscNumber = 1, Genre_01 = "Electronic"},
            new GenreModel {Artist = "Mr. Scruff", Album = "Trouser Jazz", DiscNumber = 1, Genre_01 = "Funk", Genre_02 = "Future Jazz"}
        };

        MasterModel mm = new MasterModel(albums[0], genres[0], tracks[0]);

        [TestMethod]
        public void TestMethod1()
        {
            RecordCollectionController rc = new RecordCollectionController();
            HttpResponseMessage response = rc.PostAddAlbum(mm);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
