using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using WebApi.DataModels;
using static WebApi.DataModels.TripAdvisorDataModel;

namespace WebApi.BarProviders
{
    class GetBarListTripAdvisor : IBeerable
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
            List<BarData> barList = null;
            try
            {
                var result = GetBarData(latitude, longitude);
                barList = ApiQueryResponseToBars(result, radius);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return barList;
        }

        private PlacesResponse GetBarData(string latitude, string longitude)
        {
            PlacesResponse result = null;
            try
            {
                var categories = _categoryListString.Split(',');
                var webClient = new WebClient();
                result = new PlacesResponse() { data = new List<PlaceInfo>() };

                foreach (var category in categories)
                {
                    var response = webClient.DownloadString(string.Format(_mapperLink, latitude, longitude, _accessKey, category));
                    result.data.AddRange(JsonConvert.DeserializeObject<PlacesResponse>(response).data);
                }
                FetchLocations(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return result;
        }

        private List<BarData> ApiQueryResponseToBars(PlacesResponse resultData, string radius)
        {
            var groupList = _applicableGroupsString.Split(',').ToList();
            var groupCategoryList = _applicableGroupCategories.Split(',').ToList();

            var barList = new List<BarData>();
            foreach (var result in resultData.data)
            {
                var distanceMeters = ConvertMilesToMeters(double.Parse(result.distance, CultureInfo.InvariantCulture));
                if ((int) distanceMeters <= int.Parse(radius))
                {
                    if (result.locationResponse.groups != null)
                    {
                        if (result.locationResponse.groups.Exists(x => groupList.Contains(x.name)))
                        {
                            foreach (var group in result.locationResponse.groups)
                            {
                                if (group.categories.Exists(x => groupCategoryList.Contains(x.name)))
                                {
                                    AddTripAdvisorPlaceToBars(barList, result);
                                    break;
                                }
                            }
                        }
                    }
                    else if (result.locationResponse.subcategory == null ||
                        result.locationResponse.subcategory.Exists(x => x.name == "sit_down"))
                    {
                        AddTripAdvisorPlaceToBars(barList, result);
                    }
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
                Longitude = double.Parse(place.locationResponse.longitude, CultureInfo.InvariantCulture),
                Ratings = new List<int>()
            };
            barList.Add(newBar);
        }

        private static double ConvertMilesToMeters(double miles)
        {
            return miles * 1.609344 * 1000;
        }

        private void GetLocationForPlace(PlaceInfo place)
        {
            try
            {
                var webClient = new WebClient();
                var response =
                    webClient.DownloadString(string.Format(_locationApiLink, place.location_id, _accessKey));
                var result = JsonConvert.DeserializeObject<LocationResponse>(response);
                place.locationResponse = result;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void FetchLocations(PlacesResponse result)
        {
            foreach (var place in result.data)
            {
                GetLocationForPlace(place);
            }
        }
    }
}
