using Newtonsoft.Json;
using static BeerOverflowWindowsApp.DataModels.GoogleDataModel;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using BeerOverflowWindowsApp.DataModels;
using BeerOverflowWindowsApp.Utilities;

namespace BeerOverflowWindowsApp.BarProviders
{
    class GetBarListGoogle : JsonFetcher, IBeerable
    {
        private readonly string _apiKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        private readonly string _apiLink = ConfigurationManager.AppSettings["GoogleAPILink"];
        private readonly string _categoryList = ConfigurationManager.AppSettings["GoogleAPICategories"];

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            List<BarData> barList = null;
            var result = GetBarData(latitude, longitude, radius);
            barList = PlacesApiQueryResponseToBars(result);
            return barList;
        }

        private PlacesApiQueryResponse GetBarData(string latitude, string longitude, string radius)
        {
            var categoryList = _categoryList.Split(',');
            var result = new PlacesApiQueryResponse {Results = new List<Result>()};

            foreach (var category in categoryList)
            {
                var link = string.Format(_apiLink, latitude, longitude, radius, category, _apiKey);
                var response = GetJsonStream(link);
                var deserialized = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response);
                result.Results.AddRange(deserialized.Results);
            }
            return result;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius)
        {
            List<BarData> barList = null;

            var result = await GetBarDataAsync(latitude, longitude, radius);
            barList = PlacesApiQueryResponseToBars(result);

            return barList;
        }

        private async Task<PlacesApiQueryResponse> GetBarDataAsync(string latitude, string longitude, string radius)
        {
            var categoryList = _categoryList.Split(',');
            var result = new PlacesApiQueryResponse { Results = new List<Result>() };

            foreach (var category in categoryList)
            {
                var link = string.Format(_apiLink, latitude, longitude, radius, category, _apiKey);
                var jsonStream = await GetJsonStreamAsync(link);
                var deserialized = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(jsonStream);
                result.Results.AddRange(deserialized.Results);
            }
            return result;
        }

        private List<BarData> PlacesApiQueryResponseToBars (PlacesApiQueryResponse resultData)
        {
            var barList = new List<BarData>();
            foreach (var result in resultData.Results)
            {
                var newBar = new BarData
                {
                    Title = result.Name,
                    Latitude = result.Geometry.Location.Lat,
                    Longitude = result.Geometry.Location.Lng
                };
                barList.Add(newBar);
            }
            return barList;
        }
    }
}
