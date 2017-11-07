using System.Configuration;
using BeerOverflowWindowsApp.DataModels;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace BeerOverflowWindowsApp
{
    static class BarFileWriter
    {
        private static string _filePath = ConfigurationManager.AppSettings["filePath"];

        public static async void SaveData(BarDataModel barData)
        {
            var barsInFile = BarFileReader.GetAllBarData();
            foreach (var bar in barData)
            {
                var barInListIndex = barsInFile.FindIndex(x => x.Title == bar.Title);
                if (barInListIndex != -1)
                {
                    var barOccurenceInFile = barsInFile[barInListIndex];
                    if (barOccurenceInFile.Ratings == null
                        || !barsInFile[barInListIndex].Ratings.SequenceEqual(bar.Ratings))
                    {
                        barsInFile[barInListIndex].Ratings = bar.Ratings;
                    }
                }
                else
                {
                    barsInFile.Add(bar);
                }
            }
            var barsDataJson = JsonConvert.SerializeObject(barData);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:13623/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(barData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Api/BarData/SaveBars", content);
            }
            //File.WriteAllText(_filePath, barsDataJson);
        }
    }
}
