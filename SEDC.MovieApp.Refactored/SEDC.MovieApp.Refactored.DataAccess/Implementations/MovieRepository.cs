using SEDC.MovieApp.Refactored.DataAccess.Interfaces;
using SEDC.MovieApp.Refactored.Domain.Enums;
using SEDC.MovieApp.Refactored.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MovieApp.Refactored.DataAccess.Implementations
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieAppDbContext _context;

        public MovieRepository(MovieAppDbContext context)
        {
            _context = context;
        }

        public void Add(Movie entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Movie entity)
        {
            _context.Movies.Remove(entity);
            _context.SaveChanges();
        }

        public List<Movie> FilterMovies(int? year, int? genre)
        {
            //if no filter params are sent return all movies
            if(year == null && genre == null)
            {
                return _context.Movies.ToList();
            }

            if(year == null)
            {
                //if at least one of them is not null and year is null, that means for sure that genre is not null
               return _context.Movies.Where(x => x.Genre == (Genre)genre.Value).ToList();
            }

            if(genre == null)
            {
                return _context.Movies.Where(x => x.Year == year).ToList();
            }

            //the only case that is left is that both params are sent
            return _context.Movies.Where(x => x.Year == year && x.Genre == (Genre)genre.Value).ToList();
        }

        public List<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public Movie GetById(int id)
        {
            return _context.Movies.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Movie entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
