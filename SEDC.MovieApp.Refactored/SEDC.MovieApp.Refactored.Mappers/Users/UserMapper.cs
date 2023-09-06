using SEDC.MovieApp.Refactored.Domain.Models;
using SEDC.MovieApp.Refactored.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MovieApp.Refactored.Mappers.Users
{
    public static class UserMapper
    {
        public static User ToUser(this RegisterUserDto registerUser, string hash)
        {
            return new User
            {
                FirstName = registerUser.FirstName,
                LastName = registerUser.LastName,
                UserName = registerUser.UserName,
                Password = hash,
                FavoriteGenre = registerUser.FavoriteGenre
            };
        }
    }
}
