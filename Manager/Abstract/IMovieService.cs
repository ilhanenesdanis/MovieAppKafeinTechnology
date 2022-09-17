using Manager.Dto;

namespace Manager.Abstract
{
    public interface IMovieService
    {
        ResultDTO<MovieListDto> GetAllMovie(int pageId);
        ResultDTO<MovieDetailDto> GetMovieDetail(int movieId);
        ResultDTO<MovieListDto> GetRecommendations(int movieId);
        ResultDTO<MovieListDto> GetUpcomingMovie(string region,int pageId);
        ResultDTO<MovieListDto> SearchMovie(string query, int pageId);
    }
}
