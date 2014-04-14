using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DaveWebService.Models;

namespace DaveWebService.Controllers
{
    public class RecordCollectionController : ApiController
    {
        static List <AlbumModel>albums = new List <AlbumModel>() 
        { 
            new AlbumModel { Album = "EP-01", Artist = "Dave Nolan", Label = "Unsigned", AlbumValue = 10.50, DiscNumber = 1 },
            new AlbumModel { Album = "AI:TM", Artist = "Dave Nolan", Label = "Unsigned", AlbumValue = 0.00, DiscNumber = 1 },
            new AlbumModel { Album = "Computer World", Artist = "Kraftwerk", Label = "EMI", AlbumValue = 12.00, DiscNumber = 1 }, 
            new AlbumModel { Album = "Trouser Jazz", Artist = "Mr. Scruff", Label = "Ninja Tune", AlbumValue = 11.25, DiscNumber = 1 },
        };

        static List<TrackModel>tracks = new List<TrackModel>()
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

        // POST Methods - Create/Add Data
        public MasterModel PostAddAlbum(MasterModel master)
        {
            MasterModel mm = master;
            if(master.MasterAlbum.Album != "" && master.MasterGenre.Genre_01 != "" && master.MasterTrack.Track_01_Title != "")
            {
                if (!(albums.Exists(a => a.Album.ToUpper().Equals(master.MasterAlbum.Album.ToUpper()))))
                {
                    albums.Add(master.MasterAlbum);
                    mm.MasterAlbum = albums.Last();
                }
                if(!(genres.Exists(g => g.Album.ToUpper().Equals(master.MasterGenre.Album.ToUpper()))))
                {
                    genres.Add(master.MasterGenre);
                    mm.MasterGenre = genres.Last();
                }
                if(!(tracks.Exists(t => t.Album.ToUpper().Equals(master.MasterTrack.Album.ToUpper()))))
                {
                    tracks.Add(master.MasterTrack);
                    mm.MasterTrack = tracks.Last();
                }
            }
            else
            {
                mm.MasterAlbum.Album = "Failed";
                mm.MasterGenre.Genre_01 = "Failed";
                mm.MasterTrack.Track_01_Title = "Failed";
            }
            return mm;
        }

        // PUT Methods - Update Data
        public void PutDetailUpdate(string classType, string albumId, string parameter, string update)       //  Pass in a new master model, with the type of class we're interested in, with the specific album we can to change, with the particular parameter we want to change, with the new value
        {
            if (ModelState.IsValid)
            {
                if (classType != "" && classType.ToUpper().Equals("ALBUM"))
                {
                    bool foundMatch = false;
                    AlbumModel albumToUpdate = new AlbumModel();
                    List<String> albumList = new List<String>();    //  This will return a list of Albums associated with the Artist Name that was provided.

                    foreach (AlbumModel al in albums)
                    {
                        if (al.Album.ToUpper().Equals(albumId.ToUpper()) && al.Album != null)
                        {
                            foundMatch = true;
                            albumToUpdate = al;
                            if (parameter.ToUpper().Equals("ALBUM")) { albumToUpdate.Album = update; }
                            if (parameter.ToUpper().Equals("ARTIST")) { albumToUpdate.Artist = update; }
                            if (parameter.ToUpper().Equals("DISCNUMBER")) { albumToUpdate.DiscNumber = Convert.ToInt32(update); }
                            if (parameter.ToUpper().Equals("LABEL")) { albumToUpdate.Label = update; }
                            if (parameter.ToUpper().Equals("VALUE")) { albumToUpdate.AlbumValue = Convert.ToDouble(update); }
                        }
                    }
                    if (!foundMatch)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }
                }

                if (classType != null && classType.ToUpper().Equals("GENRE"))
                {
                    bool foundMatch = false;
                    GenreModel genreToUpdate = new GenreModel();
                    List<String> albumList = new List<String>();    //  This will return a list of Albums associated with the Artist Name that was provided.

                    foreach (GenreModel ge in genres)
                    {
                        if (ge.Album.ToUpper().Equals(albumId.ToUpper()) && ge.Album != null)
                        {
                            foundMatch = true;
                            genreToUpdate = ge;
                            if (parameter.ToUpper().Equals("ALBUM")) { genreToUpdate.Album = update; }
                            if (parameter.ToUpper().Equals("ARTIST")) { genreToUpdate.Artist = update; }
                            if (parameter.ToUpper().Equals("DISCNUMBER")) { genreToUpdate.DiscNumber = Convert.ToInt32(update); }
                            if (parameter.ToUpper().Equals("GENRE01")) { genreToUpdate.Genre_01 = update; }
                            if (parameter.ToUpper().Equals("GENRE02")) { genreToUpdate.Genre_02 = update; }
                            if (parameter.ToUpper().Equals("GENRE03")) { genreToUpdate.Genre_03 = update; }
                        }
                    }
                    if (!foundMatch)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }
                }

