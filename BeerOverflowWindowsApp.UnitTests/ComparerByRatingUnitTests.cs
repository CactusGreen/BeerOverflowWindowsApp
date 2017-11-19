using System;
using System.Collections.Generic;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using NUnit.Framework;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestFixture]
    public class ComparerByRatingUnitTests
    {
        /*
               [Test]
               public void ComparerByRating_Compare_RatingsFirstBigger()
               {
                   // Arrange  
                   var barData1 = new BarData { Ratings = new List<int>{10,20}};
                   var barData2 = new BarData { Ratings = new List<int>{5,10}};
                   var expectedResult = 1;
                   var comparer = new ComparerByRating();

                   // Act  
                   var result = comparer.Compare(barData1, barData2);

                   // Assert  
                   Assert.AreEqual(expectedResult, result);
               }

               [Test]
               public void ComparerByRating_Compare_RatingsFirstNotNullSecondNull()
               {
                   // Arrange  
                   var barData1 = new BarData { Ratings = new List<int>() }; ;
                   var barData2 = new BarData { Ratings = null }; ;
                   var expectedResult = 1;
                   var comparer = new ComparerByRating();

                   // Act  
                   var result = comparer.Compare(barData1, barData2);

                   // Assert  
                   Assert.AreEqual(expectedResult, result);
               }

               [Test]
               public void ComparerByRating_Compare_RatingsFirstSmaller()
               {
                   // Arrange  
                   var barData1 = new BarData { Ratings = new List<int> { 5, 10 } };
                   var barData2 = new BarData { Ratings = new List<int> { 10, 20 } };
                   var expectedResult = -1;
                   var comparer = new ComparerByRating();

                   // Act  
                   var result = comparer.Compare(barData1, barData2);

                   // Assert  
                   Assert.AreEqual(expectedResult, result);
               }

               [Test]
               public void ComparerByRating_Compare_RatingsFirstNullSecondNotNull()
               {
                   // Arrange  
                   var barData1 = new BarData { Ratings = null };
                   var barData2 = new BarData { Ratings = new List<int>() }; ;
                   var expectedResult = -1;
                   var comparer = new ComparerByRating();

                   // Act  
                   var result = comparer.Compare(barData1, barData2);

                   // Assert  
                   Assert.AreEqual(expectedResult, result);
               }

               [Test]
               public void ComparerByRating_Compare_RatingsEqual()
               {
                   // Arrange  
                   var newRatings = new List<int> { 10, 20 };
                   var barData1 = new BarData { Ratings = newRatings };
                   var barData2 = new BarData { Ratings = newRatings };
                   var expectedResult = 0;
                   var comparer = new ComparerByRating();

                   // Act  
                   var result = comparer.Compare(barData1, barData2);

                   // Assert  
                   Assert.AreEqual(expectedResult, result);
               }

               [Test]
               public void ComparerByRating_Compare_RatingsBothNull()
               {
                   // Arrange  
                   var barData1 = new BarData { Ratings = null };
                   var barData2 = new BarData { Ratings = null }; ;
                   var expectedResult = 0;
                   var comparer = new ComparerByRating();

                   // Act  
                   var result = comparer.Compare(barData1, barData2);

                   // Assert  
                   Assert.AreEqual(expectedResult, result);
               }
       */
        [Test]
        public void ComparerByRating_Compare_BarsFirstNull()
        {
            // Arrange  
            var barData2 = new BarData();
            var comparer = new ComparerByRating();

            // Act && Assert  
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, barData2));
        }

        [Test]
        public void ComparerByRating_Compare_BarsSecondNull()
        {
            // Arrange  
            var barData1 = new BarData();
            var comparer = new ComparerByRating();

            // Act && Assert    
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(barData1, null));
        }

        [Test]
        public void ComparerByRating_Compare_BarsBothNull()
        {
            // Arrange  
            var comparer = new ComparerByRating();

            // Act && Assert  
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, null));
        }
    }
}
