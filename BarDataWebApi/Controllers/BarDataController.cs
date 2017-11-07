using BeerOverflowWindowsApp.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace BarDataWebApi.Controllers
{
    public class BarDataController : ApiController
    {
        private static readonly string _filePath = ConfigurationManager.AppSettings["filePath"];
        public List<BarData> GetAllBars()
        {
            List<BarData> result = null;
            var filePath = System.Web.HttpContext.Current.Request.MapPath("~\\" + _filePath);
            if (File.Exists(filePath))
            {
                var response = File.ReadAllText(filePath);
                result = JsonConvert.DeserializeObject<List<BarData>>(response);
            }
            return result;
        }
        [HttpPost]
        public IHttpActionResult SaveBars([FromBody]List<BarData> barsList)
        {
            var filePath = System.Web.HttpContext.Current.Request.MapPath("~\\" + _filePath);
            var barsListJson = JsonConvert.SerializeObject(barsList);
            File.WriteAllText(filePath, barsListJson);
            return Ok("Success");
        }
    }
}
