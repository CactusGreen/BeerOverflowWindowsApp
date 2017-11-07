using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeerOverflowWindowsApp
{
    interface IBeerable
    {
        List<BarData> GetBarsAround(string latitude, string longitude, string radius);
        Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius);
    }
}
