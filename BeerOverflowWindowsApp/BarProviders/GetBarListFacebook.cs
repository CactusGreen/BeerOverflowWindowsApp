using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using BeerOverflowWindowsApp.DataModels;
using BeerOverflowWindowsApp.Utilities;
using Newtonsoft.Json;
using static BeerOverflowWindowsApp.DataModels.FacebookDataModel;

namespace BeerOverflowWindowsApp.BarProviders
{
    class GetBarListFacebook : JsonFetcher, IBeerable
    {
        private readonly string _apiLink = ConfigurationManager.AppSettings["FacebookAPILink"];
        private readonly string _accessToken = ConfigurationManager.AppSettings["FacebookAccessToken"];
        private readonly string _category = ConfigurationManager.AppSettings["FacebookCategoryID"];
        private readonly string _allowedCategories = ConfigurationManager.AppSettings["FacebookAllowedCategoryStrings"];
        private readonly string _bannedCategories = ConfigurationManager.AppSettings["FacebookBannedCategoryStrings"];
        private readonly string _requestedFields = ConfigurationManager.AppSettings["FacebookRequestedFields"];

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            var placesFromFacebook = GetBarData(latitude, longitude, radius);
            var barList = PlaceListToBarList(placesFromFacebook);
            return barList;
        }

        private IEnumerable<Place> GetBarData(string latitude, string longitude, string radius)
        {
            var link = string.Format(_apiLink, _accessToken, latitude, longitude, radius, _requestedFields, _category);
            var jsonStream = GetJsonStream(link);
            var barList = JsonConvert.DeserializeObject<PlacesResponse>(jsonStream).data;
            return barList;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius)
        {
            var placesFromFacebook = await GetBarDataAsync(latitude, longitude, radius);
            var barList = PlaceListToBarList(placesFromFacebook);
            return barList;
        }

        private async Task<List<Place>> GetBarDataAsync(string latitude, string longitude, string radius)
        {
            var link = string.Format(_apiLink, _accessToken, latitude, longitude, radius, _requestedFields, _category);
            var jsonStream = await GetJsonStreamAsync(link);
            var barList = JsonConvert.DeserializeObject<PlacesResponse>(jsonStream).data;
            return barList;
        }

        private List<BarData> PlaceListToBarList(IEnumerable<Place> resultData)
        {
            var barList = new List<BarData>();
            
            foreach (var result in resultData)
            {
                if (result.restaurant_specialties != null && result.restaurant_specialties.drinks != 1) continue;
                var barCategories = result.category_list.Select(category => category.name).ToList();

                if (!HasCategories(barCategories, _allowedCategories)) continue;
                if (HasCategories(barCategories, _bannedCategories)) continue;
                var newBar = PlaceToBar(result);
                barList.Add(newBar);
            }
            return barList;
        }

        private static BarData PlaceToBar(Place place)
        {
            var newBar = new BarData
            {
                Title = place.name,
                Latitude = place.location.latitude,
                Longitude = place.location.longitude
            };
            return newBar;
        }

        private static bool HasCategories(IEnumerable<string> categories, string bannedCategoriesInString)
        {
            var allowedCategoryList = bannedCategoriesInString.Split(',');
            foreach (var category in categories)
            {
                if (allowedCategoryList.Any(allowed => category.ToLower().Contains(allowed.ToLower())))
                {
                    return true;
                }
            }
            return false;
        }
    }
}