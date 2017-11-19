using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace BeerOverflowWindowsApp.FileIO
{
    static class BarFileReader
    {
        private static readonly string _filePath = ConfigurationManager.AppSettings["filePath"];

        public static BarDataModel GetAllBarData()
        {
            BarDataModel result = null;
            if (File.Exists(_filePath))
            {
                var barsData = File.ReadAllText(_filePath);
                result = JsonConvert.DeserializeObject<BarDataModel>(barsData);
            }
            if (result == null) { result = new BarDataModel(); }
            return result;
        }

        public static List<int> GetBarRatings(BarData bar)
        {
            var barList = GetAllBarData();
            var barInDatabase = barList.Find(x => x.Title == bar.Title);
            //var barsRatings = barInDatabase?.Ratings;
            //return barsRatings;
            return null;
        }
    }
}
