using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WordFinder_Domain.Models;

namespace WordFinder_Business
{
    public static class JWThandler
    {
        private static string UserId = "userId";
        
        public static string GenerateTokenForUser(User user, IConfiguration config)
        {
            var key = config["auth:key"];
            var skey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(skey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new List<Claim>()
            {
                new Claim(UserId, user.Id.ToString())
            };
            var token = new JwtSecurityToken(
                claims: claims,
                issuer: config["auth:issuer"],
                audience: config["auth:audience"],
                expires: DateTime.Now.AddDays(Int32.Parse(config["auth:expiredays"])),
                signingCredentials: credentials
            );
            var tokenString =  new JwtSecurityTokenHandler().WriteToken(token);
            
            return tokenString;
        }
        
        public static long GetUserId(string token)
        {
            var securityToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var userId = Int32.Parse(securityToken.Claims
                .First(claim => claim.Type == UserId)
                .Value);

            return userId;
        }
    }
}