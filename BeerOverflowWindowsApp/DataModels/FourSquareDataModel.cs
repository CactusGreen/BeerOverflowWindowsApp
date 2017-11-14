using System.Collections.Generic;

namespace BeerOverflowWindowsApp.DataModels
{
    public class FourSquareDataModel
    {
        public class MetaInfo
        {
            public int code { get; set; }
            public string requestId { get; set; }
        }

        public class LabeledLatLng
        {
            public string label { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Location
        {
            public string address { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
            public List<LabeledLatLng> labeledLatLngs { get; set; }
            public int distance { get; set; }
            public string postalCode { get; set; }
            public string cc { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public List<string> formattedAddress { get; set; }
            public string crossStreet { get; set; }
        }

        public class Icon
        {
            public string prefix { get; set; }
            public string suffix { get; set; }
        }

        public class Category
        {
            public string id { get; set; }
            public string name { get; set; }
            public string pluralName { get; set; }
            public string shortName { get; set; }
            public Icon icon { get; set; }
            public bool primary { get; set; }
        }


        public class Venue
        {
            public string id { get; set; }
            public string name { get; set; }
            public Location location { get; set; }
            public List<Category> categories { get; set; }
            public bool verified { get; set; }
            public List<object> venueChains { get; set; }
            public bool hasPerk { get; set; }
        }

        public class Response
        {
            public List<Venue> venues { get; set; }
        }

        public class SearchResponse
        {
            public MetaInfo meta { get; set; }
            public Response response { get; set; }
        }
    }
}