using System;
using Newtonsoft.Json;
using static WebApi.DataModels.GoogleDataModel;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using WebApi.DataModels;

namespace WebApi.BarProviders
{
    class GetBarListGoogle : IBeerable
    {
        private readonly string _apiKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        private readonly string _apiLink = ConfigurationManager.AppSettings["GoogleAPILink"];
        private readonly string _categoryList = ConfigurationManager.AppSettings["GoogleAPICategories"];

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            List<BarData> barList = null;
            try
            {
                var result = GetBarData(latitude, longitude, radius);
                barList = PlacesApiQueryResponseToBars(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return barList;
        }

        private PlacesApiQueryResponse GetBarData(string latitude, string longitude, string radius)
        {
            var categoryList = _categoryList.Split(',');
            var result = new PlacesApiQueryResponse {Results = new List<Result>()};

            foreach (var category in categoryList)
            {
                try
                {
                    var webClient = new WebClient { Encoding = Encoding.UTF8 };
                    var response = webClient.DownloadString(string.Format(_apiLink, latitude, longitude, radius, category, _apiKey));
                    var deserialized = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response);
                    result.Results.AddRange(deserialized.Results);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
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
                    Longitude = result.Geometry.Location.Lng,
                    Ratings = new List<int>()
                };
                barList.Add(newBar);
            }
            return barList;
        }
    }
}
