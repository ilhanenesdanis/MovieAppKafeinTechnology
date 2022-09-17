using Manager.Dto;

namespace MovieAppKafeinTechnology.Models
{
    public class MovieDetailVM
    {
        public ResultDTO<MovieDetailDto> MovieDetailDto { get; set; }
        public ResultDTO<MovieListDto> MovieListDto { get; set; }
    }
}
