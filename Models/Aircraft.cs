using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTr.Models
{
    public class Aircraft : INotifyPropertyChanged
    {
        private float _originLat;
        public float OriginLat {
            get { return _originLat; }
            set {
                if (_originLat != value) { _originLat = value; OnPropertyChanged("OriginLat"); } } }                     // From


        private float _originLon;
        public float OriginLon {
            get { return _originLon; }
            set {
                if (_originLon != value) { _originLon = value; OnPropertyChanged("OriginLon"); } } }                   // To


        private float _destinationLat;
        public float DestinationLat {
            get { return _destinationLat; }
            set { if (_destinationLat != value) { _destinationLat = value; OnPropertyChanged("DestinationLat"); } } }                   // To


        private float _destinationLon;
        public float DestinationLon {
            get { return _destinationLon; }
            set { if (_destinationLon != value) { _destinationLon = value; OnPropertyChanged("DestinationLon"); } }
        }                     // To


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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
