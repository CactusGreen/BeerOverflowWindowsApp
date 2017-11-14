using System.Collections.Generic;

namespace WebApi.DataModels
{
    class TripAdvisorDataModel
    {
        public class AddressObj
        {
            public string street1 { get; set; }
            public string street2 { get; set; }
            public string city { get; set; }
            public object state { get; set; }
            public string country { get; set; }
            public string postalcode { get; set; }
            public string address_string { get; set; }
        }

        public class Subcategory
        {
            public string name { get; set; }
            public string localized_name { get; set; }
        }

        public class Category
        {
            public string name { get; set; }
            public string localized_name { get; set; }
        }

        public class Group
        {
            public string name { get; set; }
            public List<Category> categories { get; set; }
            public string localized_name { get; set; }
        }

        public class LocationResponse
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
            public List<Group> groups { get; set; }
            public Category category { get; set; }
            public List<Subcategory> subcategory { get; set; }
        }

        public class PlaceInfo
        {
            public string location_id { get; set; }
            public string name { get; set; }
            public string distance { get; set; }
            public LocationResponse locationResponse { get; set; }
            public AddressObj address_obj { get; set; }
        }

        public class PlacesResponse
        {
            public List<PlaceInfo> data { get; set; }
        }
    }
}
