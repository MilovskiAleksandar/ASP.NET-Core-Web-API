using Microsoft.IdentityModel.Tokens;
using SEDC.MovieApp.Refactored.DataAccess.Implementations;
using SEDC.MovieApp.Refactored.DataAccess.Interfaces;
using SEDC.MovieApp.Refactored.Domain.Models;
using SEDC.MovieApp.Refactored.DTOs.Movies;
using SEDC.MovieApp.Refactored.Mappers.Movies;
using SEDC.MovieApp.Refactored.Service.Interfaces;
using SEDC.MovieApp.Refactored.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MovieApp.Refactored.Service.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        //TODO METHOD FOR BOTH VALIDATIONS ADD AND UPDATE
        public void AddMovie(AddMovieDto addMovieDto)
        {
            if (addMovieDto == null)
            {
                throw new GeneralException("Movie can not be added");
            }

            ValidateData(addMovieDto);

            Movie movie = addMovieDto.ToMovie();

            _movieRepository.Add(movie);
        }

        private static void ValidateData(MovieDto movieDto)
        {
            if (string.IsNullOrEmpty(movieDto.Title))
            {
                throw new GeneralException("Title can not be null");
            }

            if (movieDto.Year <= 0)
            {
                throw new GeneralException("Year can not be negative");
            }
        }

        public void DeleteMovie(int id)
        {
            Movie movie = _movieRepository.GetById(id);
            if(movie == null)
            {
                throw new ResourceNotFoundException($"Movie with id {id} was not found");
            }

            _movieRepository.Delete(movie);
        }

        public List<MovieDto> FilterMovies(int? year, int? genre)
        {
            if(year != null && year <= 0)
            {
                throw new GeneralException("Year in filtering can not be negative");
            }

            //always filter on database level and you have the chance!!
            List<Movie> movieDb = _movieRepository.FilterMovies(year, genre);

            return movieDb.Select(x => x.ToDtoMovie()).ToList();
        }

        public List<MovieDto> GetAllMovie()
        {
            List<Movie> movieDb = _movieRepository.GetAll();

            return movieDb.Select(x => x.ToDtoMovie()).ToList();
        }

        public MovieDto GetMovieById(int id)
        {
            Movie movie = _movieRepository.GetById(id);
            if(movie == null)
            {
                throw new Exception($"Movie with id {id} was not found");
            }
            return movie.ToDtoMovie();
        }

        public void UpdateMovie(UpdateMovieDto updateMovieDto)
        {
            Movie movieDb = _movieRepository.GetById(updateMovieDto.Id);
            if( movieDb == null)
            {
                throw new ResourceNotFoundException($"Movie with id {updateMovieDto.Id} was not found");
            }

            ValidateData(updateMovieDto);

            movieDb.Year = updateMovieDto.Year;
            movieDb.Title = updateMovieDto.Title;
            movieDb.Description = updateMovieDto.Description;
            movieDb.Genre = updateMovieDto.Genre;

            _movieRepository.Update(movieDb);
        }
    }
}
