using System;
using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflowWindowsApp.BarComparers
{
    class ComparerByRating : IComparer<BarData>
    {
        public int Compare(BarData barData1, BarData barData2)
        {
            if (barData1 == null) throw new ArgumentNullException(nameof(barData1));
            if (barData2 == null) throw new ArgumentNullException(nameof(barData2));
            int result = barData1.AvgRating == barData2.AvgRating
                    ? 0 
                    : barData1.AvgRating > barData2.AvgRating
                        ? 1 
                        : -1 ;
            if (result == 0)
            {
                result = string.Compare(barData1.Title, barData2.Title);
            }
            return result;
        }
    }
}
