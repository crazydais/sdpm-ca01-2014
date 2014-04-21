using System;
using Microsoft.WindowsAzure.StorageClient;

namespace DaveWebService.Entity
{
    public class AlbumEntity : TableServiceEntity
    {

        //  Constructor - Default
        public AlbumEntity()
        {
            RowKey = Guid.NewGuid().ToString();         // generate a Global Unique ID (128 bits) for MessageID (the row key)
        }

        // row key should be unique within a partition, unique across the whole table in fact here
        public string Album { get { return RowKey; } set { RowKey = value; } }
        public string Artist { get { return PartitionKey; } set { PartitionKey = value; } }   // parition key - not unique for an entity
        public string Label { get; set; }                  
        public double AlbumValue { get; set; }
        int _Rating = 0;
        public int Rating 
        { 
            get { return _Rating; } 
            set 
            { 
                if(value < 0) { _Rating = 0; }
                else if (value > 5) { _Rating = 5; }
            }
        }

        //  Overriding a method of the GUID class
        public override string ToString()
        {
            return Album + " ~by~ " + Artist + ". Label: " + Label + ". Value: " + AlbumValue;          
        }
    }
    
}