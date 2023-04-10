using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieCatalog.WebApp.Models
{
    public class MovieFiltersViewModel
    {
        public List<Movie>? Movies { get; set; }
        public SelectList? Genres { get; set; }
        public SelectList? Ratings {get; set;}
        public string? MovieTitle { get; set; }
        public string? MovieGenre { get; set; }
        public string? MovieRating { get; set; }
    }
}