                if(classType != null && classType.ToUpper().Equals("TRACK"))
                {
                    bool foundMatch = false;
                    TrackModel trackToUpdate = new TrackModel();    //  Track parameter to update, this will be based on the parameter that is passed in.
                    List<String> albumList = new List<String>();    //  This will return a list of Albums associated with the Artist Name that was provided.

                    foreach (TrackModel tr in tracks)
                    {
                        if (tr.Album.ToUpper().Equals(albumId.ToUpper()) && tr.Album != null)
                        {
                            foundMatch = true;
                            trackToUpdate = tr;
                            if (parameter.ToUpper().Equals("ALBUM")) { trackToUpdate.Album = update; }
                            if (parameter.ToUpper().Equals("ARTIST")) { trackToUpdate.Artist = update; }
                            if (parameter.ToUpper().Equals("DISCNUMBER")) { trackToUpdate.DiscNumber = Convert.ToInt32(update); }
                            if (parameter.ToUpper().Equals("NUMOFTRACKS")) { trackToUpdate.DiscNumber = Convert.ToInt32(update); }
                            if (parameter.ToUpper().Equals("TRACK01")) { trackToUpdate.Track_01_Title = update; }
                            if (parameter.ToUpper().Equals("TRACK02")) { trackToUpdate.Track_02_Title = update; }
                            if (parameter.ToUpper().Equals("TRACK03")) { trackToUpdate.Track_03_Title = update; }
                            if (parameter.ToUpper().Equals("TRACK04")) { trackToUpdate.Track_02_Title = update; }
                            if (parameter.ToUpper().Equals("TRACK05")) { trackToUpdate.Track_03_Title = update; }
                        }
                    }
                    if (!foundMatch)
                    {
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    }
                }
            }
        }

        // DELETE Methods - Delete Data
        public void DeleteAlbum(String album)
        {
            bool found = albums.Exists(al => al.Album.ToUpper().Equals(album.ToUpper()));
            if (found)
            {
                albums.RemoveAll(al => al.Album.ToUpper().Equals(album.ToUpper()));
                genres.RemoveAll(ge => ge.Album.ToUpper().Equals(album.ToUpper()));
                tracks.RemoveAll(tr => tr.Album.ToUpper().Equals(album.ToUpper()));
                // default is to return 200 OK
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // GET Methods - Read Data
        public IEnumerable<AlbumModel> GetAllAlbums()
        {
            return albums;                                                   // 200 OK, listings serialized in response body
        }
        public string GetArtistFromAlbum(string albumName, bool getArtist)
        {
            bool foundMatch = false;
            string artistList = "";     //  This will return the artist name belonging to the album name that was provided

            foreach(AlbumModel al in albums)
            {
                if(al.Album.ToUpper().Equals(albumName.ToUpper()) && al.Album != null)
                {
                    foundMatch = true;
                    artistList = al.Artist;
                }      
            }
            if (!foundMatch)
            {
                artistList = "No Album Found, with name: " + albumName;
            }

            return artistList;                                               
        }
        public IEnumerable<String> GetAlbumsFromArtist(string artistName, bool getAlbum)
        {
            bool foundMatch = false;
            List<String> albumList = new List<String>();    //  This will return a list of Albums associated with the Artist Name that was provided.

            foreach (AlbumModel al in albums)
            {
                if (al.Artist.ToUpper().Equals(artistName.ToUpper()) && al.Artist != null)
                {
                    foundMatch = true;
                    albumList.Add(al.Album);
                }
            }
            if (!foundMatch)
            {
                albumList.Add("No Artist Found, with name: " + artistName);
            }

            return albumList;
        }
        public IEnumerable<String> GetTrackNamesFromAlbum(string albumName, int discNumber = 0, bool getTracks = true)
        {
            bool foundMatch = false;
            List<String> trackList = new List<String>();

            foreach (TrackModel tr in tracks)
            {
                if (tr.Album.ToUpper().Equals(albumName.ToUpper()) && tr.Album != null)
                {
                    foundMatch = true;
                    if (discNumber == 0)
                    {
                        if (tr.Track_01_Title != null && tr.DiscNumber == 1) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 01: " + tr.Track_01_Title); }
                        if (tr.Track_02_Title != null && tr.DiscNumber == 1) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 02: " + tr.Track_02_Title); }
                        if (tr.Track_03_Title != null && tr.DiscNumber == 1) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 03: " + tr.Track_03_Title); }
                        if (tr.Track_04_Title != null && tr.DiscNumber == 1) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 04: " + tr.Track_04_Title); }
                        if (tr.Track_05_Title != null && tr.DiscNumber == 1) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 05: " + tr.Track_05_Title); }
                        if (tr.Track_01_Title != null && tr.DiscNumber == 2) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 01: " + tr.Track_01_Title); }
                        if (tr.Track_02_Title != null && tr.DiscNumber == 2) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 02: " + tr.Track_02_Title); }
                        if (tr.Track_03_Title != null && tr.DiscNumber == 2) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 03: " + tr.Track_03_Title); }
                        if (tr.Track_04_Title != null && tr.DiscNumber == 2) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 04: " + tr.Track_04_Title); }
                        if (tr.Track_05_Title != null && tr.DiscNumber == 2) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 05: " + tr.Track_05_Title); }
                    }
                    else if (discNumber == 1)
                    {
                        if (tr.Track_01_Title != null && tr.DiscNumber == 1) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 01: " + tr.Track_01_Title); }
                        if (tr.Track_02_Title != null && tr.DiscNumber == 1) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 02: " + tr.Track_02_Title); }
                        if (tr.Track_03_Title != null && tr.DiscNumber == 1) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 03: " + tr.Track_03_Title); }
                        if (tr.Track_04_Title != null && tr.DiscNumber == 1) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 04: " + tr.Track_04_Title); }
                        if (tr.Track_05_Title != null && tr.DiscNumber == 1) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 05: " + tr.Track_05_Title); }
                    }
                    else if (discNumber == 2)
                    {

                        if (tr.Track_01_Title != null && tr.DiscNumber == 2) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 01: " + tr.Track_01_Title); }
                        if (tr.Track_02_Title != null && tr.DiscNumber == 2) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 02: " + tr.Track_02_Title); }
                        if (tr.Track_03_Title != null && tr.DiscNumber == 2) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 03: " + tr.Track_03_Title); }
                        if (tr.Track_04_Title != null && tr.DiscNumber == 2) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 04: " + tr.Track_04_Title); }
                        if (tr.Track_05_Title != null && tr.DiscNumber == 2) { trackList.Add("Disc No." + tr.DiscNumber + ". Track 05: " + tr.Track_05_Title); }
                    }
                    else if(discNumber != tr.DiscNumber)
                    {
                        trackList.Add("No Disc Found, with number: " + discNumber);
                        break;
                    }
                    
                }
            }
            if (!foundMatch)
            {
                trackList.Add("No Album Found, with name: " + albumName);
            }

            return trackList;
        }
        public IEnumerable<String> GetAlbumsFromGenre(string genre01, string genre02 = "", string genre03 = "")
        {
            bool foundMatch = false;
            List<String> albumList = new List<String>();

            foreach (GenreModel ge in genres)
            {
                if (ge.Genre_02 != null && ge.Genre_03 != null)     //  If a GenreModel object contains a Genre_02 and Genre_03 property, then carry out this search.  If this check was not there, the program would crash as it would be check a string on a NULL property.
                {
                    if (
                        ge.Genre_01.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre03.ToUpper()) ||
                        ge.Genre_02.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_02.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_02.ToUpper().Equals(genre03.ToUpper()) ||
                        ge.Genre_03.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_03.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_03.ToUpper().Equals(genre03.ToUpper())
                       )
                    {
                        foundMatch = true;
                        albumList.Add(ge.Album);
                    }
                }
                else if (ge.Genre_02 != null && ge.Genre_03 == null)
                {
                    if (
                        ge.Genre_01.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre03.ToUpper()) ||
                        ge.Genre_02.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_02.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_02.ToUpper().Equals(genre03.ToUpper())
                       )
                    {
                        foundMatch = true;
                        albumList.Add(ge.Album);
                    }

                }
                else if (ge.Genre_02 == null && ge.Genre_03 != null)
                {
                    if (
                        ge.Genre_01.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre03.ToUpper()) ||
                        ge.Genre_03.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_03.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_03.ToUpper().Equals(genre03.ToUpper())
                       )
                    {
                        foundMatch = true;
                        albumList.Add(ge.Album);
                    }

                }
                else
                {
                    if ( ge.Genre_01.ToUpper().Equals(genre01.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre02.ToUpper()) || ge.Genre_01.ToUpper().Equals(genre03.ToUpper()) )
                    {
                        foundMatch = true;
                        albumList.Add(ge.Album);
                    }

                }
            }
            if (!foundMatch)
            {
                if(genre02.Equals("") && genre03.Equals(""))
                {
                    albumList.Add("No Genre(s) Found: " + genre01);
                }
                else if (!genre02.Equals("") && genre03.Equals(""))
                {
                    albumList.Add("No Genre(s) Found: " + genre01 + ", " + genre02);
                }
                else if(genre02.Equals("") && !genre03.Equals(""))
                {
                    albumList.Add("No Genre(s) Found: " + genre01 + ", " + genre03);
                }
                else
                {
                    albumList.Add("No Genre(s) Found: " + genre01 + ", " + genre02 + ", " + genre03);
                }
            }

            return albumList;
        }
    }
}
