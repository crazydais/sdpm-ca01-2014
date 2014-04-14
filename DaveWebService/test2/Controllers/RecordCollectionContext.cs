using System;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

using DaveWebService.Models;

namespace DaveWebService.Controllers
{
    public class RecordCollectionContext : TableServiceContext
    {
        public RecordCollectionContext(string tableAddress, StorageCredentials cred)
            : base(tableAddress, cred)
        {

        }

        public IQueryable<AlbumEntity> AlbumEntity
        {
            get { return this.CreateQuery<AlbumEntity>("AlbumEntity"); }
        }
    }
}