using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using BeerOverflowWindowsApp.DataModels;
using BeerOverflowWindowsApp.Utilities;
using static BeerOverflowWindowsApp.DataModels.GoogleDataModel;

namespace BeerOverflowWindowsApp.BarProviders
{
    class GetBarListGoogle : IBeerable
    {
        public string ProviderName { get; } = "Google";
        private readonly string _apiKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        private readonly string _apiLink = ConfigurationManager.AppSettings["GoogleAPILink"];
        private readonly string _categoryList = ConfigurationManager.AppSettings["GoogleAPICategories"];
        private readonly IHttpFetcher _fetcher;

        public GetBarListGoogle(IHttpFetcher fetcher)
        {
            _fetcher = fetcher;
        }


        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var placeList = GetBarData(latitude, longitude, radius);
            var barList = PlaceListToBarList(placeList);
            return barList;
        }

        private IEnumerable<Place> GetBarData(string latitude, string longitude, string radius)
        {
            var categoryList = _categoryList.Split(',');
            var placeList = new List<Place>();
            foreach (var category in categoryList)
            {
                var link = string.Format(_apiLink, latitude, longitude, radius, category, _apiKey);
                var deserialized = FetcherAndDeserializer.FetchAndDeserialize<PlacesApiQueryResponse>(link, _fetcher).Results;
                placeList.AddRange(deserialized);
            }
            return placeList;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var placeList = await GetBarDataAsync(latitude, longitude, radius);
            var barList = PlaceListToBarList(placeList);
            return barList;
        }

        private async Task<IEnumerable<Place>> GetBarDataAsync(string latitude, string longitude, string radius)
        {
            var categoryList = _categoryList.Split(',');
            var placeList = new List<Place>();
            foreach (var category in categoryList)
            {
                var link = string.Format(_apiLink, latitude, longitude, radius, category, _apiKey);
                var deserializedResponse = await FetcherAndDeserializer.FetchAndDeserializeAsync<PlacesApiQueryResponse>(link, _fetcher);
                placeList.AddRange(deserializedResponse.Results);
            }
            return placeList;
        }

        private static List<BarData> PlaceListToBarList(IEnumerable<Place> placeList)
        {
            return placeList.Select(PlaceToBar).ToList();
        }

        private static BarData PlaceToBar(Place place)
        {
            return new BarData
            {
                Title = place.Name,
                BarId = place.Name,   // Temporary solution until we decide on BarId 
                Latitude = place.Geometry.Location.Lat,
                Longitude = place.Geometry.Location.Lng,
                Ratings = new List<int>()
            };
        }
    }
}
