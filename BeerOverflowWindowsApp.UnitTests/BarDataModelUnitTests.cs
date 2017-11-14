using System;
using System.Globalization;
using WebApi.DataModels;
using WebApi.Exceptions;
using NUnit.Framework;

namespace WebApi.UnitTests
{
    [TestFixture]
    public class BarDataModelUnitTests
    {
        [Test]
        public void BarDataModel_RemoveBarsOutsideRadius_NullRadius()
        {
            // Arrange
            var barData = new BarDataModel();
            // Act
            // Assert
            var ex = Assert.Throws<ArgumentNullException>(() => barData.RemoveBarsOutsideRadius(null));
        }

        [Test]
        [TestCase("TEST", "test")]
        [TestCase("testing", "TEST")]
        [TestCase("TEST", "test name")]
        public void BarDataModel_RemoveDuplicates_RemoveBarContainingOthersTitle(string name1, string name2)
        {
            // Arrange
            var barData = new BarDataModel();
            barData.Add(new BarData { Title = name1 });
            barData.Add(new BarData { Title = name2 });
            // Act
            barData.RemoveDuplicates();
            // Assert
            Assert.AreEqual(1, barData.Count);
        }

        [Test]
        [TestCase("TE5Ts", 0.0, 0.0, "tests", 0.0, 0.0)]      // same name as in DoNotRemoveSimilarBarsFar, with different coordinates
        [TestCase("test1ng", 0.0, 0.0, "1TESTing", 0.0, 0.0)] // same name as in DoNotRemoveSimilarBarsFar, with different coordinates
        public void BarDataModel_RemoveDuplicates_RemoveSimilarBarsNear(string name1, double lat1, double lon1,
                                                                        string name2, double lat2, double lon2)
        {
            // Arrange
            var barData = new BarDataModel();
            barData.Add(new BarData { Title = name1, Latitude = lat1, Longitude = lon1 });
            barData.Add(new BarData { Title = name2, Latitude = lat2, Longitude = lon2 });
            // Act
            barData.RemoveDuplicates();
            // Assert
            Assert.AreEqual(1, barData.Count);
        }

        [Test]
        [TestCase("TE5Ts", 0.0, 0.0, "tests", 10.0, 10.0)]      // same name as in RemoveSimilarBarsNear, with different coordinates
        [TestCase("test1ng", 0.0, 0.0, "1TESTing", 10.0, 10.0)] // same name as in RemoveSimilarBarsNear, with different coordinates
        public void BarDataModel_RemoveDuplicates_DoNotRemoveSimilarBarsFar(string name1, double lat1, double lon1,
            string name2, double lat2, double lon2)
        {
            // Arrange
            var barData = new BarDataModel();
            barData.Add(new BarData { Title = name1, Latitude = lat1, Longitude = lon1 });
            barData.Add(new BarData { Title = name2, Latitude = lat2, Longitude = lon2 });
            // Act
            barData.RemoveDuplicates();
            // Assert
            Assert.AreEqual(2, barData.Count);
        }

        [Test]
        public void BarDataModel_RemoveBarsOutsideRadius_InvalidRadius([Values("T", "-", "-1", "", "999999.0")] string radius)
        {
            // Arrange
            var barData = new BarDataModel();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => barData.RemoveBarsOutsideRadius(radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "radius");
        }

        private const int _maximumRadius = 999;
        private const int _numberOfBarsToRemove = 10;
        private const int _numberOfBarsToStay = 5;
        [Test]
        public void BarDataModel_RemoveBarsOutsideRadius_RemoveBarsOutside([Random(_numberOfBarsToStay, _maximumRadius, 2)] int radius)
        {
            // Arrange
            var barData = new BarDataModel();

            for (var i = 0; i < _numberOfBarsToStay; i++)
            {
                barData.Add(new BarData { DistanceToCurrentLocation = radius - i });
            }

            for (var i = 1; i <= _numberOfBarsToRemove; i++)
            {
                barData.Add(new BarData { DistanceToCurrentLocation = radius + i });
            }

            // Act
            barData.RemoveBarsOutsideRadius(radius.ToString("0", CultureInfo.InvariantCulture));
            // Assert
            Assert.AreEqual(_numberOfBarsToStay, barData.Count);
        }
    }
}
