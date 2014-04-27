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
        double _Rating = -1;
        public double Rating 
        { 
            get 
            {
                if (_Rating == -1) { return 0.0; }
                else{return _Rating;} 
            } 
            set 
            { 
                if(value < 0.0) { _Rating = 0.0; }
                else if (value > 5.0) { _Rating = 5.0; }
                else { _Rating = value; }
            }
        }

        //  Overriding a method of the GUID class
        public override string ToString()
        {
            return Album + " - " + Artist + "\n\t\t\t\tLabel: " + Label + "Value: EUR: " + AlbumValue + ". Rating: " + Rating;          
        }
    }
    
}