using Manager.Abstract;
using Manager.Dto;
using Newtonsoft.Json;
using System.Net;

namespace Manager.Concrete
{
    public class MovieService : IMovieService
    {
        public ResultDTO<MovieListDto> GetAllMovie(int pageId)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync($"https://api.themoviedb.org/3/movie/popular?api_key=428e43276725ebe1c2451bced0496535&language=tr-TR&page={pageId}").Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var list = JsonConvert.DeserializeObject<ResultDTO<MovieListDto>>(response.Content.ReadAsStringAsync().Result);
                list.IsStatus = true;
                return list;
            }
            return new ResultDTO<MovieListDto>() { IsStatus = false };
        }

        public ResultDTO<MovieDetailDto> GetMovieDetail(int movieId)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key=428e43276725ebe1c2451bced0496535&language=tr-TR").Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var detail = JsonConvert.DeserializeObject<MovieDetailDto>(response.Content.ReadAsStringAsync().Result);
                return new ResultDTO<MovieDetailDto>() { IsStatus = true, Data = detail };
            }
            return new ResultDTO<MovieDetailDto>() { IsStatus = false };
        }

        public ResultDTO<MovieListDto> GetRecommendations(int movieId)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}/recommendations?api_key=428e43276725ebe1c2451bced0496535&language=tr-TR").Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var list = JsonConvert.DeserializeObject<ResultDTO<MovieListDto>>(response.Content.ReadAsStringAsync().Result);
                list.IsStatus = true;
                return list;
            }
            return new ResultDTO<MovieListDto> { IsStatus = false };
        }

        public ResultDTO<MovieListDto> GetUpcomingMovie(string region, int pageId)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync($"https://api.themoviedb.org/3/movie/upcoming?api_key=428e43276725ebe1c2451bced0496535&language=tr-TR&page={pageId}&region={region}").Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var list = JsonConvert.DeserializeObject<ResultDTO<MovieListDto>>(response.Content.ReadAsStringAsync().Result);
                list.IsStatus = true;
                return list;
            }
            return new ResultDTO<MovieListDto> { IsStatus = false };
        }

        public ResultDTO<MovieListDto> SearchMovie(string query, int pageId)
        {
            List<MovieListDto> listDtos = new List<MovieListDto>();
            HttpClient client = new HttpClient();
            var response = client.GetAsync($"https://api.themoviedb.org/3/search/keyword?api_key=428e43276725ebe1c2451bced0496535&page={pageId}&query={query}").Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var list = JsonConvert.DeserializeObject<ResultDTO<KeywordsDto>>(response.Content.ReadAsStringAsync().Result);
                if (list.total_results > 0 && list.results.Count > 0)
                {
                    foreach (var item in list.results)
                    {
                        var getMovie = client.GetAsync($"https://api.themoviedb.org/3/movie/{item.id}?api_key=428e43276725ebe1c2451bced0496535&language=tr-TR").Result;
                        if (getMovie.StatusCode == HttpStatusCode.OK)
                        {
                            var movie = JsonConvert.DeserializeObject<MovieListDto>(getMovie.Content.ReadAsStringAsync().Result);
                            listDtos.Add(movie);
                        }
                    }
                    ResultDTO<MovieListDto> result = new ResultDTO<MovieListDto>()
                    {
                        Page = pageId,
                        IsStatus = true,
                        results = listDtos,
                        total_pages = list.total_pages,
                        total_results = list.total_results
                    };
                    return result;
                }
            }
            return new ResultDTO<MovieListDto> { IsStatus = false };
        }
    }
}
