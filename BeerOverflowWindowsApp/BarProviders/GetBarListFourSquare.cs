using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;
using BeerOverflowWindowsApp.DataModels;
using BeerOverflowWindowsApp.Utilities;
using Newtonsoft.Json;
using static BeerOverflowWindowsApp.DataModels.FourSquareDataModel;

namespace BeerOverflowWindowsApp.BarProviders
{
    class GetBarListFourSquare : JsonFetcher, IBeerable
    {
        private readonly string _apiLink = ConfigurationManager.AppSettings["FourSquareAPILink"];
        private readonly string _clientId = ConfigurationManager.AppSettings["FourSquareClientId"];
        private readonly string _clientSecret = ConfigurationManager.AppSettings["FourSquareClientSecret"];
        private readonly string _categoryIdDs = ConfigurationManager.AppSettings["FourSquareCategoryIDs"];

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            var result = GetBarData(latitude, longitude, radius);
            var barList = VenueListToBars(result, radius);
            return barList;
        }

        private IEnumerable<Venue> GetBarData (string latitude, string longitude, string radius)
        {
            var categoryIDs = _categoryIdDs.Split(',');
            var venueList = new List<Venue>();

            foreach (var category in categoryIDs)
            {
                var link = string.Format(_apiLink, _clientId, _clientSecret, latitude, longitude, category, radius);
                var response = GetJsonStream(link);
                venueList.AddRange(JsonConvert.DeserializeObject<SearchResponse>(response).response.venues);
            }
            return venueList;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius)
        {
            var result = await GetBarDataAsync(latitude, longitude, radius);
            var barList = VenueListToBars(result, radius);
            return barList;
        }

        private async Task<IEnumerable<Venue>> GetBarDataAsync(string latitude, string longitude, string radius)
        {
            var categoryIDs = _categoryIdDs.Split(',');
            var venueList = new List<Venue>();

            foreach (var category in categoryIDs)
            {
                var link = string.Format(_apiLink, _clientId, _clientSecret, latitude, longitude, category, radius);
                var response = await GetJsonStreamAsync(link);
                venueList.AddRange(JsonConvert.DeserializeObject<SearchResponse>(response).response.venues);
            }
            return venueList;
        }

        private List<BarData> VenueListToBars (IEnumerable<Venue> resultData, string radius)
        {
            var barList = new List<BarData>();
            var radiusNum = int.Parse(radius, CultureInfo.InvariantCulture);
            foreach (var result in resultData)
            {
                if (result.location.distance > radiusNum) continue;
                var newBar = new BarData
                {
                    Title = result.name,
                    Latitude = result.location.lat,
                    Longitude = result.location.lng
                };
                barList.Add(newBar);
            }
            return barList;
        }
    }
}
