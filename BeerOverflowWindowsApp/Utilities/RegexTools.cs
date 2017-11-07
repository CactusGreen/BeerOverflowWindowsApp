namespace BeerOverflowWindowsApp.Utilities
{
    class RegexTools
    {
        public static bool LongitudeTextIsCorrect(string longitude)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(longitude,
                @"^(-?(?:1[0-7]|[1-9])?\d(?:\.\d{1,})?|180(?:\.0{1,})?)$");
        }

        public static bool LatitudeTextIsCorrect(string latitude)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(latitude,
                @"^(-?[1-8]?\d(?:\.\d{1,})?|90(?:\.0{1,})?)$");
        }

        public static bool RadiusTextIsCorrect(string radius)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(radius,
                @"^[0-9]{1,3}$");
        }
    }
}
