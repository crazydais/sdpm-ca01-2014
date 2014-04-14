using System;
using Microsoft.WindowsAzure.StorageClient;

namespace DaveWebService.Models
{
    public class AlbumEntity : TableServiceEntity
    {

        //  Constructor - Default
        public AlbumEntity()
        {
            RowKey = Guid.NewGuid().ToString();         // generate a Global Unique ID (128 bits) for MessageID (the row key)
        }

        public String Artist                              // parition key - not unique for an entity
        {
            get
            {
                return PartitionKey;
            }
            set
            {
                PartitionKey = value;
            }
        }

        // row key should be unique within a partition, unique across the whole table in fact here
        public String Album
        {
            get
            {
                return RowKey;
            }
            set
            {
                RowKey = value;
            }
        }

        public String Label { get; set; }                  
        public double AlbumValue { get; set; }
        public int DiscNumber { get; set; }

        //  Overriding a method of the GUID class
        public override string ToString()
        {
            return Album + " ~by~ " + Artist + ". Label: " + Label + ". Value: " + AlbumValue;          
        }
    }
    
}