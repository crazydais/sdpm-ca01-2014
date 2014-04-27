using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using DaveClientApp.Entity;

namespace DaveClientApp.HttpMethods
{
    public class WebServiceConnection
    {
        //  POST Methods
        public void PostNewAlbumToCollection(HttpClient client, HttpResponseMessage response, AlbumEntity album, GenreEntity genre, TrackEntity track)
        {
            HttpResponseMessage response1 = client.PostAsJsonAsync("api/WebService?album=true", album).Result;
            HttpResponseMessage response2 = client.PostAsJsonAsync("api/WebService?genre=true", genre).Result;
            HttpResponseMessage response3 = client.PostAsJsonAsync("api/WebService?track=true", track).Result;
            if (response1.IsSuccessStatusCode)                                               // 200 .. 299
            {
                //  This line is not working for some reason... //Uri newMasterUri = response.Headers.Location.AbsoluteUri;          //response.Headers.Location;
                Console.WriteLine("POST method was SUCCESSFUL: '" + album.Album + "' was added"); //+ response.Headers.Location.AbsoluteUri);
                response = response1;
            }
            else
            {
                Console.WriteLine("POST method was NOT successful, " + response1.StatusCode + " " + response1.ReasonPhrase);
                response = response1;
            }
        }

        //  GET Methods
        public List<AlbumEntity> GetAllCollection(HttpClient client, HttpResponseMessage response)
        {
            response = client.GetAsync("api/WebService?showCollection=true").Result;
            List<AlbumEntity> albums = new List<AlbumEntity>();
            if (response.IsSuccessStatusCode)
            {
                albums = response.Content.ReadAsAsync<IEnumerable<AlbumEntity>>().Result.ToList();
                foreach (var al in albums)
                {
                    Console.WriteLine(al.Album + " - " + al.Artist);
                }
            }
            else
            {
                Console.WriteLine("GET method was NOT successful: " + response.StatusCode + " : " + response.ReasonPhrase);
            }

            return albums;
        }

        public void GetArtistFromAlbumTitle(HttpClient client, HttpResponseMessage response, string albumName)
        {
            response = client.GetAsync("api/Webservice?getArtistFromAlbumName=" + albumName).Result;
            if (response.IsSuccessStatusCode)
            {
                var artist = response.Content.ReadAsAsync<String>().Result;
                Console.WriteLine(artist);
            }
        }
        public void GetAlbumsFromArtist(HttpClient client, HttpResponseMessage response, string artistName)
        {
            response = client.GetAsync("api/Webservice?getAlbumsFromArtistName=" + artistName).Result;
            if (response.IsSuccessStatusCode)
            {
                var albums = response.Content.ReadAsAsync<IEnumerable<string>>().Result;
                foreach(string s in albums)
                { 
                    Console.WriteLine(s);
                }  
            }
        }

        public void GetTrackListFromAlbum(HttpClient client, HttpResponseMessage response, string albumName)
        {
            response = client.GetAsync("api/Webservice?getTracksFromAlbumName=" + albumName).Result;
            if (response.IsSuccessStatusCode)
            {
                var tracks = response.Content.ReadAsAsync<IEnumerable<string>>().Result;
                foreach (string s in tracks)
                {
                    Console.WriteLine(s);
                } 
            }
        }

        public void GetAlbumsWithGenres(HttpClient client, HttpResponseMessage response, string genre1, string genre2, string genre3)
        {
            response = client.GetAsync("api/WebService?forGenre01=" + genre1 + "&forGenre02=" + genre2 + "&forGenre03=" + genre3).Result;
            if (response.IsSuccessStatusCode)
            {
                var albums = response.Content.ReadAsAsync<IEnumerable<string>>().Result;
                foreach (string s in albums)
                {
                    Console.WriteLine(s);
                } 
            }
        }

        public void GetAlbumsInOrderOfValue(HttpClient client, HttpResponseMessage response)
        {
            response = client.GetAsync("api/WebService?showHighestValueAlbumsFirst=true").Result;
            if (response.IsSuccessStatusCode)
            {
                var albums = response.Content.ReadAsAsync<IEnumerable<AlbumEntity>>().Result;
                foreach (AlbumEntity a in albums)
                {
                    Console.WriteLine(a.ToString());
                }
            }
        }

        public void GetAlbumsInOrderOfRating(HttpClient client, HttpResponseMessage response)
        {
            response = client.GetAsync("api/WebService?showTopRatedAlbums=true").Result;
            if (response.IsSuccessStatusCode)
            {
                var albums = response.Content.ReadAsAsync<IEnumerable<AlbumEntity>>().Result;
                foreach (AlbumEntity a in albums)
                {
                    Console.WriteLine(a.ToString());
                }
            }
        }

        //GetCollectionReport(bool showRecordCollectionStatistics
        public void GetRecordCollectionReport(HttpClient client, HttpResponseMessage response)
        {
            response = client.GetAsync("api/WebService?showRecordCollectionStatistics=true").Result;
            if (response.IsSuccessStatusCode)
            {
                var report = response.Content.ReadAsAsync<IEnumerable<string>>().Result;
                foreach (string s in report)
                {
                    Console.WriteLine(s);
                }
            }
            else
            {
                var report = response.Content.ReadAsAsync<IEnumerable<string>>().Result;
                foreach (string s in report)
                {
                    Console.WriteLine(s);
                }
            }
        }


        //  PUT Methods     -       Http.PutUpdateForParameter(client, response, albumToUpdate, byArtist, parameter, newValue);

        public void PutUpdateForParameter(HttpClient client, HttpResponseMessage response, string entityType, string albumToUpdate, string byArtist, string parameter, string newValue)
        {
            //PUT  api/WebService?albumToUpdate={albumToUpdate}&byArtist={byArtist}&parameterToUpdate={parameterToUpdate}&newValue={newValue}
            response = client.PutAsJsonAsync("api/WebService?entityType=" + entityType + "&albumToUpdate=" + albumToUpdate + "&byArtist=" + byArtist + "&parameterToUpdate=" + parameter + "&newValue=" + newValue, "").Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("PUT method was SUCCESSFUL: '" + albumToUpdate + "' was updated"); //+ response.Headers.Location.AbsoluteUri);
            }
            else
            {
                Console.WriteLine("PUT method was NOT successful" + response.StatusCode + " - " + response.ReasonPhrase + " - " + response.Content.ToString());
            }
        }

        //  DELETE Methods  -   DELETE api/WebService?albumToDelete={albumToDelete}&byArtist={byArtist}
       
        public void DeleteAlbumFromCollection(HttpClient client, HttpResponseMessage response, string albumId, string artist)
        {
            //  DELETE api/RecordCollection?album={album}  
            response = client.DeleteAsync("api/WebService?albumToDelete=" + albumId + "&byArtist=" + artist).Result;

            if (response.IsSuccessStatusCode)                                               // 200 .. 299
            {
                //  This line is not working for some reason... //Uri newMasterUri = response.Headers.Location.AbsoluteUri;          //response.Headers.Location;
                Console.WriteLine("DELETE method was SUCCESSFUL: '" + albumId + "' was deleted"); //+ response.Headers.Location.AbsoluteUri);
            }
            else
            {
                Console.WriteLine("DELETE method was NOT successful" + response.StatusCode + " - " + response.ReasonPhrase);
            }
        }


    }
}
