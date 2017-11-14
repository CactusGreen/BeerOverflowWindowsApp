using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using WebApi.DataModels;
using Newtonsoft.Json;
using static WebApi.DataModels.FacebookDataModel;

namespace WebApi.BarProviders
{
    class GetBarListFacebook : IBeerable
    {
        private readonly string _apiLink = ConfigurationManager.AppSettings["FacebookAPILink"];
        private readonly string _accessToken = ConfigurationManager.AppSettings["FacebookAccessToken"];
        private readonly string _category = ConfigurationManager.AppSettings["FacebookCategoryID"];
        private readonly string _allowedCategories = ConfigurationManager.AppSettings["FacebookAllowedCategoryStrings"];
        private readonly string _bannedCategories = ConfigurationManager.AppSettings["FacebookBannedCategoryStrings"];
        private readonly string _requestedFields = ConfigurationManager.AppSettings["FacebookRequestedFields"];

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var result = GetBarData(latitude, longitude, radius);
            var barList = FacebookDataToBars(result);
            return barList;
        }

        private PlacesResponse GetBarData(string latitude, string longitude, string radius)
        {
            PlacesResponse result;
            try
            {
                var webClient = new WebClient();
                var response = webClient.DownloadString(
                    string.Format(_apiLink, _accessToken, latitude+","+longitude, radius, _requestedFields, _category));
                result = JsonConvert.DeserializeObject<PlacesResponse>(response);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return result;
        }

        private List<BarData> FacebookDataToBars(PlacesResponse resultData)
        {
            var barList = new List<BarData>();
            
            foreach (var result in resultData.data)
            {
                if (result.restaurant_specialties != null && result.restaurant_specialties.drinks != 1) continue;
                var barCategories = result.category_list.Select(category => category.name).ToList();

                if (!HasCategories(barCategories, _allowedCategories)) continue;
                if (HasCategories(barCategories, _bannedCategories)) continue;
                var newBar = new BarData()
                {
                    Title = result.name,
                    Latitude = result.location.latitude,
                    Longitude = result.location.longitude,
                    Ratings = new List<int>()
                };
                barList.Add(newBar);
            }
            return barList;
        }

        private bool HasCategories(IEnumerable<string> categories, string bannedCategoriesInString)
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
