using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BeerOverflowWindowsApp.Utilities
{
    public static class FetcherAndDeserializer
    {
        public static T FetchAndDeserialize<T>(string link, IHttpFetcher fetcher)
        {
            var jsonStream = fetcher.GetHttpStream(link);
            var deserialized = JsonConvert.DeserializeObject<T>(jsonStream);
            return deserialized;
        }

        public static async Task<T> FetchAndDeserializeAsync<T>(string link, IHttpFetcher fetcher)
        {
            var jsonStream = await fetcher.GetHttpStreamAsync(link);
            var deserialized = JsonConvert.DeserializeObject<T>(jsonStream);
            return deserialized;
        }
    }
}