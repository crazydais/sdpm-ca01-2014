using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

using DaveWebService.Models;
using DaveWebService.Controllers;

namespace DaveWebService.Controllers
{
    public class AlbumDataSource
    {
        // this configuration setting would be in .csdef and .cscfg for a cloud service
        // use the development storage rather than a specific storage account
            //private static String dataConnectionString = "UseDevelopmentStorage=true";
        private static CloudStorageAccount storageAccount;

        // for a real storage account:
        //dataConnectionString = "DefaultEndpointsProtocol=https;AccountName=garyclynch;AccountKey=keyvalue";
            //private static String tableName = "AlbumCollectionTable";
        private RecordCollectionContext context;

        //  Static Constructor
        static AlbumDataSource()
        {
            storageAccount = CloudStorageAccount.DevelopmentStorageAccount;

            CloudTableClient.CreateTablesFromModel(
                typeof(RecordCollectionContext),
                storageAccount.TableEndpoint.AbsoluteUri,
                storageAccount.Credentials);
        }

       //   Instance Constructor
        public AlbumDataSource()
        {
            this.context = new RecordCollectionContext(storageAccount.TableEndpoint.AbsoluteUri, storageAccount.Credentials);
            this.context.RetryPolicy = RetryPolicies.Retry(3, TimeSpan.FromSeconds(1));

        }

        public void AddAlbumToAlbumEntity(AlbumEntity newAlbum)
        {
            this.context.AddObject("AlbumEntity", newAlbum);
            this.context.SaveChanges();
        }

        public IEnumerable<AlbumEntity> GetAlbumFromAlbumEntity()
        {
            var results = from al in this.context.AlbumEntity
                          select al;
            return results;
        }
    }
}