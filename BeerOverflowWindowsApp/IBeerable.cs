using WebApi.DataModels;
using System.Collections.Generic;

namespace WebApi
{
    interface IBeerable
    {
        List<BarData> GetBarsAround(string latitude, string longitude, string radius);
    }
}
