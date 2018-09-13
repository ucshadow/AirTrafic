using AirTr.Interfaces;
using AirTr.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AirTr
{
    class AirController : IAirController
    {

        private string _airQuery = "https://public-api.adsbexchange.com/VirtualRadar/AircraftList.json?lat=55.8581&lng=9.8476&fDstL=0&fDstU=100";
        private string _inversaeGeoLocation = "https://geocode.xyz/ENGM%20Oslo%20Gardermoen,%20Norway?json=1";

        public ObservableCollection<Aircraft> AircraftList { get; set; }

        public AirController()
        {
            AircraftList = new ObservableCollection<Aircraft>();
        }

        public void GetAircrafts(MapLocation centerLocation, int distance)
        {
            using(var c = new WebClient())
            {
                c.DownloadDataAsync(new Uri($"https://public-api.adsbexchange.com/VirtualRadar/AircraftList.json?" +
                    $"lat={centerLocation.Lat}&lng={centerLocation.Lon}&fDstL=0&fDstU={distance}"));

                c.DownloadDataCompleted += AirDataCompleted;
            }
        }

        public void GetAircrafts(float latitude, float longitude, int distance)
        {
            using (var c = new WebClient())
            {
                c.DownloadDataAsync(new Uri($"https://public-api.adsbexchange.com/VirtualRadar/AircraftList.json?" +
                    $"lat={latitude}&lng={longitude}&fDstL=0&fDstU={distance}"));

                c.DownloadDataCompleted += AirDataCompleted;
            }
        }

        private void QueryGeoLocation(Aircraft s) // todo: add caching!
        {
            if(s.From.Length > 2)
            {
                using (var c = new WebClient())
                {
                    c.DownloadDataAsync(new Uri($"https://geocode.xyz/" +
                        $"{s.From}?json=1"));

                    c.DownloadDataCompleted += (sender, e) => FromGeoDataComplete(sender, e, s);
                }
            }

            if(s.To.Length > 2)
            {
                using (var c = new WebClient())
                {
                    var u = new Uri($"https://geocode.xyz/" + $"{s.To}?json=1");
                    Console.WriteLine(u);
                    c.DownloadDataAsync(u);

                    c.DownloadDataCompleted += (sender, e) => ToGeoDataComplete(sender, e, s);
                }
            }
        }

        private void ToGeoDataComplete(object sender, DownloadDataCompletedEventArgs e, Aircraft aircraft)
        {
            var data = e.Result;
            var asString = Encoding.UTF8.GetString(data);
            var res = JObject.Parse(asString);
            Console.WriteLine($"To {res}");
            var location = new MapLocation
            {
                Name = (string)res["standard"]["city"],
                Lat = (float)res["latt"],
                Lon = (float)res["longt"]
            };
            Console.WriteLine(location);
            ChangeDestination(aircraft, (float)res["longt"], (float)res["latt"]);
        }

        private void FromGeoDataComplete(object sender, DownloadDataCompletedEventArgs e, Aircraft aircraft)
        {
            var data = e.Result;
            var asString = Encoding.UTF8.GetString(data);
            var res = JObject.Parse(asString);
            Console.WriteLine($"From {res}");
            ChangeOrigin(aircraft, (float)res["longt"], (float)res["latt"]);
        }

        private string TryParseString(Object o)
        {
            try
            {
                var s = (string)o;
                return s;
            } catch (Exception e)
            {
                return "Unknown";
            }
        }

        private void QueryGeoLocation() // todo: add caching!
        {
            foreach(var s in AircraftList)
            {
                using (var c = new WebClient())
                {
                    c.DownloadDataAsync(new Uri($"https://geocode.xyz/" +
                        $"{s.From}?json=1"));

                    c.DownloadDataCompleted += (sender, e) => OriginGeoDataComplete(sender, e, s);
                }
            }
        }

        private void OriginGeoDataComplete(object sender, DownloadDataCompletedEventArgs e, Aircraft aircraft)
        {
            var data = e.Result;
            var asString = Encoding.UTF8.GetString(data);
            var res = JObject.Parse(asString);
            ChangeOrigin(aircraft, (float)res["longt"], (float)res["latt"]);
        }

        private void ChangeOrigin(Aircraft aircrfat, float lon, float lat)
        {
            foreach(var a in AircraftList)
            {
                if(a.Id == aircrfat.Id)
                {
                    a.OriginLon = lon;
                    a.OriginLat = lat;
                    return;
                }
            }
        }

        private void ChangeDestination(Aircraft aircrfat, float lon, float lat)
        {
            foreach (var a in AircraftList)
            {
                if (a.Id == aircrfat.Id)
                {
                    a.DestinationLon = lon;
                    a.DestinationLat = lat;
                    return;
                }
            }
        }

        private void AirDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            var data = e.Result;
            var asString = Encoding.UTF8.GetString(data);
            var res = JObject.Parse(asString);

            var dataArray = (JArray) res["acList"];

            foreach(var d in dataArray)
            {

                //var Id = d["Id"];
                //var Identifier = d["Icao"];
                //var Altitude = d["Alt"];
                //var Lat = d["Lat"];
                //var Lon = d["Long"];
                //var Time = d["PosTime"];
                //var Speed = d["Spd"];
                //var Type = d["Type"];
                //var Model = d["Mdl"];
                //var Manufacturer = d["Man"];
                //var Year = d["Year"];
                //var Operator = d["Op"];
                //var VerticalSpeed = d["Vsi"];
                //var Turbulence = d["WTC"];
                //var Species = d["Species"];
                //var Military = d["Mil"];
                //var Country = d["Cou"];
                //var Call = d["Call"];
                //var From = d["From"];
                //var To = d["To"];

                var airCraft = new Aircraft
                {
                    Id = d["Id"] == null ? -1 : (int)d["Id"],
                    Identifier = d["Icao"] == null ? "Unknown" : (string)d["Icao"],
                    Altitude = d["Alt"] == null ? -1 : (int)d["Alt"],
                    Lat = d["Lat"] == null ? -1 : (float)d["Lat"],
                    Lon = d["Long"] == null ? -1f : (float)d["Long"],
                    Time = d["PosTime"] == null ? -1 : (long)d["PosTime"],
                    Speed = d["Spd"] == null ? -1f : (float)d["Spd"],
                    Type = d["Type"] == null ? "Unknown" : (string)d["Type"],
                    Model = d["Mdl"] == null ? "Unknown" : (string)d["Mdl"],
                    Manufacturer = d["Man"] == null ? "Unknown" : (string)d["Man"],
                    Year = d["Year"] == null ? "Unknown" : (string)d["Year"],
                    Operator = d["Op"] == null ? "Unknown" : (string)d["Op"],
                    VerticalSpeed = d["Vsi"] == null ? -1 : (int)d["Vsi"],
                    Turbulence = d["WTC"] == null ? "Unknown" : ParseTurbulence((int)d["WTC"]),
                    Species = d["Species"] == null ? "Unknown" : ParseSpecies((int)d["Species"]),
                    Military = d["Mil"] == null ? false : (bool)d["Mil"],
                    Country = d["Cou"] == null ? "Unknown" : (string)d["Cou"],
                    Call = d["Call"] == null ? "Unknown" : (string)d["Call"],
                    From = d["From"] == null ? "Stockholm-Arlanda, Stockholm, Sweden" : (string)d["From"],
                    To = d["To"] == null ? "Billund, Denmark" : (string)d["To"],
                };
                if(airCraft != null)
                {
                    AircraftList.Add(airCraft);
                    QueryGeoLocation(airCraft);
                }
            }
        }

        private string ParseTurbulence(int n)
        {
            if(n == 0)
            {
                return "None";
            }
            if(n == 1)
            {
                return "Light";
            }
            if (n == 2)
            {
                return "Medium";
            }
            if (n == 3)
            {
                return "Heavy";
            }
            return "None";
        }

        private string ParseSpecies(int n)
        {
            if (n == 0)
            {
                return "None";
            }
            if (n == 1)
            {
                return "Land Plane";
            }
            if (n == 2)
            {
                return "Sea Plane";
            }
            if (n == 3)
            {
                return "Amphibian";
            }
            if (n == 4)
            {
                return "Helicopter";
            }
            if (n == 5)
            {
                return "Gyrocopter";
            }
            if (n == 6)
            {
                return "Tiltwing";
            }
            if (n == 7)
            {
                return "Ground Vehicle";
            }
            if (n == 8)
            {
                return "Tower";
            }
            return "None";
        }
    }
}
