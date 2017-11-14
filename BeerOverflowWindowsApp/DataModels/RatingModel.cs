using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerOverflowWindowsApp.DataModels;

namespace BeerOverflowWindowsApp.DataModels
{
    public class RatingModel
    {
        public BarData barData { get; set; }
        public int rating { get; set; }
    }
}
