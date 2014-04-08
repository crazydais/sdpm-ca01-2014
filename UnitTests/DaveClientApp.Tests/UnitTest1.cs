using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DaveClientApp.DataClasses;
using DaveClientApp.HttpMethods;

namespace DaveClientApp.Tests
{
    [TestClass]
    public class UnitTest1
    {
        public 

        [TestMethod]
        public void Test_GetArtistFromAlbumTitle()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:2454/"); 
            HttpResponseMessage response = new HttpResponseMessage();
            DaveClientHttpMethods hmet = new DaveClientHttpMethods();

            // Act
            List<AlbumModel> result = hmet.GetAllCollection(client, response);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotSame(0, result.Count);
 
        }
    }
}
