using GiphyService.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiphyService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GiphyController : ControllerBase
    {
        private readonly IGiphyService _giphyService;

        public GiphyController(IGiphyService giphyService)
        {
            _giphyService = giphyService;
        }
        /// <summary>
        /// Get trending gifs of the day 
        /// </summary>
        /// <returns></returns>
        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingGifsAsync()
        {
            try
            {
                var result = await _giphyService.GetTrendingGifsAsync();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Get gifs by search term
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet("search/{searchTerm}")]
        public async Task<IActionResult> SearchGifsAsync(string searchTerm)
        {
            try
            {
                var result = await _giphyService.SearchGifsAsync(searchTerm);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
