using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DaveWebService.Entity;

namespace DaveWebService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int currentHour = DateTime.Now.Hour;
            int currentMin = DateTime.Now.Minute;
            ViewBag.TimeMessage01 = String.Format("The time is: {0:00}:{1:00}", currentHour, currentMin);



            string album = "Simon's Album", artist = "Simon", label = "Unknown";
            double rating = 2.1, value = 4.20;
            string genre1 = "Shite", genre2 = "", genre3 = "";
            int numOftk = 2;
            string tr1 = "Some Song", tr2 = "Another Song";


            //var albumEntity = new AlbumEntity()
            //{
            //    Album = album,
            //    Artist = artist,
            //    AlbumValue = value,
            //    Label = label,
            //    Rating = rating
            //};

            //var genreEntity = new GenreEntity()
            //{
            //    Album = album,
            //    Artist = artist,
            //    Genre_01 = genre1,
            //    Genre_02 = genre2,
            //    Genre_03 = genre3
            //};

            //var trackEntity = new TrackEntity()
            //{
            //    Album = album,
            //    Artist = artist,
            //    NumberOfTracks = numOftk,
            //    Track_01_Title = tr1,
            //    Track_02_Title = tr2,
            //    Track_03_Title = "",
            //    Track_04_Title = "",
            //    Track_05_Title = ""
            //};

            //var albumDataSource = new RecordCollectionDataSource();
            //albumDataSource.AddAlbumToAlbumEntity(albumEntity);
            //albumDataSource.AddGenreToGenreEntity(genreEntity);
            //albumDataSource.AddTrackToTrackEntity(trackEntity);
            ////var entries = albumDataSource.GetAlbumFromAlbumEntity();
            ////foreach (var entry in entries)
            ////{
            ////    //Console.WriteLine(entry.Album + "~by~" + entry.Artist);
            ////    System.Diagnostics.Debug.WriteLine(entry.Album + "~by~" + entry.Artist);

            ////}

            return View();
        }

        public ActionResult WebPage2()
        {
            return View();
        }

    }
}
