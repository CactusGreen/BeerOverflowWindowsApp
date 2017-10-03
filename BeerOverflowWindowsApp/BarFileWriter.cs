﻿using BeerOverflowWindowsApp.DataModels;
using System.IO;
using Newtonsoft.Json;

namespace BeerOverflowWindowsApp
{
    static class BarFileWriter
    {
        static string filePath = System.Configuration.ConfigurationManager.AppSettings["filePath"];

        static public void SaveData(BarDataModel barData)
        {
            var barsDataJson = JsonConvert.SerializeObject(barData);
            File.WriteAllText(filePath, barsDataJson);
        }
    }
}