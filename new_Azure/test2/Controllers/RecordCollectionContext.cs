using System;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

using DaveWebService.Entity;

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

        public IQueryable<GenreEntity> GenreEntity
        {
            get { return this.CreateQuery<GenreEntity>("GenreEntity"); }
        }

        public IQueryable<TrackEntity> TrackEntity
        {
            get { return this.CreateQuery<TrackEntity>("TrackEntity"); }
        }
    }
}