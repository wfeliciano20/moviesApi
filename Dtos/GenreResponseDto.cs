namespace moviesApi.Dtos
{
    public class GenreResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public List<string>? Movies { get; set; } = new List<string>();
    }
}
