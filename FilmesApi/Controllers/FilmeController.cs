using AutoMapper;
using Azure;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : Controller
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddMovies([FromBody] CreateMovieDto movieDto)
        {
            try
            {
                var movie = _mapper.Map<Movie>(movieDto);
                _context.Filmes.Add(movie);
                _context.SaveChanges();
                return CreatedAtAction(nameof(ReadMovieById), new { id = movie.Id }, movie);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IEnumerable<ReadMovieDto> ReadMovie([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var resultMovie = _context.Filmes.Skip(skip).Take(take).ToList();
            return _mapper.Map<List<ReadMovieDto>>(resultMovie);
        }

        [HttpGet("{Id}")]
        public IActionResult ReadMovieById(int Id)
        {
            var movie = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);
            if (movie == null) { return NotFound(); }
            var movieDto = _mapper.Map<ReadMovieDto>(movie);

            return Ok(movieDto);
        }

        [HttpPut("{Id}")]
        public IActionResult UpdateMovieById(int Id, [FromBody] UpdateMovieDto movieDto)
        {
            var movie = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);

            if (movie == null) { return NotFound(); }
            _mapper.Map(movieDto, movie);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{Id}")]
        public IActionResult UpdateMovieByIdPatch(int Id, JsonPatchDocument<UpdateMovieDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var movie = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);
            if (movie == null) { return NotFound(); }

            var movieToPatch = _mapper.Map<UpdateMovieDto>(movie);
            patchDoc.ApplyTo(movieToPatch, ModelState);

            if (!TryValidateModel(movieToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(movieToPatch, movie);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteMovieById(int Id)
        {
            var movie = _context.Filmes.FirstOrDefault(filme => filme.Id == Id);

            if (movie == null) { return NotFound(); }
            _context.Remove(movie);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
