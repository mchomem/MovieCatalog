namespace MovieCatalog.WebApp.Dtos
{
    [Serializable]
    public class DashboardDto
    {
        public int TotalMovies { get; set; }
        public List<MovieRatingDto>? MoviesRating { get; set; }
        public MovieDto? FirstMovieAdded { get; set; }
        public MovieDto? LastMovieAdded { get; set; }
    }
}
