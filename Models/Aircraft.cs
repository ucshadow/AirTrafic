using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTr.Models
{
    class Aircraft
    {
        public float OriginLat { get; set; }                     // From
        public float OriginLon { get; set; }                   // To
        public float DestinationLat { get; set; }                   // To
        public float DestinationLon { get; set; }                   // To
        public int Id { get; set; }
        public string Identifier { get; set; }                       // Icao
        public int Altitude { get; set; }                           // Alt
        public float Lat { get; set; }
        public float Lon { get; set; }
        public long Time { get; set; }                              // PosTime
        public float Speed { get; set; }                            // Spd, speed in knots
        public string Type { get; set; }
        public string Model { get; set; }                           // Mdl
        public string Manufacturer { get; set; }                    // Man
        public string Year { get; set; }
        public string Operator { get; set; }                        // Op
        public int VerticalSpeed { get; set; }                      // Vsi
        public string Turbulence { get; set; }                      // WTC, 0 = None 1 = Light 2 = Medium 3 = Heavy
        public string Species { get; set; }                         // 0 = None 1 = Land Plane 2 = Sea Plane 3 = Amphibian 4 = Helicopter 5 = Gyrocopter 6 = Tiltwing 7 = Ground Vehicle 8 = Tower
        public bool Military { get; set; }                          // Mil
        public string Country { get; set; }                         // Cou 
        public string Call { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
