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

            
            
            //string album = "Simon's Album", artist = "Simon", label = "Simon's Label";
            //int rating = 1;
            //double value = 11.20;

            //var albumEntity = new AlbumEntity()
            //{
            //    Album = album,
            //    Artist = artist,
            //    AlbumValue = value,
            //    Label = label,
            //    Rating = rating
            //};

            //var albumDataSource = new RecordCollectionDataSource();
            //albumDataSource.AddAlbumToAlbumEntity(albumEntity);
            //var entries = albumDataSource.GetAlbumFromAlbumEntity();
            //foreach (var entry in entries)
            //{
            //    //Console.WriteLine(entry.Album + "~by~" + entry.Artist);
            //    System.Diagnostics.Debug.WriteLine(entry.Album + "~by~" + entry.Artist);

            //}

            return View();
        }

        public ActionResult WebPage2()
        {
            return View();
        }

    }
}
