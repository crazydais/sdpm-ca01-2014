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
                AlbumModel newAlbum = new AlbumModel() { Artist = "Dave Nolan", Album = "Daveys Hits", DiscNumber = 1, Label = "Unsigned", AlbumValue = 12.50 };
                GenreModel newGenre = new GenreModel() { Album = "Album", Artist = "Dave Nolan", DiscNumber = 1, Genre_01 = "Techno", Genre_02 = "House", Genre_03 = "Electronic" };
                TrackModel newTrack = new TrackModel() { Album = "Album", Artist = "Dave Nolan", DiscNumber = 1, NumberOfTracks = 5, Track_01_Title = "Track01", Track_02_Title = "Track02", Track_03_Title = "Track03", Track_04_Title = "Track04", Track_05_Title = "Track05" };
                MasterModel newMaster = new MasterModel() { MasterAlbum = newAlbum, MasterGenre = newGenre, MasterTrack = newTrack };
                //HttpResponseMessage responseAlbum;
                //HttpResponseMessage responseGenre;
                //HttpResponseMessage responseTrack;


                //  POST - New Master (Master encapsulates Album, Genre, and Track model classes)
                response = client.PostAsJsonAsync("api/RecordCollection/", newMaster).Result;
                if (response.IsSuccessStatusCode)                                               // 200 .. 299
                {
                    //  This line is not working for some reason... //Uri newMasterUri = response.Headers.Location.AbsoluteUri;          //response.Headers.Location;
                    Console.WriteLine("POST method was SUCCESSFUL"); //+ response.Headers.Location.AbsoluteUri);
                }
                else
                {
                    Console.WriteLine("POST method was NOT successful, " + response.StatusCode + " " + response.ReasonPhrase);
                }

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
                //PUT  api/RecordCollection?classType={classType}&albumId={albumId}&parameter={parameter}&update={update}
                string classType = "ALBUM", albumId = "Daveys Hits", parameter = "ALBUM", update = "Electronic Sounds";          //  This is to update 
                response = client.PutAsJsonAsync("api/RecordCollection?classType=" + classType + "&albumId=" + albumId + "&parameter=" + parameter + "&update=" + update, "").Result;

                if (response.IsSuccessStatusCode)                                               // 200 .. 299
                {
                    //  This line is not working for some reason... //Uri newMasterUri = response.Headers.Location.AbsoluteUri;          //response.Headers.Location;
                    Console.WriteLine("PUT method was SUCCESSFUL"); //+ response.Headers.Location.AbsoluteUri);
                }
                else
                {
                    Console.WriteLine("PUT method was NOT successful" + response.StatusCode + " - " + response.ReasonPhrase + " - " + response.Content.ToString());
                }

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
                    Console.WriteLine(response.StatusCode + " - " + response.ReasonPhrase + " - " + response.Content);
                }

    //  DELETE

                //  DELETE api/RecordCollection?album={album}  
                albumId = "Trouser Jazz";          
                response = client.DeleteAsync("api/RecordCollection?album=" + albumId).Result;

                if (response.IsSuccessStatusCode)                                               // 200 .. 299
                {
                    //  This line is not working for some reason... //Uri newMasterUri = response.Headers.Location.AbsoluteUri;          //response.Headers.Location;
                    Console.WriteLine("DELETE method was SUCCESSFUL"); //+ response.Headers.Location.AbsoluteUri);
                }
                else
                {
                    Console.WriteLine("DELETE method was NOT successful" + response.StatusCode + " - " + response.ReasonPhrase + " - " + response.Content.ToString());
                }


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
                    Console.WriteLine(response.StatusCode + " - " + response.ReasonPhrase + " - " + response.Content);
                }






            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
