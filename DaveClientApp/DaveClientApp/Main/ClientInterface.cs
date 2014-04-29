using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using DaveClientApp.Entity;
using DaveClientApp.HttpMethods;

namespace DaveClientApp.Main
{
    public class ClientInterface
    {

        HttpClient client = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
                
        public void Start()
        {
            //client.BaseAddress = new Uri("http://localhost:2454/");
            client.BaseAddress = new Uri("http://davewebservice.cloudapp.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            bool run = true;

            do
            {
                run = MainMenu(run);


            } while (run == true);
        }

        public bool Exit()
        {
            Console.WriteLine("\n\nThanks, and goodbye!!!\n\n");
            return false;
        }

        public void Clear(string message, int time)
        {
            Console.Clear();
            Console.WriteLine(message);
            System.Threading.Thread.Sleep(time);
            Console.Clear();
        }    //  C

        public bool MainMenu(bool run)  
        {
            this.Clear("", 500);

            Console.WriteLine("\t\t~ :: Welcome to SoulFul Sounds :: ~ ");
            Console.WriteLine("\t\t\t\t~ ~\t");
            Console.WriteLine("\t  The WebService for Archiving Your Vinyl Records");
            Console.WriteLine("\t\t\t\t~ ~\t");
            Console.WriteLine("\n\n");
            Console.WriteLine("\tMain Menu - Please choose a number...\n");

            Console.WriteLine("\t0. Quit App\n");
            Console.WriteLine("\t1. Add An Album...");
            Console.WriteLine("\t2. Update An Album...");
            Console.WriteLine("\t3. Delete An Album...");
            Console.WriteLine("\t4. Get Album Information...");

            Console.WriteLine("\n");
            Console.Write("Enter Option :> ");
            string userInput = Console.ReadLine();
            if (userInput.ToUpper().Equals(""))
            {
                //  Do nothing until a valid value is entered...
            }
            else
            {
                int num = -1;
                if (!int.TryParse(userInput, out num))
                {
                    string message = String.Format("The value you entered '{0}' is not a valid option!\nPlease pick a value from the list", userInput);
                    this.Clear(message, 2000);
                }
                else
                {
                    if (num == 0) { run = this.Exit(); }
                    if (num == 1) { this.AddAlbum(); }
                    if (num == 2) { this.UpdateEntity(); }
                    if (num == 3) { this.DeleteAlbum(); }
                    if (num == 4) { this.GetAlbumsInfo(); }
                }
            }

            return run;
        }               //  00

        public void AddAlbum()          
        {
            AlbumEntity newAl = new AlbumEntity();
            GenreEntity newGe = new GenreEntity();
            TrackEntity newTr = new TrackEntity();

             bool returnToMainMenu = false;
            do
            {
                this.Clear("", 0);
                Console.WriteLine("\t ~ Add Album Menu ~\n\n");

                Console.WriteLine("\tC. Clear Screen");
                Console.WriteLine("\t0. Return To Main Menu\n");
                Console.WriteLine("\t1. Add a new Album...");

                Console.WriteLine("\n");
                Console.Write("Enter Option :> ");
                string userInput = Console.ReadLine();
                if (userInput.ToUpper().Equals("C"))
                {
                    this.Clear("", 0);
                }
                else
                {
                    int num = -1;
                    if (!int.TryParse(userInput, out num))
                    {
                        string message = String.Format("The value you entered '{0}' is not a valid option!\nPlease pick a value from the list", userInput);
                        this.Clear(message, 2000);
                    }
                    else if (num == 0)
                    {
                        returnToMainMenu = true;
                    }
                    else
                    {
                        if (num == 0) { returnToMainMenu = true; }

                        if (num == 1)
                        {
                            Console.WriteLine("In Order to Create an Album, you must provide the following information...\n\n");
                            Console.Write("\nAlbum Title(Required): "); newAl.Album = Console.ReadLine();
                            Console.Write("\nAlbum Artist(Required): "); newAl.Artist = Console.ReadLine();
                            Console.Write("\nNumber of Tracks(Required): "); newTr.NumberOfTracks = Convert.ToInt32(Console.ReadLine());
                            Console.Write("\nRecord Label(Required): "); newAl.Label = Console.ReadLine();
                            Console.Write("\nAlbum rating(0-5): "); try { newAl.Rating = Convert.ToDouble(Console.ReadLine()); }
                            catch (Exception) { newAl.Rating = 0.0; }
                            Console.Write("\nValue of the Album in Euros(Required): "); try { newAl.AlbumValue = Convert.ToDouble(Console.ReadLine()); }
                            catch (Exception) { newAl.AlbumValue = 0.0; }

                            Console.Clear();

                            Console.WriteLine("Ok, now enter up to 3 genres that best fit the album...\n\n");
                            newGe.Album = newAl.Album; newGe.Artist = newAl.Artist;
                            Console.Write("\nGenre 01(Required): "); newGe.Genre_01 = Console.ReadLine();
                            Console.Write("\nGenre 02(Optional): "); newGe.Genre_02 = Console.ReadLine();
                            Console.Write("\nGenre 03(Optional): "); newGe.Genre_03 = Console.ReadLine();

                            Console.Clear();

                            Console.WriteLine("Great! Now enter the track titles...\n\n");
                            newTr.Album = newAl.Album; newTr.Artist = newAl.Artist;

                            Console.Write("\nTrack 1 - Title (Required): ");
                            newTr.Track_01_Title = Console.ReadLine();
                            Console.Write("\nTrack 2 - Title (Required): ");
                            newTr.Track_02_Title = Console.ReadLine();
                            Console.Write("\nTrack 3 - Title (Required): ");
                            newTr.Track_03_Title = Console.ReadLine();
                            Console.Write("\nTrack 4 - Title (Required): ");
                            newTr.Track_04_Title = Console.ReadLine();
                            Console.Write("\nTrack 5 - Title (Required): ");
                            newTr.Track_05_Title = Console.ReadLine();

                            this.Clear("", 0);

                            WebServiceConnection wsc = new WebServiceConnection();
                            wsc.PostNewAlbumToCollection(client, response, newAl, newGe, newTr);
                            //Console.WriteLine("Your Album was created successfully");

                            Console.Write("Would you like to add another album? (yes/no): ");
                            string answer = Console.ReadLine();
                            if (answer.ToUpper().Equals("Y") || answer.ToUpper().Equals("YES"))
                            {
                                //  Keep going with do while loop
                            }
                            else
                            {
                                returnToMainMenu = true;
                            }
                        }

                    }
                }
            } while (returnToMainMenu == false);
        }               //  01

        public void UpdateEntity()      
        {
            string entityType = ""; string albumToUpdate = ""; string byArtist = ""; string parameter = ""; string newValue = "";
            
            bool returnToMainMenu = false;
            do
            {
            
                    this.Clear("", 0);
                    Console.WriteLine("\t ~ Update Album Menu ~\n\n");

                    Console.WriteLine("\tC. Clear Screen");
                    Console.WriteLine("\t0. Return To Main Menu\n");
                    Console.WriteLine("\t1. Update General Album Details\n");
                    Console.WriteLine("\t2. Update Album Genre Details\n");
                    Console.WriteLine("\t3. Update Album Track Details\n");

                    Console.WriteLine("\n");
                    Console.Write("Enter Option :> ");
                    string userInput = Console.ReadLine();
                    if (userInput.ToUpper().Equals("C"))
                    {
                        this.Clear("", 0);
                    }
                    else
                    {
                        int num = -1;
                        if (!int.TryParse(userInput, out num))
                        {
                            string message = String.Format("The value you entered '{0}' is not a valid option!\nPlease pick a value from the list", userInput);
                            this.Clear(message, 2000);
                        }
                        else if (num == 0) 
                        { 
                            returnToMainMenu = true; 
                        }
                        else
                        {
                            if (num == 1)
                            {
                                entityType = "ALBUMENTITY";
                            }
                            if (num == 2)
                            {
                                entityType = "GENRENTITY";
                            }
                            if (num == 3)
                            {
                                entityType = "TRACKNTITY";
                            }

                            Console.Write("\n");
                            Console.Write("What is the Name of the Album you want to Update? : "); albumToUpdate = Console.ReadLine();
                            Console.Write("By What Artist? : "); byArtist = Console.ReadLine();
                            Console.Write("What Parameter Do You Want To Update? : "); parameter = Console.ReadLine();
                            Console.Write("Lastly, What is the new value? : "); newValue = Console.ReadLine();

                            this.Clear("", 0);

                            WebServiceConnection wsc = new WebServiceConnection();
                            wsc.PutUpdateForParameter(client, response, entityType, albumToUpdate, byArtist, parameter, newValue);
                            Console.WriteLine("Your Album was created successfully");

                            Console.Write("Would you like to update another album? (yes/no): ");
                            string answer = Console.ReadLine();
                            if (answer.ToUpper().Equals("Y") || answer.ToUpper().Equals("YES"))
                            {
                                //  Keep going with do while loop
                            }
                            else
                            {
                                returnToMainMenu = true;
                            }
                        }
                    }
            }while(returnToMainMenu == false);
        }               //  02

        public void DeleteAlbum()       
        {
            string albumId = ""; string artist = "";
            
            bool returnToMainMenu = false;
            do
            {
                this.Clear("", 0);
                Console.WriteLine("\t ~ Delete Album Menu ~\n\n");

                Console.WriteLine("\tC. Clear Screen");
                Console.WriteLine("\t0. Return To Main Menu\n");
                Console.WriteLine("\t1. Delete Album\n");

                Console.WriteLine("\n");
                Console.Write("Enter Option :> ");
                string userInput = Console.ReadLine();
                if (userInput.ToUpper().Equals("C"))
                {
                    this.Clear("", 0);
                }
                else
                {
                    int num = -1;
                    if (!int.TryParse(userInput, out num))
                    {
                        string message = String.Format("The value you entered '{0}' is not a valid option!\nPlease pick a value from the list", userInput);
                        this.Clear(message, 2000);
                    }
                    else if (num == 0)
                    {
                        returnToMainMenu = true;
                    }
                    else
                    {
                        if (num == 1)
                        {
                            Console.Write("\n");
                            Console.Write("What is the Name of the Album you want to Delete? : "); albumId = Console.ReadLine();
                            Console.Write("What is the Name of the Arist? : "); artist = Console.ReadLine();

                            this.Clear("", 00);

                            WebServiceConnection wsc = new WebServiceConnection();
                            wsc.DeleteAlbumFromCollection(client, response, albumId, artist);

                            Console.Write("Would you like to delete another album? (yes/no): ");
                            string answer = Console.ReadLine();
                            if (answer.ToUpper().Equals("Y") || answer.ToUpper().Equals("YES"))
                            {
                                //  Keep going with do while loop
                            }
                            else
                            {
                                returnToMainMenu = true;
                            }
                        }
                    }
                }
            } while (returnToMainMenu == false);
        }               //  03

        public void GetAlbumsInfo()     
        {
            WebServiceConnection wsc = new WebServiceConnection();
            
            bool returnToMainMenu = false;
            do
            {
                this.Clear("", 0);
                Console.WriteLine("Album Info Menu\n");

                Console.WriteLine("\tC. Clear Screen");
                Console.WriteLine("\t0. Return To Main Menu\n");
                Console.WriteLine("\t1. List all the albums in the collection...");
                Console.WriteLine("\t2. Find the Artists name for an album...");
                Console.WriteLine("\t3. List all the Albums by an Artist...\n");
                Console.WriteLine("\t4. List all the Tracks for an Album...");
                Console.WriteLine("\t5. List all the Albums that match a Genre...\n");
                Console.WriteLine("\t6. List Albums by Price...");
                Console.WriteLine("\t7. List Albums by Rating......\n");
                Console.WriteLine("\t8. Show a summary of your Record Collection...");

                Console.WriteLine("\n");
                Console.Write("Enter Option :> ");
                string userInput = Console.ReadLine();
                if (userInput.ToUpper().Equals("C"))
                {
                    this.Clear("", 0);
                }
                else
                    {
                        int num = -1;
                        if (!int.TryParse(userInput, out num))
                        {
                            string message = String.Format("The value you entered '{0}' is not a valid option!\nPlease pick a value from the list", userInput);
                            this.Clear(message, 2000);
                        }
                        else if (num == 0)
                        {
                            returnToMainMenu = true;
                        }
                        else
                        {
                            if (num == 0) { returnToMainMenu = true; }

                            if (num == 1)   //  Get all albums
                            {
                                this.Clear("", 0);
                                wsc.GetAllCollection(client, response);
                                Console.WriteLine("\n\nPress Any Key To Continue...");
                                Console.ReadKey();
                            }
                            if (num == 2)   //  Find the Artists name for an album
                            {
                                this.Clear("", 0);
                                Console.Write("Please enter the Album name: ");
                                string album = Console.ReadLine();
                                this.Clear("", 0);
                                wsc.GetArtistFromAlbumTitle(client, response, album);
                                Console.WriteLine("\n\nPress Any Key To Continue...");
                                Console.ReadKey();
                            }
                            if (num == 3)   //  Find all albums by Artist
                            {
                                this.Clear("", 0);
                                Console.Write("Please enter the Artists name: ");
                                string artist = Console.ReadLine();
                                this.Clear("", 0);
                                wsc.GetAlbumsFromArtist(client, response, artist);
                                Console.WriteLine("\n\nPress Any Key To Continue...");
                                Console.ReadKey();
                            }
                            if (num == 4)   //  List all the Tracks for an Album
                            {
                                this.Clear("", 0);
                                Console.Write("Please enter the Album name: ");
                                string album = Console.ReadLine();
                                this.Clear("", 0);
                                wsc.GetTrackListFromAlbum(client, response, album);
                                Console.WriteLine("\n\nPress Any Key To Continue...");
                                Console.ReadKey();
                            }
                            if (num == 5)   //  List all the Albums that match a Genre
                            {
                                this.Clear("", 0);
                                Console.Write("Please enter the 1st Genre (Required): ");
                                string genre1 = Console.ReadLine(); 
                                Console.Write("Please enter the 2nd Genre (Optional): ");
                                string genre2 = Console.ReadLine(); if (genre2.Equals("")) { genre2 = "null"; }
                                Console.Write("Please enter the 3rd Genre (Optional): ");
                                string genre3 = Console.ReadLine(); if (genre3.Equals("")) { genre3 = "null"; }
                                this.Clear("", 0);
                                wsc.GetAlbumsWithGenres(client, response, genre1, genre2, genre3);
                                Console.WriteLine("\n\nPress Any Key To Continue...");
                                Console.ReadKey();
                            }
                            if (num == 6)
                            {
                                this.Clear("", 0);
                                wsc.GetAlbumsInOrderOfValue(client, response);
                                Console.WriteLine("\n\nPress Any Key To Continue...");
                                Console.ReadKey();
                            }
                            if (num == 7)
                            {
                                this.Clear("", 0);
                                wsc.GetAlbumsInOrderOfRating(client, response);
                                Console.WriteLine("\n\nPress Any Key To Continue...");
                                Console.ReadKey();
                            }
                            if (num == 8)
                            {
                                this.Clear("", 0);
                                wsc.GetRecordCollectionReport(client, response);
                                Console.WriteLine("\n\nPress Any Key To Continue...");
                                Console.ReadKey();
                            }
                        }
                   
                }
            } while (returnToMainMenu == false);
        }               //  04
    
 
    }   // end class 
}       // end namespace  
