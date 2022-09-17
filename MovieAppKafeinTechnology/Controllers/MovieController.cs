using Manager.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using MovieAppKafeinTechnology.Models;
using Newtonsoft.Json;

namespace MovieAppKafeinTechnology.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movie;
        private readonly IDistributedCache _cache;
        public MovieController(IMovieService movie, IDistributedCache cache)
        {
            _movie = movie;
            _cache = cache;
        }
        public IActionResult Index(int pageId, string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                pageId = pageId == 0 ? 1 : pageId;
                var list = _movie.SearchMovie(query, pageId);
                AddSearchCache(query);
                ViewBag.query = query;
                return View(list);
            }
            else
            {
                pageId = pageId == 0 ? 1 : pageId;
                var list = _movie.GetAllMovie(pageId);
                return View(list);
            }
        }
        public IActionResult MovieDetails(int id)
        {
            var detail = _movie.GetMovieDetail(id);
            var recommendations = _movie.GetRecommendations(id);
            MovieDetailVM movieDetail = new MovieDetailVM();
            movieDetail.MovieDetailDto = detail;
            movieDetail.MovieListDto = recommendations;
            return View(movieDetail);
        }
        public IActionResult Upcoming(int pageId)
        {
            pageId = pageId == 0 ? 1 : pageId;
            var list = _movie.GetUpcomingMovie("TR", pageId);
            return View(list);
        }
        private void AddSearchCache(string query)
        {
            var list = new List<SearchHistoryVM>();
            var getList = _cache.GetString("Search");
            if (getList != null)
            {
                list = JsonConvert.DeserializeObject<List<SearchHistoryVM>>(getList).OrderBy(x=>x.SearchDate).ToList();
                if (list.Any(x => x.Name == query))
                {
                    list.FirstOrDefault(x => x.Name == query).SearchDate = DateTime.Now;
                }
                else
                {
                    if (list.Count == 5)
                    {
                        list.Remove(list.First());
                    }
                    list.Add(new SearchHistoryVM { Name = query, SearchDate = DateTime.Now });
                }
                string jsonSearch = JsonConvert.SerializeObject(list);
                _cache.SetString("Search", jsonSearch);
            }
            else
            {
                list.Add(new SearchHistoryVM { Name = query ,SearchDate=DateTime.Now});
                string jsonSearch = JsonConvert.SerializeObject(list);
                _cache.SetString("Search", jsonSearch);
            }

        }
    }
}
