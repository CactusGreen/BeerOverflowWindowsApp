using System;
using WebApi.DataModels;
using System.Collections.Generic;

namespace WebApi.BarComparers
{
    class ComparerByDistance : IComparer<BarData>
    {
        public int Compare(BarData barData1, BarData barData2)
        {
            if (barData1 != null && barData2 != null)
            {
                int result;
                var distance1 = barData1.DistanceToCurrentLocation;
                var distance2 = barData2.DistanceToCurrentLocation;
                if (distance1 == distance2)
                {
                    result = 0;
                }
                else if (distance1 > distance2)
                {
                    result = 1;
                }
                else
                {
                    result = -1;
                }
                return result;
            }
            else
            {
                var nullArguments = new List<string>();
                if (barData1 == null)
                {
                    nullArguments.Add(nameof(barData1)); 
                }
                if (barData2 == null)
                {
                    nullArguments.Add(nameof(barData2));
                }
                throw new ArgumentNullException(string.Join(",", nullArguments));
            }
        }
    }
}
