using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

using DaveClientApp.DataClasses;
using DaveClientApp.Entity;
using DaveClientApp.HttpMethods;


namespace DaveClientApp
{
    class Program
    {
        static void Main()
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:2454/");                             // base URL for API Controller i.e. RESTFul service
                //client.BaseAddress = new Uri("http://daveyservice01.cloudapp.net/");
                
                // add an Accept header for JSON
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //  Vars
                HttpResponseMessage response = new HttpResponseMessage();
                //AlbumModel newAlbum = new AlbumModel() { Artist = "Dave Nolan", Album = "Daveys Hits", DiscNumber = 1, Label = "Unsigned", AlbumValue = 12.50 };
                //GenreModel newGenre = new GenreModel() { Album = "Album", Artist = "Dave Nolan", DiscNumber = 1, Genre_01 = "Techno", Genre_02 = "House", Genre_03 = "Electronic" };
                //TrackModel newTrack = new TrackModel() { Album = "Album", Artist = "Dave Nolan", DiscNumber = 1, NumberOfTracks = 5, Track_01_Title = "Track01", Track_02_Title = "Track02", Track_03_Title = "Track03", Track_04_Title = "Track04", Track_05_Title = "Track05" };
                //MasterModel newMaster = new MasterModel() { MasterAlbum = newAlbum, MasterGenre = newGenre, MasterTrack = newTrack };

                AlbumEntity newAlbum = new AlbumEntity() { Artist = "Dave Nolan", Album = "Daveys Hits", Label = "Unsigned", AlbumValue = 12.50 };
                GenreEntity newGenre = new GenreEntity() { Album = "Album", Artist = "Dave Nolan", Genre_01 = "Techno", Genre_02 = "House", Genre_03 = "Electronic" };
                TrackEntity newTrack = new TrackEntity() { Album = "Album", Artist = "Dave Nolan", NumberOfTracks = 5, Track_01_Title = "Track01", Track_02_Title = "Track02", Track_03_Title = "Track03", Track_04_Title = "Track04", Track_05_Title = "Track05" };

                //DaveClientHttpMethods Http = new DaveClientHttpMethods();
                WebServiceConnection Http = new WebServiceConnection();

                CreateMasterModel Create = new CreateMasterModel();

                //  Run Client
                Console.WriteLine("~~~~ Start Client ~~~~");

                Console.WriteLine("\n~~~~  ~~~~\n");

                Http.GetAllCollection(client, response);

                Console.WriteLine("\n~~~~  ~~~~\n");
                
                Http.PostNewAlbumToCollection(client, newAlbum, newGenre, newTrack);

                Console.WriteLine("\n~~~~  ~~~~\n");
                
                Http.GetAllCollection(client, response);

                Console.WriteLine("\n~~~~  ~~~~\n");

                //string classType = "ALBUM", albumId = "Computer World", parameter = "ALBUM", update = "Computer Welt";          //  This is to update
                //Http.PutUpdateForParameter(client, response, classType, albumId, parameter, update);

                Console.WriteLine("\n~~~~  ~~~~\n");

                Http.GetAllCollection(client, response);

                Console.WriteLine("\n~~~~  ~~~~\n");
     
                //albumId = "Daveys Hits";
                //Http.DeleteAlbumFromCollection(client, response, albumId);

                Console.WriteLine("\n~~~~  ~~~~\n");

                Http.GetAllCollection(client, response);

                Console.WriteLine("\n~~~~  ~~~~\n");
                
                string albumId = "Trouser Jazz";
                Http.GetArtistFromAlbumTitle(client, response, albumId);

                Console.WriteLine("\n~~~~  ~~~~\n");
                //newMaster = Create.CreateMaster();
                //Http.PostNewAlbumToCollection(client, response, newMaster);
                //Http.GetAllCollection(client, response);
                

                Console.WriteLine("\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
