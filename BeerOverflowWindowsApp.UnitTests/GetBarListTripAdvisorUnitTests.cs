﻿using System;
using BeerOverflowWindowsApp.BarProviders;
using BeerOverflowWindowsApp.Exceptions;
using BeerOverflowWindowsApp.UnitTests.TestData;
using NUnit.Framework;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestFixture]
    public class GetBarListTripAdvisorUnitTests : BarProviderTestData
    {
        /*[TestCaseSource("ValidDataCases")]
        public void GetBarsTripAdvisor_GetBarsAround_ValidData(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListTripAdvisor();
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

        [TestCaseSource("NullDataCases")]
        public void GetBarsTripAdvisor_GetBarsAround_NullData(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListTripAdvisor();
            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() => getBars.GetBarsAround(latitude, longitude, radius));
        }

        [TestCaseSource("InvalidLatitudeCases")]
        public void GetBarsTripAdvisor_GetBarsAround_InvalidLatidute(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListTripAdvisor();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "latitude");
        }

        [TestCaseSource("InvalidLongitudeCases")]
        public void GetBarsTripAdvisor_GetBarsAround_InvalidLongitude(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListTripAdvisor();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "longitude");
        }

        [TestCaseSource("InvalidRadiusCases")]
        public void GetBarsTripAdvisor_GetBarsAround_InvalidRadius(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListTripAdvisor();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "radius");
        }

        [TestCaseSource("NullDataCases")]
        public void GetBarsTripAdvisor_GetBarsAroundAsync_NullData(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListTripAdvisor();
            // Act && Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>
                            (async () => await getBars.GetBarsAroundAsync(latitude, longitude, radius));
        }

        [TestCaseSource("InvalidLatitudeCases")]
        public void GetBarsTripAdvisor_GetBarsAroundAsync_InvalidLatidute(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListTripAdvisor();
            // Act
            var ex = Assert.ThrowsAsync<ArgumentsForProvidersException>
                            (async () => await getBars.GetBarsAroundAsync(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "latitude");
        }

        [TestCaseSource("InvalidLongitudeCases")]
        public void GetBarsTripAdvisor_GetBarsAroundAsync_InvalidLongitude(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListTripAdvisor();
            // Act
            var ex = Assert.ThrowsAsync<ArgumentsForProvidersException>
                            (async () => await getBars.GetBarsAroundAsync(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "longitude");
        }

        [TestCaseSource("InvalidRadiusCases")]
        public void GetBarsTripAdvisor_GetBarsAroundAsync_InvalidRadius(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListTripAdvisor();
            // Act
            var ex = Assert.ThrowsAsync<ArgumentsForProvidersException>
                            (async () => await getBars.GetBarsAroundAsync(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "radius");
        }
    }
}
