﻿using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflowWindowsApp
{
    static class WebApiAccess
    {
        public static List<int> GetBarRatings(BarData bar)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1726/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(bar);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("Api/Data/GetBarRatings", content);
                var aa = response.Result;
                return null;
            }
        }
        public static BarDataModel GetAllBarData(BarDataModel localBars)
        {
            return null;
        }
        public static void SaveBarRating(BarData barToRate, int rating)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1726/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(barToRate);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("Api/Data/GetBarRatings", content);
                var aa = response.Result;
            }
        }
    }
}