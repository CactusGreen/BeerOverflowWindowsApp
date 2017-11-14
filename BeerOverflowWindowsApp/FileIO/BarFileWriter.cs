using System.Configuration;
using WebApi.DataModels;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WebApi.FileIO
{
    static class BarFileWriter
    {
        private static string _filePath = ConfigurationManager.AppSettings["filePath"];

        public static void SaveData(BarDataModel barData)
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
            File.WriteAllText(_filePath, barsDataJson);
        }
    }
}
