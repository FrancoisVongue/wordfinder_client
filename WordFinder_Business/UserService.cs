using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WordFinder_Domain.Models;
using WordFinder_Repository;

namespace WordFinder_Business
{
    public class UserService
    {
        private readonly ApiContext _context;
        private readonly IConfiguration _config;

        public UserService(ApiContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public User Register(User user)
        {
            setHashedPassword(user);
            
            var addedUser = _context.Users.Add(user).Entity;
            _context.SaveChanges();
            return addedUser;
        }

        public string GenerateToken(User user)
        {
            var key = _config["auth:key"];
            var skey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(skey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new List<Claim>()
            {
                new Claim("userId", user.Id.ToString())
            };
            var token = new JwtSecurityToken(
                claims: claims,
                issuer: _config["auth:issuer"],
                audience: _config["auth:audience"],
                expires: DateTime.Now.AddDays(Int32.Parse(_config["auth:expiredays"])),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User trySignIn(string credentialsLogin, string credentialsPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.Login == credentialsLogin);
            if (user != null)
            {
                var correctPassword = verifyPassword(credentialsPassword, user);
                if (correctPassword)
                {
                    return user;
                }
            }

            return null;
        }

        public User GetUser(HttpContext context)
        {
            var token = (string)context.Request.Headers["Authorization"];
            token = token.Split(' ')[1]; // get rid of bearer prefix
            
            var securityToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var userId = Int32.Parse(securityToken.Claims
                .First(claim => claim.Type == "userId")
                .Value);
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            return user;
        }

        private string setHashedPassword(User user)
        {
            var salt = getSalt(8);
            string hash;
            
            using (SHA256 sha256Hash = SHA256.Create())
            {
                hash = GetHash(sha256Hash, user.Password + salt);
            }

            user.Salt = salt;
            user.Password = hash;

            return hash;
        }

        private bool verifyPassword(string pw, User u)
        {
            bool valid;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                valid = verifyHash(sha256Hash, pw + u.Salt, u.Password);
            }

            return valid;
        }

        private string getSalt(int length)
        {
            const string symbols = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            var salt = new StringBuilder();;
            
            for (int i = 0; i < length; i++)
            {
                salt.Append(symbols[random.Next(symbols.Length)]);
            }

            return salt.ToString();
        }

        private string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            
            return sBuilder.ToString();
        }

        private bool verifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            var hashOfInput = GetHash(hashAlgorithm, input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}