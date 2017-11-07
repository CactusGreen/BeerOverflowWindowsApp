using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;

namespace BeerOverflowWindowsApp
{
    static class BarFileReader
    {
        public static BarDataModel GetAllBarData()
        {
            var webClient = new WebClient();
            var response = webClient.DownloadString("http://localhost:13623/Api/BarData/GetAllBars");

            var result = JsonConvert.DeserializeObject<BarDataModel>(response);

            if (result == null) { result = new BarDataModel(); }
            return result;
        }

        public static List<int> GetBarRatings(BarData bar)
        {
            var barList = GetAllBarData();
            var barInDatabase = barList.Find(x => x.Title == bar.Title);
            var barsRatings = barInDatabase?.Ratings;
            return barsRatings;
        }
    }
}
