using MovieCatalog.WebApp.Models;

namespace MovieCatalog.WebApp.Dtos
{
    [Serializable]
    public class MovieDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
        public Rating Rating { get; set; }

        public MovieDto()
        {
            Rating = new Rating();
        }
    }
}
