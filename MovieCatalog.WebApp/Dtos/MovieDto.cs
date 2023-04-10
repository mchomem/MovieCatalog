namespace MovieCatalog.WebApp.Dtos
{
    [Serializable]
    public class MovieDto
    {
        public string? Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public string? Rating { get; set; }
    }
}
