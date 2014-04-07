using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaveClientApp.DataClasses
{
    public class AlbumModel
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public int DiscNumber { get; set; }
        public string Label { get; set; }
        public double AlbumValue { get; set; }
    }
}
