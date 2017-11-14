using BeerOverflowWindowsApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApi.Database;

namespace WebApi.Controllers
{
    public class DataController : ApiController
    {

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult GetAllBarData([FromBody]List<BarData> localBars)
        {
            var dbManager = new DatabaseManager();
            List<BarData> result = dbManager.GetAllBarData(localBars);            
            return Ok(result);
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult SaveBarRating([FromBody] RatingModel barObject)
        {
            var dbManager = new DatabaseManager();
            dbManager.SaveBarRating(barObject.barData, barObject.rating);
            return Ok("Success");
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult GetBarRatings([FromBody]BarData localBars)
        {
            var dbManager = new DatabaseManager();
            List<int> result = dbManager.GetBarRatings(localBars);
            return Ok(result);
        }
    }
}
