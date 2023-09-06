using SEDC.MovieApp.Refactored.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.MovieApp.Refactored.Service.Interfaces
{
    public interface IUserService
    {
        void Registration(RegisterUserDto registerUser);
        string Login(LoginUserDto loginUserDto);

    }
}
