using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos
{
    public class ReadMovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime CreatedDate { get; set;} = DateTime.Now;
    }
}
