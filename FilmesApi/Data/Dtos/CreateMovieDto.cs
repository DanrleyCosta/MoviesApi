using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Data.Dtos
{
    public class CreateMovieDto
    {
        [Required(ErrorMessage = "the Title is necessery")]
        public string Title { get; set; }

        [Required(ErrorMessage = "the Description is necessery")]
        [StringLength(50, ErrorMessage = "the size is invalid")]
        public string Description { get; set; }

        [Required(ErrorMessage = "the Duration is necessery")]
        [Range(70, 600, ErrorMessage = "The Duration must have 70 between 600 minutes")]
        public int Duration { get; set; }
    }
}
