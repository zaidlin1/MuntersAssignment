using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace GiphyApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DataService _dataService;

        public IndexModel(DataService dataService)
        {
            _dataService = dataService;
        }

        public IList<object> Data { get; private set; }

        public async Task OnGetAsync()
        {
            Data = await _dataService.FetchDataAsync();
        }
    }
}