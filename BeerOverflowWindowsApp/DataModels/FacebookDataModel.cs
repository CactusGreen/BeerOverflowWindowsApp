using System.Collections.Generic;

namespace BeerOverflowWindowsApp.DataModels
{
    public class FacebookDataModel
    {
        public class Location
        {
            public string street { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string zip { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class RestaurantSpecialties
        {
            public int breakfast { get; set; }
            public int coffee { get; set; }
            public int dinner { get; set; }
            public int drinks { get; set; }
            public int lunch { get; set; }
        }

        public class CategoryList
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class PlaceInfo
        {
            public string name { get; set; }
            public Location location { get; set; }
            public RestaurantSpecialties restaurant_specialties { get; set; }
            public List<CategoryList> category_list { get; set; }
            public string id { get; set; }
        }

        public class PlacesResponse
        {
            public List<PlaceInfo> data { get; set; }
        }
    }
}
