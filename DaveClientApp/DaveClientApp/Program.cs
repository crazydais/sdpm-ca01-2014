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

                AlbumEntity newAlbum = new AlbumEntity() { Album = "Davey's Hits", Artist = "Dave Nolan", Label = "Unsigned", AlbumValue = 12.50, PartitionKey = "Dave Nolan", Rating = 4.1, RowKey = "Davey's Hits" };
                GenreEntity newGenre = new GenreEntity() { Album = "Davey's Hits", Artist = "Dave Nolan", Genre_01 = "Techno", Genre_02 = "House", Genre_03 = "Electronic", PartitionKey = "Dave Nolan", RowKey = "Davey's Hits" };
                TrackEntity newTrack = new TrackEntity() { Album = "Davey's Hits", Artist = "Dave Nolan", NumberOfTracks = 5, Track_01_Title = "Track01", Track_02_Title = "Track02", Track_03_Title = "Track03", Track_04_Title = "Track04", Track_05_Title = "Track05", PartitionKey = "Dave Nolan", RowKey = "Davey's Hits" };

                //DaveClientHttpMethods Http = new DaveClientHttpMethods();
                WebServiceConnection Http = new WebServiceConnection();

                CreateMasterModel Create = new CreateMasterModel();

                //  Run Client
                        Console.WriteLine("~~~~ Start Client ~~~~");

                        Console.WriteLine("\n~~~~  ~~~~\n");

                Http.GetAllCollection(client, response);

                        Console.WriteLine("\n~~~~  ~~~~\n");
                
                //  POST new Album
                //Http.PostNewAlbumToCollection(client, newAlbum, newGenre, newTrack);

                        Console.WriteLine("\n~~~~  ~~~~\n");
                
                Http.GetAllCollection(client, response);

                        Console.WriteLine("\n~~~~  ~~~~\n");

                //  PUT 
                        string entityType = "ALBUMENTITY", albumToUpdate = "Davey's Hits", byArtist = "Dave Nolan", parameter = "ALBUM", newValue = "Drum Machine";          //  This is to update
                        Http.PutUpdateForParameter(client, response, entityType, albumToUpdate, byArtist, parameter, newValue);

                        Console.WriteLine("\n~~~~  ~~~~\n");

                Http.GetAllCollection(client, response);

                        Console.WriteLine("\n~~~~  ~~~~\n");
     
                //  DELETE Album with title
                string albumId = "Electro Classics", artist = "Dave Nolan";
                //Http.DeleteAlbumFromCollection(client, response, albumId, artist);


                        Console.WriteLine("\n~~~~  ~~~~\n");

                Http.GetAllCollection(client, response);

                        Console.WriteLine("\n~~~~  ~~~~\n");
                
                //  GET Artist from Album
                albumId = "Trouser Jazz";
                Http.GetArtistFromAlbumTitle(client, response, albumId);

               

                        Console.WriteLine("\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
