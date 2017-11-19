﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BeerOverflowWindowsApp.DataModels;

namespace BeerOverflowWindowsApp.BarProviders
{
    interface IBeerable
    {
        string ProviderName { get; }
        List<BarData> GetBarsAround(string latitude, string longitude, string radius);
        Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius);
    }
}
