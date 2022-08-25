using jwtauth.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace jwtauth.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration configuration;
        Dictionary<string, string> UserRecords = new Dictionary<string, string>
         {
             {"admin1","password1" },
             {"admin2","password2" },
             {"admin3","password3" }
         };
        public JWTManagerRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Tokens Authenticate(Users user)
        {
           if(!UserRecords.Any(x=>x.Key==user.Name && x.Value == user.Password))
            {
                return null;
            }
           //else we generate token handler
            var tokenhandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                    }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenhandler.CreateToken(tokenDescriptior);
            return new Tokens { Token = tokenhandler.WriteToken(token) };
        }

    }
}
