using System.Collections.Generic;

namespace WebApi.DataModels
{
    public class GoogleDataModel
    {
        public class Location
        {
            public double Lat { get; set; }
            public double Lng { get; set; }
        }

        public class Geometry
        {
            public Location Location { get; set; }
        }

        public class OpeningHours
        {
            public bool Open_now { get; set; }
            public List<object> Weekday_text { get; set; }
        }

        public class Photo
        {
            public int Height { get; set; }
            public List<string> Html_attributions { get; set; }
            public string Photo_reference { get; set; }
            public int Width { get; set; }
        }

        public class Result
        {
            public Geometry Geometry { get; set; }
            public string Icon { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }
            public OpeningHours Opening_hours { get; set; }
            public List<Photo> Photos { get; set; }
            public string Place_id { get; set; }
            public double Rating { get; set; }
            public string Reference { get; set; }
            public string Scope { get; set; }
            public List<string> Types { get; set; }
            public string Vicinity { get; set; }
        }

        public class PlacesApiQueryResponse
        {
            public List<object> Html_attributions { get; set; }
            public List<Result> Results { get; set; }
            public string Status { get; set; }
            public string next_page_token { get; set; }
        }
    }
}
