using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;

namespace BeerOverflowWindowsApp
{
    interface IBeerable
    {
        List<BarData> GetBarsAround(string latitude, string longitude, string radius);
    }
}
