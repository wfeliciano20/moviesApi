using moviesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace moviesApi.Dtos
{
    public class MovieDto
    {
        public string Title { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public int ReleaseYear { get; set; }

    
        public int Rating { get; set; }

    }
}
