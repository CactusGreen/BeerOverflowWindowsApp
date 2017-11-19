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
        public static User defaultUser = new User { Username = "Jonas" };

        public static BarData GetBarRatings(BarData bar)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1726/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(new UserAndBarModel { Bar = bar, User = defaultUser});
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("Api/Data/GetBarRatings", content);
                var responseResult = response.Result.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BarData>(responseResult.Result);
                bar.AvgRating = result.AvgRating;
                bar.UserRating = result.UserRating;
                return bar;
            }
        }

        public static List<BarData> GetAllBarData(BarDataModel localBars)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1726/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(new UserAndBarsModel { Bars = localBars, User = defaultUser});
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
                var json = JsonConvert.SerializeObject(new RatingModel{Bar = barToRate, Rating = rating, User = defaultUser});
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("Api/Data/SaveBarRating", content).Result;
            }
        }
    }
}
