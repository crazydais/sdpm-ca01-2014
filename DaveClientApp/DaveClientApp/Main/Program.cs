using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

using DaveClientApp.Entity;
using DaveClientApp.HttpMethods;


namespace DaveClientApp.Main
{
    class Program
    {
        static void Main()
        {
            //HttpTestFeed test = new HttpTestFeed();
            //test.RunFeed();

            ClientInterface ci = new ClientInterface();
            ci.Start();
        }
    }
}
