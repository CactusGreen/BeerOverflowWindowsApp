using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

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
                var responseResult = response.Result.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<int>>(responseResult.Result);
                return result;
            }
        }
        public static List<BarData> GetAllBarData(BarDataModel localBars)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1726/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(localBars);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("Api/Data/GetAllBarData", content);
                var responseResult = response.Result.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BarDataModel>(responseResult.Result);
                return result;
            }
        }
        public static void SaveBarRating(BarData barToRate, int rating)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1726/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(new RatingModel{barData = barToRate, rating = rating});
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("Api/Data/SaveBarRating", content).Result;
            }
        }
    }
}
