using Newtonsoft.Json;
using System.Xml.Linq;

namespace GiphyApp
{
    public class DataService
    {
        private readonly IHttpClientFactory _clientFactory;

        public DataService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<object>> FetchDataAsync()
        {
            // Replace with the logic for fetching data from your external source
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7209/Giphy/trending");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<object>>(responseContent);
                return data;
            }
            else
            {
                return new List<object>();
            }
        }
    }
}
