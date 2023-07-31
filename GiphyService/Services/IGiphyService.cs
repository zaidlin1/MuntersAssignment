using Microsoft.Extensions.Caching.Memory;

namespace GiphyService.Services
{
    public interface IGiphyService
    {
        Task<List<string>> GetTrendingGifsAsync();
        Task<List<string>> SearchGifsAsync(string searchTerm);
    }


}
