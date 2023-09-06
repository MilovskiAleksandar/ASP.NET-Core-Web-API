using SEDC.MovieApp.Refactored.DTOs.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MovieApp.Refactored.Service.Interfaces
{
    public interface IMovieService
    {
        List<MovieDto> GetAllMovie();
        MovieDto GetMovieById(int id);
        void AddMovie(AddMovieDto addMovieDto);
        void DeleteMovie(int id);
        void UpdateMovie(UpdateMovieDto updateMovieDto);

        List<MovieDto> FilterMovies(int? year, int? genre);
    }
}
