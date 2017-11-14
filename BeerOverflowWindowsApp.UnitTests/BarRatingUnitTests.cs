using System;
using BeerOverflowWindowsApp.BarComparers;
using NUnit.Framework;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestFixture]
    public class BarRatingUnitTests
    {
        [Test]
        public void BarRating_AddRating_ToNullBarList()
        {
            // Arrange
            var rating = new BarRating();
            // Act && Assert  
            Assert.Throws<ArgumentNullException>(() => rating.AddRating(null, 5));
        }

        [Test]
        public void BarRating_Sort_NormalEnum([Range(0, 3, 1)] CompareType compareType)
        {
            // Arrange
            var rating = new BarRating();

            // Act
            rating.Sort(compareType);
            // Assert, should be no exceptions. 
            //Not sure if should test if sorting was successful. Maybe it's covered in Comparers unit testing?
        }
    }
}
