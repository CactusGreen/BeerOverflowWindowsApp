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
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var placeList = GetBarData(latitude, longitude);
            var barList = PlaceListToBarList(placeList, radius);
            return barList;
        }

        private IEnumerable<PlaceInfo> GetBarData(string latitude, string longitude)
        {
            var categories = _categoryListString.Split(',');
            var placeList =  new List<PlaceInfo>();

            foreach (var category in categories)
            {
                var link = string.Format(_mapperLink, latitude, longitude, _accessKey, category);
                var jsonStream = GetJsonStream(link);
                placeList.AddRange(JsonConvert.DeserializeObject<PlacesResponse>(jsonStream).data);
            }
            FetchLocations(placeList);

            return placeList;
        }

        private void FetchLocations(IEnumerable<PlaceInfo> placeList)
        {
            foreach (var place in placeList)
            {
                place.locationResponse = GetLocationForPlace(place);
            }
        }

        private LocationResponse GetLocationForPlace(PlaceInfo place)
        {
            var link = string.Format(_locationApiLink, place.location_id, _accessKey);
            var jsonStream = GetJsonStream(link);
            var placeLocation = JsonConvert.DeserializeObject<LocationResponse>(jsonStream);
            return placeLocation;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var placeList = await GetBarDataAsync(latitude, longitude);
            var barList = PlaceListToBarList(placeList, radius);
            return barList;
        }

        private async Task<List<PlaceInfo>> GetBarDataAsync(string latitude, string longitude)
        {
            var categories = _categoryListString.Split(',');
            var placeList = new List<PlaceInfo>();

            foreach (var category in categories)
            {
                var jsonStream = await GetJsonStreamAsync(string.Format(_mapperLink, latitude, longitude, _accessKey, category));
                placeList.AddRange(JsonConvert.DeserializeObject<PlacesResponse>(jsonStream).data);
            }
            await FetchLocationsAsync(placeList);

            return placeList;
        }

        private async Task FetchLocationsAsync(IEnumerable<PlaceInfo> placeList)
        {
            foreach (var place in placeList)
            {
                place.locationResponse = await GetLocationForPlaceAsync(place);
            }
        }

        private async Task<LocationResponse> GetLocationForPlaceAsync(PlaceInfo place)
        {
            var link = string.Format(_locationApiLink, place.location_id, _accessKey);
            var jsonStream = await GetJsonStreamAsync(link);
            var placeLocation = JsonConvert.DeserializeObject<LocationResponse>(jsonStream);
            return placeLocation;
        }

        private List<BarData> PlaceListToBarList(IEnumerable<PlaceInfo> placeList, string radius)
        {
            var groupList = _applicableGroupsString.Split(',').ToList();
            var groupCategoryList = _applicableGroupCategories.Split(',').ToList();

            var barList = new List<BarData>();
            foreach (var place in placeList)
            {
                var distanceMeters = ConvertMilesToMeters(double.Parse(place.distance, CultureInfo.InvariantCulture));
                if ((int) distanceMeters > int.Parse(radius, CultureInfo.InvariantCulture)) continue;
                if (place.locationResponse.groups != null)
                {
                    if (place.locationResponse.groups.Exists(x => groupList.Contains(x.name)) &&
                        place.locationResponse.groups.Any(group => group.categories.Exists(x => groupCategoryList.Contains(x.name))))
                    {
                        barList.Add(PlaceToBar(place));
                    }
                }
                else if (place.locationResponse.subcategory == null ||
                         place.locationResponse.subcategory.Exists(x => x.name == "sit_down"))
                {
                    barList.Add(PlaceToBar(place));
                }
            }
            return barList;
        }

        private static BarData PlaceToBar(PlaceInfo place)
        {
            var newBar = new BarData
            {
                Title = place.name,
                BarId = place.name,   // Temporary solution until we decide on BarId 
                Latitude = double.Parse(place.locationResponse.latitude, CultureInfo.InvariantCulture),
                Longitude = double.Parse(place.locationResponse.longitude, CultureInfo.InvariantCulture),
                Ratings = new List<int>()
            };
            return newBar;
        }

        private static double ConvertMilesToMeters(double miles)
        {
            return miles * 1.609344 * 1000;
        }
    }
}
