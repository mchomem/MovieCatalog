namespace MovieCatalog.WebApp.Dtos
{
    [Serializable]
    public class MovieRatingDto
    {
        public string? Rating { get; set; }
        public int TotalMovies { get; set; }
    }
}
