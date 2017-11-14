using System;
using WebApi.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.BarComparers
{
    class ComparerByRating : IComparer<BarData>
    {
        public int Compare(BarData barData1, BarData barData2)
        {
            if (barData1 == null) throw new ArgumentNullException(nameof(barData1));
            if (barData2 == null) throw new ArgumentNullException(nameof(barData2));

            int result;
            if (barData1.Ratings == null && barData2.Ratings == null)
            {
                result = 0;
            }
            // 0 < x
            else if (barData1.Ratings == null && barData2.Ratings != null)
            {
                result = -1;
            }
            // x > 0
            else if (barData1.Ratings != null && barData2.Ratings == null)
            {
                result = 1;
            }
            else
            {
                var bar1RatingAverage = barData1.Ratings.DefaultIfEmpty().Average();
                var bar2RatingAverage = barData2.Ratings.DefaultIfEmpty().Average();

                result = bar1RatingAverage == bar2RatingAverage
                    ? 0 
                    : bar1RatingAverage > bar2RatingAverage
                        ? 1 
                        : -1 ;
            }
            // compares by titles, if ratings are equal. 
            // Is this necesary?
            if (result == 0)
            {
                result = string.Compare(barData1.Title, barData2.Title);
            }
            return result;
        }
    }
}
