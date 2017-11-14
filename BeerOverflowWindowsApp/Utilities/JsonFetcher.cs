using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflowWindowsApp.Utilities
{
    public class JsonFetcher
    {
        public async Task<string> GetJsonStreamAsync(string uri)
        {
            string result = null;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        public string GetJsonStream(string uri)
        {
            var webClient = new WebClient { Encoding = Encoding.UTF8 };
            var response = webClient.DownloadString(uri);
            return response;
        }
    }
}