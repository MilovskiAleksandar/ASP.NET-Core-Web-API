using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.MovieApp.Refactored.DataAccess;
using SEDC.MovieApp.Refactored.DataAccess.Implementations;
using SEDC.MovieApp.Refactored.DataAccess.Interfaces;
using SEDC.MovieApp.Refactored.Domain.Models;
using SEDC.MovieApp.Refactored.Service.Implementations;
using SEDC.MovieApp.Refactored.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MovieApp.Refactored.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<MovieAppDbContext>(x => x.UseSqlServer("Server=.\\;Database=MovieAppRefactored;Trusted_Connection=True;TrustServerCertificate=True"));
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IMovieService, MovieService>();
            services.AddTransient<IUserService, UserService>();
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            //services.AddTransient<IRepository<Movie>, MovieRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
