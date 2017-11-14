using System;
using WebApi.DataModels;
using System.Collections.Generic;

namespace WebApi.BarComparers
{
    public class ComparerByTitle : IComparer<BarData>
    {
        public int Compare(BarData barData1, BarData barData2)
        {
            if (barData1 != null && barData2 != null)
            {
                if (barData1.Title != null && barData2.Title != null)
                {
                    return string.Compare(barData1.Title, barData2.Title);
                }
                else
                {
                    var nullArguments = new List<string>();
                    if (barData1.Title == null)
                    {
                        nullArguments.Add(nameof(barData1.Title));
                    }
                    if (barData2.Title == null)
                    {
                        nullArguments.Add(nameof(barData2.Title));
                    }
                    throw new ArgumentNullException(string.Join(",", nullArguments));
                }
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
