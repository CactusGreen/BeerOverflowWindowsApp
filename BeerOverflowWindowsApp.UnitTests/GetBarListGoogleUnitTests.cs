using System;
using BeerOverflowWindowsApp.BarProviders;
using BeerOverflowWindowsApp.Exceptions;
using NUnit.Framework;

namespace BeerOverflowWindowsApp.UnitTests
{
    public class GetBarListGoogleUnitTests
    {
        /*[Test]
        [TestCase("0.0", "0.0", "0")]
        [TestCase("-90.0", "-180.0", "50")]
        [TestCase("90.0", "180.0", "50")]
        [TestCase("-90.0", "180.0", "50")]
        [TestCase("90.0", "-180.0", "50")]
        [TestCase("54.67933", "25.283960", "999")]
        public void GetBarsGoogle_GetBarsAround_ValidData(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListGoogle();
            // Act
            var barList = getBars.GetBarsAround(latitude, longitude, radius);
            // Assert
            Assert.NotNull(barList);
            foreach (var bar in barList)
            {
                Assert.NotNull(bar);
                Assert.NotNull(bar.Ratings);
                Assert.NotNull(bar.DistanceToCurrentLocation);
                Assert.GreaterOrEqual(bar.DistanceToCurrentLocation, 0.0);
                Assert.NotNull(bar.Latitude);
                Assert.NotNull(bar.Longitude);
                Assert.NotNull(bar.Title);
            }
        }*/

        [Test]
        [TestCase(null, "0.0", "0")]
        [TestCase("0.0", null, "0")]
        [TestCase("0.0", "0.0", null)]
        public void GetBarsGoogle_GetBarsAround_NullData(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListGoogle();
            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() => getBars.GetBarsAround(latitude, longitude, radius));
        }

        [Test]
        public void GetBarsGoogle_GetBarsAround_InvalidLatidute([Values("T", "", "-91.0", "91.0")] string latitude, 
                                                                [Values("1.0")] string longitude, 
                                                                [Values("1")] string radius)
        {
            // Arrange
            var getBars = new GetBarListGoogle();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "latitude");
        }

        [Test]
        public void GetBarsGoogle_GetBarsAround_InvalidLongitude([Values("1.0")] string latitude, 
                                                                 [Values("T", "", "-181.0", "181.0")]string longitude, 
                                                                 [Values("1")] string radius)
        {
            // Arrange
            var getBars = new GetBarListGoogle();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "longitude");
        }

        [Test]
        public void GetBarsGoogle_GetBarsAround_InvalidRadius([Values("1.0")] string latitude, 
                                                              [Values("1.0")] string longitude, 
                                                              [Values("T", "-", "-1", "", "999999")] string radius)
        {
            // Arrange
            var getBars = new GetBarListGoogle();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "radius");
        }
    }
}
