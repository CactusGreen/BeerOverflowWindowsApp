using System;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using NUnit.Framework;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestFixture]
    public class ComparerByDistanceUnitTests
    {
        private const int timesToRepeatTests = 1;

        [Test]
        public void ComparerByDistance_Compare_FirstIsCloser([Random(0.0, 50.0, timesToRepeatTests)]double distanceToCurrentLocation1, [Random(50.1, 100.0, timesToRepeatTests)] double distanceToCurrentLocation2)
        {
            // Arrange
            var bar1 = new BarData { DistanceToCurrentLocation = distanceToCurrentLocation1 };
            var bar2 = new BarData { DistanceToCurrentLocation = distanceToCurrentLocation2 };
            var expectedValue = -1;
            // Act
            var returnValue = new ComparerByDistance().Compare(bar1, bar2);
            // Assert
            Assert.AreEqual(expectedValue, returnValue);
        }

        [Test]
        public void ComparerByDistance_Compare_SecondIsCloser([Random(50.1, 100.0, timesToRepeatTests)] double distanceToCurrentLocation1, [Random(0.0, 50.0, timesToRepeatTests)] double distanceToCurrentLocation2)
        {
            // Arrange
            var bar1 = new BarData { DistanceToCurrentLocation = distanceToCurrentLocation1 };
            var bar2 = new BarData { DistanceToCurrentLocation = distanceToCurrentLocation2 };
            var expectedValue = 1;
            // Act
            var returnValue = new ComparerByDistance().Compare(bar1, bar2);
            // Assert
            Assert.AreEqual(expectedValue, returnValue);
        }

        [Test]
        public void ComparerByDistance_Compare_DistanceEqual([Random(0.0, 200.0, timesToRepeatTests)] double distanceToCurrentLocation)
        {
            // Arrange
            var bar1 = new BarData { DistanceToCurrentLocation = distanceToCurrentLocation };
            var bar2 = new BarData { DistanceToCurrentLocation = distanceToCurrentLocation };
            var expectedValue = 0;
            // Act
            var returnValue = new ComparerByDistance().Compare(bar1, bar2);
            // Assert
            Assert.AreEqual(expectedValue, returnValue);
        }

        [Test]
        public void ComparerByDistance_Compare_BarsAreNull()
        {
            // Arrange
            var comparer = new ComparerByDistance();
            var newBar = new BarData {DistanceToCurrentLocation = 0};   // Investigate mocking
            // Act && Assert  
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, newBar));
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(newBar, null));
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, null));
        }
    }
}
