using Microsoft.AspNetCore.Mvc.Rendering;
using MovieCatalog.WebApp.Dtos;

namespace MovieCatalog.WebApp.Models
{
    public class MovieViewModel : PaginatedList
    {
        public MoviePackageData? MoviePackageData { set; get; }
        public SelectList? Genres { get; set; }
        public SelectList? Ratings { get; set; }
        public string? TitleFilter { get; set; }
        public string? GenreFilter { get; set; }
        public string? RatingFilter { get; set; }
    }

    public class MoviePackageData
    {
        public List<MovieDto>? Movies { get; set; }
        public int TotalPageMovie { get; set; }
        public int TotalRecordsFounded { get; set; }
    }
}
