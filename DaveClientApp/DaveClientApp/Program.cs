using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

using DaveClientApp.Data_Classes;


namespace DaveClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:2454/");                             // base URL for API Controller i.e. RESTFul service

                // add an Accept header for JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //  Vars
                HttpResponseMessage response = new HttpResponseMessage();
                AlbumModel newAlbum = new AlbumModel() { Artist = "Dave Nolan", Album = "Album", DiscNumber = 1, Label = "Unsigned", AlbumValue = 12.50 };
                GenreData newGenre = new GenreData() { Album = "Album", Artist = "Dave Nolan", DiscNumber = 1, Genre_01 = "Techno", Genre_02 = "House", Genre_03 = "Electronic" };
                TrackData newTrack = new TrackData() { Album = "Album", Artist = "Dave Nolan", DiscNumber = 1, NumberOfTracks = 5, Track_01_Title = "Track01", Track_02_Title = "Track02", Track_03_Title = "Track03", Track_04_Title = "Track04", Track_05_Title = "Track05" };
                //HttpResponseMessage responseAlbum;
                //HttpResponseMessage responseGenre;
                //HttpResponseMessage responseTrack;


    //  POST - New Album
                //response = client.PostAsJsonAsync("api/RecordCollection/", newAlbum).Result;
                //if (response.IsSuccessStatusCode)                                               // 200 .. 299
                //{
                //    Uri newStockUri = response.Headers.Location;
                //    //Console.WriteLine("URI for new resource: " + newStockUri.ToString());
                //}
                //else
                //{
                //    Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                //}
    //  Post - New Genre

    //  Post - New Track

    //  GET - 01
                response = client.GetAsync("api/RecordCollection").Result;
                if (response.IsSuccessStatusCode)
                {
                    // read result 

                    //List<Albumclientapp.AlbumData> albums = new List<Albumclientapp.AlbumData>();
                    var albums = response.Content.ReadAsAsync<IEnumerable<AlbumModel>>().Result;
                    //var albums = responseAlbum.Content.ReadAsAsync<IEnumerable<AlbumData>>().Result;
                    foreach (var al in albums)
                    {
                        Console.WriteLine(al.Album + " - " + al.Artist);
                    }
                }
                else
                {
                    Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                }
    //  GET - 02
                //string albumName = "trouser jazz";
                //string artistBool = "true";
                //response = client.GetAsync("api/recordcollection?albumName=" + albumName + "&getArtist=" + artistBool).Result;
                //if (response.IsSuccessStatusCode)
                //{
                //     read result 
                //    var artist = response.Content.ReadAsAsync<String>().Result;

                //        Console.WriteLine(artist);

                //}
                //else
                //{
                //    Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                //}


    //  PUT

    //  DELETE
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
