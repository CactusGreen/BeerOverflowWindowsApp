using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
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
            var venueList = GetBarData(latitude, longitude, radius);
            var barList = VenueListToBarList(venueList, radius);
            return barList;
        }

        private IEnumerable<Venue> GetBarData (string latitude, string longitude, string radius)
        {
            var categoryIDs = _categoryIdDs.Split(',');
            var venueList = new List<Venue>();

            foreach (var category in categoryIDs)
            {
                var link = string.Format(_apiLink, _clientId, _clientSecret, latitude, longitude, category, radius);
                var jsonStream = GetJsonStream(link);
                venueList.AddRange(JsonConvert.DeserializeObject<SearchResponse>(jsonStream).response.venues);
            }
            return venueList;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius)
        {
            var venueList = await GetBarDataAsync(latitude, longitude, radius);
            var barList = VenueListToBarList(venueList, radius);
            return barList;
        }

        private async Task<IEnumerable<Venue>> GetBarDataAsync(string latitude, string longitude, string radius)
        {
            var categoryIDs = _categoryIdDs.Split(',');
            var venueList = new List<Venue>();

            foreach (var category in categoryIDs)
            {
                var link = string.Format(_apiLink, _clientId, _clientSecret, latitude, longitude, category, radius);
                var jsonStream = await GetJsonStreamAsync(link);
                venueList.AddRange(JsonConvert.DeserializeObject<SearchResponse>(jsonStream).response.venues);
            }
            return venueList;
        }

        private static List<BarData> VenueListToBarList(IEnumerable<Venue> venueList, string radius)
        {
            var radiusNum = int.Parse(radius, CultureInfo.InvariantCulture);
            return (from venue in venueList where (venue.location.distance <= radiusNum) select VenueToBar(venue)).ToList();
        }

        private static BarData VenueToBar(Venue venue)
        {
            var newBar = new BarData
            {
                Title = venue.name,
                Latitude = venue.location.lat,
                Longitude = venue.location.lng
            };
            return newBar;
        }
    }
}
