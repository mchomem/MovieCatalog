using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieCatalog.WebApp.Models
{
    public class Movie
    {        
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        // Regex explanation:
        // Use only letters
        // The first letter is upper case
        // Empty space are allowed, whereas numbers and special characteres don't allowed.
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        [StringLength(30)]
        public string? Genre { get; set; }

        //[Range(1,100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }        

        [Display(Name = "Created in")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime CreatedIn { get; set; }

        [Display(Name = "Modified in")]
        [Column(TypeName = "datetime")]
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedIn { get; set; }

        [Required]
        public int RatingId { get; set; }
        public Rating? Rating { get; set; }
    }
}
