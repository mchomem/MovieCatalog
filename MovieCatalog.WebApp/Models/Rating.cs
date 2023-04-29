using System.ComponentModel.DataAnnotations;

namespace MovieCatalog.WebApp.Models
{
    public class Rating
    {
        public int Id { get; set; }

        // Regex explanation:
        // The first letter is upper case
        // Allow numbers and special characteres in subsequencial space, "PG-13" is valid!
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [Required]
        [StringLength(5)]
        public string? Name { get; set; }

        [Required]
        public ICollection<Movie>? Movies { get; set; }
    }
}
