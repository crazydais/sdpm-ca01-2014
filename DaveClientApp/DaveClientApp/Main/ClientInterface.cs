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
            client.BaseAddress = new Uri("http://localhost:2454/");
            bool run = true;

            do
            {
                run = MainMenu(run);


            } while (run == true);
        }

        public bool MainMenu(bool run)
        {
            Console.Clear();
            
            Console.WriteLine("\t\t~ :: Welcome to SoulFul Sounds :: ~ ");
            Console.WriteLine("\t\t\t\t~ ~\t");
            Console.WriteLine("\t  The WebService for Archiving Your Vinyl Records");
            Console.WriteLine("\t\t\t\t~ ~\t");
            Console.WriteLine("\n\n");
            Console.WriteLine("\tPlease select an option...\n");

            Console.WriteLine("\t0. Exit\n");
            Console.WriteLine("\t1. Add An Album");
            Console.WriteLine("\t2. Update An Album");
            Console.WriteLine("\t3. Delete An Album");
            Console.WriteLine("\t4. Get Album Information");

            Console.WriteLine("\n");
            Console.Write("Enter Option :> ");
            string userInput = Console.ReadLine();
            if (userInput.ToUpper().Equals(""))
            {
                //  Do nothing until a valid value is entered...
            }
            else
            {
                int num = 0;
                if (!int.TryParse(userInput, out num))
                {
                    Console.WriteLine("The value you entered '{0}' is not a valid option!  Please pick a value from the list", userInput);
                }

                if (Convert.ToInt32(userInput) == 0) { run = this.Exit(); }
                if (Convert.ToInt32(userInput) == 1) { this.AddAlbum(); }
                if (Convert.ToInt32(userInput) == 2) { this.UpdateEntity(); }
                if (Convert.ToInt32(userInput) == 3) { this.DeleteAlbum(); }
                if (Convert.ToInt32(userInput) == 4) { this.GetAlbumsInfo(); }
            }

            return run;
        }

        public bool Exit()
        {
            Console.WriteLine("\n\nEXIT  - Thanks, and goodbye!!!\n\n");
            return false;
        }

        public void Clear()
        {
            Console.Clear();
            System.Threading.Thread.Sleep(500); 
        }

        public void AddAlbum()          //  01
        {
            AlbumEntity newAl = new AlbumEntity();
            GenreEntity newGe = new GenreEntity();
            TrackEntity newTr = new TrackEntity();
            
            bool returnToMainMenu = false;
            do
            {
                Console.Clear();
                Console.WriteLine("\t ~ Add Album Menu ~\n\n");

                Console.WriteLine("In Order to Create an Album, you must provide the following information...\n\n");
                Console.Write("\nAlbum Title(Required): "); newAl.Album = Console.ReadLine();
                Console.Write("\nAlbum Artist(Required): "); newAl.Artist = Console.ReadLine();
                Console.Write("\nNumber of Tracks(Required): "); newTr.NumberOfTracks = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nRecord Label(Required): "); newAl.Label = Console.ReadLine();
                Console.Write("\nAlbum rating(0-5)"); try { newAl.Rating = Convert.ToDouble(Console.ReadLine()); }
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

                this.Clear();

                WebServiceConnection wsc = new WebServiceConnection();
                wsc.PostNewAlbumToCollection(client, response, newAl, newGe, newTr);
                //Console.WriteLine("Your Album was created successfully");
                
                Console.Write("Would you like to add another album? (yes/no): ");
                string answer = Console.ReadLine();
                if(answer.ToUpper().Equals("Y") || answer.ToUpper().Equals("YES"))
                {
                    //  Keep going with do while loop
                }
                else
                {
                    returnToMainMenu = true;
                }

            } while (returnToMainMenu == false);
        }

        public void UpdateEntity()      //  02
        {
            string entityType = ""; string albumToUpdate = ""; string byArtist = ""; string parameter = ""; string newValue = "";
            
            bool returnToMainMenu = false;
            do
            {
            
                    Console.Clear();
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
                        this.Clear();
                    }
                    else
                    {
                        int num = 0;
                        if (!int.TryParse(userInput, out num))
                        {
                            Console.WriteLine("The value you entered '{0}' is not a valid option!  Please pick a value from the list", userInput);
                        }
                        if (Convert.ToInt32(userInput) == 0) { returnToMainMenu = true; }

                        if (Convert.ToInt32(userInput) == 1) 
                        {
                            entityType = "ALBUMENTITY";
                        }
                        if (Convert.ToInt32(userInput) == 2) 
                        {
                            entityType = "GENRENTITY";
                        }
                        if (Convert.ToInt32(userInput) == 3) 
                        {
                            entityType = "TRACKNTITY";
                        }

                        Console.Write("\n");
                        Console.Write("What is the Name of the Album you want to Update? : "); albumToUpdate = Console.ReadLine();
                        Console.Write("By What Artist? : "); byArtist = Console.ReadLine();
                        Console.Write("What Parameter Do You Want To Update? : "); parameter = Console.ReadLine();
                        Console.Write("Lastly, What is the new value? : "); newValue = Console.ReadLine();

                        this.Clear();

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
            }while(returnToMainMenu == false);
        }

        public void DeleteAlbum()       //  03
        {
            string albumId = ""; string artist = "";
            
            bool returnToMainMenu = false;
            do
            {
            
                    Console.Clear();
                    Console.WriteLine("\t ~ Delete Album Menu ~\n\n");

                    Console.WriteLine("\tC. Clear Screen");
                    Console.WriteLine("\t0. Return To Main Menu\n");
                    Console.WriteLine("\t1. Delete Album\n");

                    Console.WriteLine("\n");
                    Console.Write("Enter Option :> ");
                    string userInput = Console.ReadLine();
                    if (userInput.ToUpper().Equals("C"))
                    {
                        this.Clear();
                    }
                    else
                    {
                        int num = 0;
                        if (!int.TryParse(userInput, out num))
                        {
                            Console.WriteLine("The value you entered '{0}' is not a valid option!  Please pick a value from the list", userInput);
                        }
                        if (Convert.ToInt32(userInput) == 0) { returnToMainMenu = true; }

                        if (Convert.ToInt32(userInput) == 1) 
                        {
                            Console.Write("\n");
                            Console.Write("What is the Name of the Album you want to Delete? : "); albumId = Console.ReadLine();
                            Console.Write("What is the Name of the Arist? : "); artist = Console.ReadLine();
                        }
                    }

                    this.Clear();

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

            } while (returnToMainMenu == false);
 
        }

        public void GetAlbumsInfo()     //  04
        {
            Console.Clear();
            Console.WriteLine("Get Album Info Menu\n");
        }

    
    
    
    
    }   // end class 
}       // end namespace  
