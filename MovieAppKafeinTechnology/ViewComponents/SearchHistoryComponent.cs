using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MovieAppKafeinTechnology.Models;
using Newtonsoft.Json;

namespace MovieAppKafeinTechnology.ViewComponents
{
    public class SearchHistoryComponent:ViewComponent
    {
        private readonly IDistributedCache _cache;

        public SearchHistoryComponent(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IViewComponentResult Invoke()
        {
            List<SearchHistoryVM> search = new List<SearchHistoryVM>();
            var getList = _cache.GetString("Search");
            if (getList != null)
            {
                search = JsonConvert.DeserializeObject<List<SearchHistoryVM>>(getList);
                return View(search);
            }
            return View(search);
            
        }
    }
}
