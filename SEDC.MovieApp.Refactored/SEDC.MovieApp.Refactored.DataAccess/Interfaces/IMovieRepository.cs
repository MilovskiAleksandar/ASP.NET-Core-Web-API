using SEDC.MovieApp.Refactored.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MovieApp.Refactored.DataAccess.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        List<Movie> FilterMovies(int? year, int? genre); //movie specific method
        //we create a separated repository interface to contain all CRUD methods from IRepository
        //plus all methods related 
    }
}
