using FilmesApi.Data;
using FilmesApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : Controller
    {
        private FilmeContext _context;

        public FilmeController(FilmeContext context) 
        {
            _context = context;
        }    

        [HttpPost]
        public IActionResult AddMovies([FromBody] Movie movie)
        {
            try
            {
                _context.Filmes.Add(movie);
                _context.SaveChanges();
                return CreatedAtAction(nameof(ReadMovieById), new { id = movie.Id}, movie);  
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]  
        public IActionResult ReadMovie([FromQuery] int skip = 0, [FromQuery] int take = 10) 
        {
            var resultMovie = _context.Filmes.Skip(skip).Take(take).ToList();   
            return Ok(resultMovie);
        }

        [HttpGet("{Id}")]
        public IActionResult ReadMovieById(int Id)
        {
            var movie = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);   
            if (movie == null) { return  NotFound(); }  
            return Ok(movie);
        }



    }
}
