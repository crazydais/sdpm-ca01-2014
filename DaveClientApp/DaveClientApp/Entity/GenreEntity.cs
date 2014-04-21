using System;
using Microsoft.WindowsAzure.StorageClient;

namespace DaveClientApp.Entity
{
    public class GenreEntity : TableServiceEntity
    {

        //  Constructor - Default
        public GenreEntity()
        {
            RowKey = Guid.NewGuid().ToString();         // generate a Global Unique ID (128 bits) for MessageID (the row key)
        }

        // row key should be unique within a partition, unique across the whole table in fact here
        public string Album { get { return RowKey; } set { RowKey = value; } }
        public string Artist { get { return PartitionKey; } set { PartitionKey = value; } }   // parition key - not unique for an entity
        
        public string Genre_01 { get; set; }
        public string Genre_02 { get; set; }
        public string Genre_03 { get; set; } 



        //  Overriding a method of the GUID class
        public override string ToString()
        {
            return "";          
        }
    }
    
}