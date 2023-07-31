using GiphyService.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace GiphyService
{
    public class GifService: IGiphyService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly string _apiKey;
      

        public GifService(HttpClient httpClient, IMemoryCache cache, string apiKey)
        {
            _httpClient = httpClient;
            _cache = cache;         
            _apiKey = apiKey;
        }

        public async Task<List<string>> GetTrendingGifsAsync()
        {   // check if exists in cache - if exists, get response from cache
            var cacheKey = "trending";
            if (_cache.TryGetValue(cacheKey, out List<string> cachedGifs)) 
            {
                return cachedGifs;
            }

            // otherwise - get response from API
            var response = await _httpClient.GetAsync($"https://api.giphy.com/v1/gifs/trending?api_key={_apiKey}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var gifs = ParseGifUrls(json);

            _cache.Set(cacheKey, gifs, TimeSpan.FromHours(1));
            return gifs;
        }

        public async Task<List<string>> SearchGifsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                throw new ArgumentException("Search term cannot be empty", nameof(searchTerm));
            // check if exists in cache - if exists, get response from cache
            var cacheKey = $"search:{searchTerm}";
            if (_cache.TryGetValue(cacheKey, out List<string> cachedGifs))
            {
                return cachedGifs;
            }
            // otherwise - get response from API
            var response = await _httpClient.GetAsync($"https://api.giphy.com/v1/gifs/search?api_key={_apiKey}&q={searchTerm}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var gifs = ParseGifUrls(json);

            _cache.Set(cacheKey, gifs, TimeSpan.FromHours(1));
            return gifs;
        }

        private List<string> ParseGifUrls(string json)
        {
            var jObject = JObject.Parse(json);
            var urls = new List<string>();
            foreach (var gif in jObject["data"])
            {
                urls.Add(gif["images"]["original"]["url"].ToString());
            }
            return urls;
        }
    }
}
