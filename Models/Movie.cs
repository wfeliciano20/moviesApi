using System.ComponentModel.DataAnnotations;
using System.IO;

namespace moviesApi.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = String.Empty;

        [Required]
        public string Description { get; set; } = String.Empty;

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }

        [MaxLength(3, ErrorMessage = "There must be at most 3 genres")]
        public List<Genre>? Genres { get; set; }
   
        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; } 


    }
}
