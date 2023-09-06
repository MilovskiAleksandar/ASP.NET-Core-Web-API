using Microsoft.IdentityModel.Tokens;
using SEDC.MovieApp.Refactored.DataAccess.Interfaces;
using SEDC.MovieApp.Refactored.Domain.Models;
using SEDC.MovieApp.Refactored.DTOs.Users;
using SEDC.MovieApp.Refactored.Mappers.Users;
using SEDC.MovieApp.Refactored.Service.Interfaces;
using SEDC.MovieApp.Refactored.Shared.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XSystem.Security.Cryptography;

namespace SEDC.MovieApp.Refactored.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Registration(RegisterUserDto registerUser)
        {
            //Validate
            if(string.IsNullOrEmpty(registerUser.UserName))
            {
                throw new GeneralException("You must send the username");
            }
            if (string.IsNullOrEmpty(registerUser.FirstName))
            {
                throw new GeneralException("You must send the first name");
            }

            if (string.IsNullOrEmpty(registerUser.LastName))
            {
                throw new GeneralException("You must send the last name");
            }

            if (string.IsNullOrEmpty(registerUser.Password) || string.IsNullOrEmpty(registerUser.ConfirmPassword))
            {
                throw new GeneralException("Password fields are required");
            }

            if (registerUser.Password != registerUser.ConfirmPassword)
            {
                throw new GeneralException("Password do not match");
            }

            
            User users = _userRepository.GetUserByUsername(registerUser.UserName);
            if(users != null)
            {
                throw new GeneralException($"Username {registerUser.UserName} exists!!! Try with new username");
            }

            //HASH
            //MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            //byte[] bytes = Encoding.ASCII.GetBytes(registerUser.Password);
            //byte[] hashedPassword = mD5CryptoServiceProvider.ComputeHash(bytes);

            //string hash = Encoding.ASCII.GetString(hashedPassword);

            string hash = HashedPassword(registerUser.Password);

            User newUser = registerUser.ToUser(hash);

            _userRepository.Add(newUser);

        }

        public string Login(LoginUserDto loginUser)
        {
            if (string.IsNullOrEmpty(loginUser.UserName) || string.IsNullOrEmpty(loginUser.Password))
            {
                throw new GeneralException("Username and Password fields are required");
            }

            string hash = HashedPassword(loginUser.Password);

            User userDb = _userRepository.GetUserByUsernameAndPassword(loginUser.UserName, hash);
            if(userDb == null)
            {
                throw new ResourceNotFoundException($"You can not login with this username: {loginUser.UserName}, try again");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] keyBytes = Encoding.ASCII.GetBytes("Our very secret secretttt secret key");
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(1), // the token will be valid for one hour
                //signature configuration, signing algorithm that will be used to generate hash (third part of token)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
                                SecurityAlgorithms.HmacSha256Signature),
                //payload
                Subject = new ClaimsIdentity(
                                new[]
                                {
                        new Claim("username", userDb.UserName)
                                })
            };
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string resultToken = jwtSecurityTokenHandler.WriteToken(token);
            return resultToken;
        }

        private string HashedPassword(string password)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] hashedPassword = mD5CryptoServiceProvider.ComputeHash(bytes);

            return Encoding.ASCII.GetString(hashedPassword);
        }
    }
}
