using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AirTr.Util.Helpers;

namespace AirTr.Classes
{

    // toDo: move IO to async
    public static class FileHandler
    {

        private static List<string> _allLocations = new List<string>();

        /// <summary>
        /// location file should be of form
        /// location name:latitude,longitude|location name:latitude,longitude|location name:latitude,longitude
        /// </summary>
        /// <param name="address"></param>
        public static Tuple<float, float> LocationExists(string address)
        {
            if(_allLocations.Count == 0)
            {
                var locations = File.ReadAllText(Directory.GetCurrentDirectory() + @"\locations");
                var locationsArr = locations.Split('|');
                _allLocations = locationsArr.ToList();
            }

            foreach(var l in _allLocations)
            {
                var locationName = l.Split(':')[0];
                if(locationName == address)
                {
                    //Print($"{address} found in {l}");
                    var data = l.Split(':')[1].Split(',');
                    //Print($"data is {data[0]} and {data[1]}");
                    return new Tuple<float, float>(float.Parse(data[0]), float.Parse(data[1]));
                }
            }

            return null;
        }

        public static void AddLocation(string locationName, float lat, float lon)
        {
            foreach(var s in _allLocations)
            {
                if(s.Split(':')[0] == locationName)
                {
                    return;
                }
            }
            Print($"Adding new location {locationName}", ConsoleColor.Green);
            File.AppendAllText(Directory.GetCurrentDirectory() + @"\locations", $"{locationName}:{lat},{lon}|");
            _allLocations.Add($"{locationName}:{lat},{lon}");
        }

        public static void CreateLocationFile()
        {
            var dir = Directory.GetCurrentDirectory();
            if(!File.Exists(dir + @"\locations"))
            {
                Console.WriteLine("Creating locations cache file");
                File.Create(dir + @"\locations");
            }
        }
    }
}
