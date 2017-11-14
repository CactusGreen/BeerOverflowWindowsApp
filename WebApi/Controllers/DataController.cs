using BeerOverflowWindowsApp.Database;
using BeerOverflowWindowsApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    public class DataController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public BarDataModel GetAllBarData([FromBody]BarData localBars)
        {
            var dbManager = new DatabaseManager();
            BarDataModel result = null;// dbManager.GetAllBarData(localBars);            
            return result;
        }

        [HttpPost]
        public IHttpActionResult SaveBarRating([FromBody] BarData barToRate, int rating)
        {
            
            return Ok("Success");
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public List<int> GetBarRatings([FromBody]BarData localBars)
        {
            var dbManager = new DatabaseManager();
            var result = dbManager.GetBarRatings(localBars);
            return result;
        }
    }
}
