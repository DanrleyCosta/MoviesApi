using AutoMapper;
using FilmesApi.Controllers;
using FilmesApi.Data;
using FilmesApi.Data.Dtos;
using FilmesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using Xunit;

namespace FilmesApi.Tests
{
    public class CreateMovieTest
    {
        private readonly FilmeContext _context;
        private readonly Mock<IMapper> _mockMapper;
        private readonly FilmeController _controller;

        public CreateMovieTest()
        {
            // Setup in-memory database for testing
            var options = new DbContextOptionsBuilder<FilmeContext>()
                .UseInMemoryDatabase(databaseName: "FilmesTestDB")
                .Options;

            _context = new FilmeContext(options);
            _mockMapper = new Mock<IMapper>();
            _controller = new FilmeController(_context, _mockMapper.Object);
        }

        [Fact]
        public void AddMovies_ReturnsCreatedAtActionResult_WhenValidMovieIsAdded()
        {
            // Arrange
            var createMovieDto = new CreateMovieDto
            {
                Title = "Test Movie",
                Description = "Test Description",
                Duration = 120
            };
            var movie = new Movie
            {
                Id = 1,
                Title = "Test Movie",
                Description = "Test Description",
                Duration = 120
            };

            _mockMapper.Setup(m => m.Map<Movie>(It.IsAny<CreateMovieDto>())).Returns(movie);

            // Act
            var result = _controller.AddMovies(createMovieDto);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Movie>(actionResult.Value);
            Assert.Equal("Test Movie", returnValue.Title);
        }

        [Fact]
        public void AddMovies_ReturnsBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            var createMovieDto = new CreateMovieDto
            {
                Title = "Test Movie",
                Description = "Test Description",
                Duration = 120
            };

            _mockMapper.Setup(m => m.Map<Movie>(It.IsAny<CreateMovieDto>())).Throws(new Exception());

            // Act
            var result = _controller.AddMovies(createMovieDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
