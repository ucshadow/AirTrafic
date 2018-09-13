using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTr.Models
{
    class MapLocation
    {
        public string Name { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }

        public MapLocation()
        {
            Name = "Unknown";
            Lat = -1;
            Lon = -1;
        }

        public override string ToString()
        {
            return $"{Name}, lat {Lat} lon {Lon}";
        }
    }
}
