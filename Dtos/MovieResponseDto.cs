using moviesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace moviesApi.Dtos
{
    public class MovieResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public int ReleaseYear { get; set; }

        public int Rating { get; set; }

        public List<string>? Genres { get; set; }
    }
}
