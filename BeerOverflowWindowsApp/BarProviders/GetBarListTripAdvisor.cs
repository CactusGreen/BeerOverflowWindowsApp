using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BeerOverflowWindowsApp.DataModels;
using BeerOverflowWindowsApp.Utilities;
using static BeerOverflowWindowsApp.DataModels.TripAdvisorDataModel;

namespace BeerOverflowWindowsApp.BarProviders
{
    class GetBarListTripAdvisor : JsonFetcher, IBeerable
    {
        private readonly string _accessKey = ConfigurationManager.AppSettings["TripAdvisorAccessKey"];
        private readonly string _mapperLink = ConfigurationManager.AppSettings["TripAdvisorMapperLink"];
        private readonly string _locationApiLink = ConfigurationManager.AppSettings["TripAdvisorLocationAPILink"];
        private readonly string _categoryListString = ConfigurationManager.AppSettings["TripAdvisorCategories"];
        private readonly string _applicableGroupsString = ConfigurationManager.AppSettings["TripAdvisorApplicableGroups"];
        private readonly string _applicableGroupCategories = ConfigurationManager.AppSettings["TripAdvisorApplicableGroupCategories"];

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            List<BarData> barList = null;
            var result = GetBarData(latitude, longitude);
            barList = ApiQueryResponseToBars(result, radius);
            return barList;
        }

        private PlacesResponse GetBarData(string latitude, string longitude)
        {
            PlacesResponse result = null;

            var categories = _categoryListString.Split(',');
            result = new PlacesResponse() { data = new List<PlaceInfo>() };

            foreach (var category in categories)
            {
                var link = string.Format(_mapperLink, latitude, longitude, _accessKey, category);
                var response = GetJsonStream(link);
                result.data.AddRange(JsonConvert.DeserializeObject<PlacesResponse>(response).data);
            }
            FetchLocations(result);

            return result;
        }

        private void FetchLocations(PlacesResponse result)
        {
            foreach (var place in result.data)
            {
                GetLocationForPlace(place);
            }
        }

        private void GetLocationForPlace(PlaceInfo place)
        {
            var link = string.Format(_locationApiLink, place.location_id, _accessKey);
            var response = GetJsonStream(link);
            var result = JsonConvert.DeserializeObject<LocationResponse>(response);
            place.locationResponse = result;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius)
        {
            List<BarData> barList = null;
            var result = await GetBarDataAsync(latitude, longitude);
            barList = ApiQueryResponseToBars(result, radius);
            return barList;
        }

        private async Task<PlacesResponse> GetBarDataAsync(string latitude, string longitude)
        {
            PlacesResponse result = null;

            var categories = _categoryListString.Split(',');
            result = new PlacesResponse() { data = new List<PlaceInfo>() };

            foreach (var category in categories)
            {
                var response = await GetJsonStreamAsync(string.Format(_mapperLink, latitude, longitude, _accessKey, category));
                result.data.AddRange(JsonConvert.DeserializeObject<PlacesResponse>(response).data);
            }
            await FetchLocationsAsync(result);

            return result;
        }

        private async Task FetchLocationsAsync(PlacesResponse result)
        {
            foreach (var place in result.data)
            {
                await GetLocationForPlaceAsync(place);
            }
        }

        private async Task GetLocationForPlaceAsync(PlaceInfo place)
        {
            var link = string.Format(_locationApiLink, place.location_id, _accessKey);
            var response = await GetJsonStreamAsync(link);
            var result = JsonConvert.DeserializeObject<LocationResponse>(response);
            place.locationResponse = result;
        }

        private List<BarData> ApiQueryResponseToBars(PlacesResponse resultData, string radius)
        {
            var groupList = _applicableGroupsString.Split(',').ToList();
            var groupCategoryList = _applicableGroupCategories.Split(',').ToList();

            var barList = new List<BarData>();
            foreach (var result in resultData.data)
            {
                var distanceMeters = ConvertMilesToMeters(double.Parse(result.distance, CultureInfo.InvariantCulture));
                if ((int) distanceMeters > int.Parse(radius)) continue;
                if (result.locationResponse.groups != null)
                {
                    if (result.locationResponse.groups.Exists(x => groupList.Contains(x.name)) &&
                        result.locationResponse.groups.Any(group => group.categories.Exists(x => groupCategoryList.Contains(x.name))))
                    {
                        AddTripAdvisorPlaceToBars(barList, result);
                    }
                }
                else if (result.locationResponse.subcategory == null ||
                         result.locationResponse.subcategory.Exists(x => x.name == "sit_down"))
                {
                    AddTripAdvisorPlaceToBars(barList, result);
                }
            }
            return barList;
        }

        private void AddTripAdvisorPlaceToBars(ICollection<BarData> barList, PlaceInfo place)
        {
            var newBar = new BarData
            {
                Title = place.name,
                Latitude = double.Parse(place.locationResponse.latitude, CultureInfo.InvariantCulture),
                Longitude = double.Parse(place.locationResponse.longitude, CultureInfo.InvariantCulture)
            };
            barList.Add(newBar);
        }

        private static double ConvertMilesToMeters(double miles)
        {
            return miles * 1.609344 * 1000;
        }
    }
}
