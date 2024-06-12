using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int Id { get; set; }
        //public int Id { get; set; }
        [Required(ErrorMessage = "the Title is necessery")]
        public string Title { get; set; }

        [Required(ErrorMessage = "the Description is necessery")]
        [MaxLength(50, ErrorMessage = "the size is invalid")]
        public string Description { get; set; }

        [Required(ErrorMessage = "the Duration is necessery")]
        [Range(70, 600 , ErrorMessage = "The Duration must have 70 between 600 minutes")]
        public int Duration { get; set; }
    }
}
