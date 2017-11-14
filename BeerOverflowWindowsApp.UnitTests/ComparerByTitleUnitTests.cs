using System;
using WebApi.BarComparers;
using WebApi.DataModels;
using NUnit.Framework;

namespace WebApi.UnitTests
{
    [TestFixture]
    public class ComparerByTitleUnitTests
    {
        [Test]
        [TestCase("aaaa", "b")]
        [TestCase("aba", "abc")]
        [TestCase("ą", "č")]
        [TestCase("1", "2")]
        [TestCase("", "1")]
        public void ComparerByTitle_Compare_FirstIsSmaller(string title1, string title2)
        {
            // Arrange
            var bar1 = new BarData { Title = title1 };
            var bar2 = new BarData { Title = title2 };
            var expectedValue = -1;
            // Act
            var returnValue = new ComparerByTitle().Compare(bar1, bar2);
            // Assert
            Assert.AreEqual(expectedValue, returnValue);
        }

        [Test]
        [TestCase("b", "aaaa")]
        [TestCase("abc", "aba")]
        [TestCase("2", "1")]
        [TestCase("1", "")]
        public void ComparerByTitle_Compare_SecondIsBigger(string title1, string title2)
        {
            // Arrange
            var bar1 = new BarData { Title = title1 };
            var bar2 = new BarData { Title = title2 };
            var expectedValue = 1;
            // Act
            var returnValue = new ComparerByTitle().Compare(bar1, bar2);
            // Assert
            Assert.AreEqual(expectedValue, returnValue);
        }

        [Test]
        [TestCase("aaaa")]
        [TestCase("aba")]
        [TestCase("ą")]
        [TestCase("1")]
        [TestCase("")]
        public void ComparerByTitle_Compare_Equal(string title)
        {
            // Arrange
            var bar1 = new BarData { Title = title };
            var bar2 = new BarData { Title = title };
            var expectedValue = 0;
            // Act
            var returnValue = new ComparerByTitle().Compare(bar1, bar2);
            // Assert
            Assert.AreEqual(expectedValue, returnValue);
        }

        [Test]
        public void ComparerByTitle_Compare_BarsAreNull()
        {
            // Arrange
            var comparer = new ComparerByTitle();
            var newBar = new BarData { Title = "" };   // Investigate mocking
            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, newBar));
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(newBar, null));
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, null));
        }

        [Test]
        [TestCase("", null)]
        [TestCase(null, "")]
        [TestCase(null, null)]
        public void ComparerByTitle_Compare_TitlesAreNull(string title1, string title2)
        {
            // Arrange
            var comparer = new ComparerByTitle();
            // Investigate mocking
            var bar1 = new BarData { Title = title1 };
            var bar2 = new BarData { Title = title2 };  
            // Act && Assert
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(bar1, bar2));
        }
    }
}
