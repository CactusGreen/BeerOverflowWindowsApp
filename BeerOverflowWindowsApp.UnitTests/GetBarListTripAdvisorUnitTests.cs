using System;
using System.Threading.Tasks;
using BeerOverflowWindowsApp.BarProviders;
using BeerOverflowWindowsApp.Exceptions;
using BeerOverflowWindowsApp.UnitTests.TestData;
using BeerOverflowWindowsApp.Utilities;
using Moq;
using NUnit.Framework;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestFixture]
    public class GetBarListTripAdvisorUnitTests
    {
        [TestCaseSource(typeof(BarProviderTestData), "ValidDataCases")]
        public void GetBarsFourSquare_GetBarsAround_ValidData(string latitude, string longitude, string radius)
        {
            // Arrange
            var validFetcher = new Mock<IHttpFetcher>();
            validFetcher.SetupSequence(p => p.GetHttpStream(It.IsAny<string>()))
                .Returns(BarProviderTestData.TripAdvisorValidDataResponse[0])
                .Returns(BarProviderTestData.TripAdvisorValidDataResponse[1])
                .Returns(BarProviderTestData.TripAdvisorValidDataResponse[2])
                .Returns(BarProviderTestData.TripAdvisorValidDataResponse[3]);
            var getBars = new GetBarListTripAdvisor(validFetcher.Object);
            // Act
            var barList = getBars.GetBarsAround(latitude, longitude, radius);
            // Assert
            Assert.NotNull(barList);
            foreach (var bar in barList)
            {
                Assert.NotNull(bar);

                Assert.NotNull(bar.Title);
            }
        }

        [TestCaseSource(typeof(BarProviderTestData), "ValidDataCases")]
        public void GetBarsFourSquare_GetBarsAroundAsync_ValidData(string latitude, string longitude, string radius)
        {
            // Arrange
            var validFetcher = new Mock<IHttpFetcher>();
            validFetcher.SetupSequence(p => p.GetHttpStreamAsync(It.IsAny<string>()))
                .Returns(Task.Run(() => BarProviderTestData.TripAdvisorValidDataResponse[0]))
                .Returns(Task.Run(() => BarProviderTestData.TripAdvisorValidDataResponse[1]))
                .Returns(Task.Run(() => BarProviderTestData.TripAdvisorValidDataResponse[2]))
                .Returns(Task.Run(() => BarProviderTestData.TripAdvisorValidDataResponse[3]));
            var getBars = new GetBarListTripAdvisor(validFetcher.Object);
            // Act
            var barList = getBars.GetBarsAroundAsync(latitude, longitude, radius).Result;
            // Assert
            Assert.NotNull(barList);
            foreach (var bar in barList)
            {
                Assert.NotNull(bar);

                Assert.NotNull(bar.Title);
            }
        }

        [TestCaseSource(typeof(BarProviderTestData), "NullDataCases")]
        public void GetBarsTripAdvisor_GetBarsAround_NullData(string latitude, string longitude, string radius)
        {
            var emptyFetcher = new Mock<IHttpFetcher>();
            emptyFetcher.Setup(p => p.GetHttpStream(It.IsAny<string>())).Throws(new Exception("This should not have been thrown"));
            // Arrange
            var getBars = new GetBarListTripAdvisor(emptyFetcher.Object);
            // Act && Assert
            var ex = Assert.Throws<ArgumentNullException>(() => getBars.GetBarsAround(latitude, longitude, radius));
        }

        [TestCaseSource(typeof(BarProviderTestData), "InvalidDataCases")]
        public void GetBarsTripAdvisor_GetBarsAround_InvalidData(string latitude, string longitude, string radius, string exceptionMessage)
        {
            var emptyFetcher = new Mock<IHttpFetcher>();
            emptyFetcher.Setup(p => p.GetHttpStream(It.IsAny<string>())).Throws(new Exception("This should not have been thrown"));
            // Arrange
            var getBars = new GetBarListTripAdvisor(emptyFetcher.Object);
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == exceptionMessage);
        }

        [TestCaseSource(typeof(BarProviderTestData), "NullDataCases")]
        public void GetBarsTripAdvisor_GetBarsAroundAsync_NullData(string latitude, string longitude, string radius)
        {
            var emptyFetcher = new Mock<IHttpFetcher>();
            emptyFetcher.Setup(p => p.GetHttpStream(It.IsAny<string>())).Throws(new Exception("This should not have been thrown"));
            // Arrange
            var getBars = new GetBarListTripAdvisor(emptyFetcher.Object);
            // Act && Assert
            var ex = Assert.ThrowsAsync<ArgumentNullException>
                            (async () => await getBars.GetBarsAroundAsync(latitude, longitude, radius));
        }

        [TestCaseSource(typeof(BarProviderTestData), "InvalidDataCases")]
        public void GetBarsTripAdvisor_GetBarsAroundAsync_InvalidData(string latitude, string longitude, string radius, string exceptionMessage)
        {
            var emptyFetcher = new Mock<IHttpFetcher>();
            emptyFetcher.Setup(p => p.GetHttpStream(It.IsAny<string>())).Throws(new Exception("This should not have been thrown"));
            // Arrange
            var getBars = new GetBarListTripAdvisor(emptyFetcher.Object);
            // Act
            var ex = Assert.ThrowsAsync<ArgumentsForProvidersException>
                            (async () => await getBars.GetBarsAroundAsync(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == exceptionMessage);
        }
    }
}
