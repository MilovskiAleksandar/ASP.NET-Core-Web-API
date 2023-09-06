using SEDC.MovieApp.Refactored.Domain.Models;
using SEDC.MovieApp.Refactored.DTOs.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MovieApp.Refactored.Mappers.Movies
{
    public static class MovieMapper
    {
        public static MovieDto ToDtoMovie(this Movie movie)
        {
            return new MovieDto
            {
                Title = movie.Title,
                Year = movie.Year,
                Description = movie.Description,
                Genre = movie.Genre
            };
        }

        public static Movie ToMovie(this AddMovieDto addMovieDto)
        {
            return new Movie
            {
                Title = addMovieDto.Title,
                Year = addMovieDto.Year,
                Description = addMovieDto.Description,
                Genre = addMovieDto.Genre
            };
        }
    }
}
