using SEDC.MovieApp.Refactored.DataAccess.Interfaces;
using SEDC.MovieApp.Refactored.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MovieApp.Refactored.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieAppDbContext _movieAppDbContext;

        public UserRepository(MovieAppDbContext movieAppDbContext)
        {
            _movieAppDbContext = movieAppDbContext;
        }

        public void Add(User entity)
        {
            _movieAppDbContext.Users.Add(entity);
            _movieAppDbContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            return _movieAppDbContext.Users.FirstOrDefault(x => x.UserName == username);
        }

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            return _movieAppDbContext.Users.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower() && x.Password == password);
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
