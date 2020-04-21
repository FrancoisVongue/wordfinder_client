using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WordFinder_Domain.Models;
using WordFinder_Domain.ServiceIO;
using WordFinder_Repository;

namespace WordFinder_Business
{
    public class UserService
    {
        private readonly ApiContext _context;
        private readonly IConfiguration _configuration;
        private static readonly int SaltLength = 9; 

        public UserService(ApiContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        public UserService() {}

        public User Register(User user)
        {
            user.Salt = PasswordHandler.getSalt(SaltLength);
            user.Password = PasswordHandler.getHashed(user.Salt + user.Password);
            
            var addedUser = _context.Users.Add(user).Entity;
            _context.SaveChanges();
            return addedUser;
        }

        public User GetUserByCredentials(SignInCredentials credentials)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == credentials.Login);
            if (user != null)
            {
                var hashedInput = PasswordHandler.getHashed(user.Salt + credentials.Password);
                var correctPassword = PasswordHandler.verifyPassword(hashedInput, user.Password);
                
                if (correctPassword)
                    return user;
            }
            return null;
        }

        public User GetUserByToken(string token)
        {
            var id = JWThandler.GetUserId(token);
            return getUserById(id);
        }

        public string CreateTokenForUser(User user)
        {
            string token = JWThandler.GenerateTokenForUser(user, _configuration);
            return token;
        }

        private User getUserById(long id)
        {
            return _context.Users.Find(id);
        }
    }
}