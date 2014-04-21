using System;
using Microsoft.WindowsAzure.StorageClient;

namespace DaveWebService.Entity
{
    public class TrackEntity : TableServiceEntity
    {

        //  Constructor - Default
        public TrackEntity()
        {
            RowKey = Guid.NewGuid().ToString();         // generate a Global Unique ID (128 bits) for MessageID (the row key)
        }

        // row key should be unique within a partition, unique across the whole table in fact here
        public string Album { get { return RowKey; } set { RowKey = value; } }
        public string Artist { get { return PartitionKey; } set { PartitionKey = value; } }   // parition key - not unique for an entity

        public int NumberOfTracks { get; set; }
        public string Track_01_Title { get; set; }
        public string Track_02_Title { get; set; }
        public string Track_03_Title { get; set; }
        public string Track_04_Title { get; set; }
        public string Track_05_Title { get; set; }


        //  Overriding a method of the GUID class
        public override string ToString()
        {
            return "";          
        }
    }
    
}