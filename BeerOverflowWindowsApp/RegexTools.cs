using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebApi.Exceptions;

namespace WebApi
{
    static class RegexTools
    {
        private static string _invalidDataMessage = "Invalid data entered";
        private static string _latitudeRegex = @"^(-?[1-8]?\d(\.\d{1,})?|-?90(\.0{1,})?)$";
        private static string _longitudeRegex = @"^(-?(1[0-7]|[1-9])?\d(\.\d{1,})?|-?180(\.0{1,})?)$";
        private static string _radiusRegex = @"^[0-9]{1,3}$";

        public static bool LocationDataTextIsCorrect(string latitude, string longitude, string radius)
        {
            var latitudeIsCorrect = Regex.IsMatch(latitude, _latitudeRegex);
            var longitudeIsCorrect = Regex.IsMatch(longitude, _longitudeRegex);
            var radiusIsCorrect = Regex.IsMatch(radius, _radiusRegex);
            var returnValue = latitudeIsCorrect && longitudeIsCorrect && radiusIsCorrect;

            if (returnValue) return true;
            var badArguments = new List<string>();

            if (!latitudeIsCorrect) { badArguments.Add("latitude"); }
            if (!longitudeIsCorrect) { badArguments.Add("longitude"); }
            if (!radiusIsCorrect) { badArguments.Add("radius"); }
            var invalidArguments = string.Join(",", badArguments);
            throw new ArgumentsForProvidersException(_invalidDataMessage, invalidArguments);
        }

        public static bool LatitudeTextIsCorrect(string latitude)
        {
            var latitudeIsCorrect = Regex.IsMatch(latitude, _latitudeRegex);
            if (latitudeIsCorrect) return true;
            throw new ArgumentsForProvidersException(_invalidDataMessage, "latitude");
        }

        public static bool LongitudeTextIsCorrect(string longitude)
        {
            var longitudeIsCorrect = Regex.IsMatch(longitude, _longitudeRegex);
            if (longitudeIsCorrect) return true;
            throw new ArgumentsForProvidersException(_invalidDataMessage, "longitude");
        }

        public static bool RadiusTextIsCorrect(string radius)
        {
            var radiusIsCorrect = Regex.IsMatch(radius, _radiusRegex);
            if (radiusIsCorrect) return true;
            throw new ArgumentsForProvidersException(_invalidDataMessage, "radius");
        }
    }
}
