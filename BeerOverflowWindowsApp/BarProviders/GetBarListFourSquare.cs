using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using FourSquare.SharpSquare.Core;
using FourSquare.SharpSquare.Entities;
using BeerOverflowWindowsApp.DataModels;

namespace BeerOverflowWindowsApp.BarProviders
{
    class GetBarListFourSquare : IBeerable
    {
        private readonly string _clientId = ConfigurationManager.AppSettings["FourSquareClientId"];
        private readonly string _clientSecret = ConfigurationManager.AppSettings["FourSquareClientSecret"];
        private readonly string _categoryIdDs = ConfigurationManager.AppSettings["FourSquareCategoryIDs"];

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);

            // FourSquare doesn't like when the coordinates are (0,0), so we change it to something close.
            // Should this be done with exceptions?
            if (double.Parse(latitude, CultureInfo.InvariantCulture) == 0 && double.Parse(longitude, CultureInfo.InvariantCulture) == 0)
            {
                latitude = "0.000001";
                longitude = "0.000001";
            }

            List<BarData> barList = null;
            var result = GetBarData(latitude, longitude, radius);
            barList = VenueListToBars(result);
            return barList;
        }

        private IEnumerable<Venue> GetBarData (string latitude, string longitude, string radius)
        {
            var sharpSquare = new SharpSquare(_clientId, _clientSecret);
            var categoryIDs = _categoryIdDs.Split(',');
            var venueList = new List<Venue>();

            foreach (var id in categoryIDs)
            {
                // let's build the query
                var parameters = new Dictionary<string, string>
                {
                    { "ll", latitude + "," + longitude }, // Coords
                    { "radius", radius },
                    { "categoryId", id },
                    { "intent", "browse" }
                };
                venueList.AddRange(sharpSquare.SearchVenues(parameters));
            }
            return venueList;
        }

        private List<BarData> VenueListToBars (IEnumerable<Venue> resultData)
        {
            var barList = new List<BarData>();
            foreach (var result in resultData)
            {
                var newBar = new BarData
                {
                    Title = result.name,
                    Latitude = result.location.lat,
                    Longitude = result.location.lng,
                    Ratings = new List<int>()
                };
                barList.Add(newBar);
            }
            return barList;
        }
    }
}
