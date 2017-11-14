namespace BeerOverflowWindowsApp.UnitTests.TestData
{
    public class BarProviderTestData
    {
        public static object[] ValidDataCases =
        {
            new object[] { "0.0", "0.0", "0" },
            new object[] { "-90.0", "-180.0", "50" },
            new object[] { "90.0", "180.0", "50" },
            new object[] { "-90.0", "180.0", "50" },
            new object[] { "90.0", "-180.0", "50" },
            new object[] { "54.67933", "25.283960", "999" }
        };

        public static readonly object[] NullDataCases =
        {
            new object[] { null, "0.0", "0" },
            new object[] { "0.0", null, "0" },
            new object[] { "0.0", "0.0", null }
        };

        public static readonly object[] InvalidLatitudeCases =
        {
            new object[] { "T", "1.0", "1" },
            new object[] { "", "1.0", "1" },
            new object[] { "-91.0", "1.0", "1" },
            new object[] { "91.0", "1.0", "1" }
        };

        public static readonly object[] InvalidLongitudeCases =
        {
            new object[] { "1.0", "T", "1" },
            new object[] { "1.0", "", "1" },
            new object[] { "1.0", "-181.0", "1" },
            new object[] { "1.0", "181.0", "1" }
        };

        public static readonly object[] InvalidRadiusCases =
        {
            new object[] { "1.0", "1", "T" },
            new object[] { "1.0", "1", "-" },
            new object[] { "1.0", "1", "" },
            new object[] { "1.0", "1", "-1" },
            new object[] { "1.0", "1", "9999999" }
        };
    }
}