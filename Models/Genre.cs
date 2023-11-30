using System.ComponentModel.DataAnnotations;

namespace moviesApi.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        public List<Movie>? Movies { get; set; }
    }
}