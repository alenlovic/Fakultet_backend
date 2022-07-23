using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjekatFakultet.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ProjekatFakultet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes("application/json")]
    [Authorize]
    public class LoginController : ControllerBase
    {

        private List<User> listaKorisnika = new List<User>
        {
            new User { Username = "profesor123", Password = "admin", Role = "Admin"},
            new User { Username = "ucenik123", Password = "user", Role = "User"},
            new User { Username = "developer", Password = "service", Role = "Service"}
        };
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(User userLogin)
        {
            IActionResult response = Unauthorized();

            var korisnik = ProvjeraKorisnika(userLogin);

            if(korisnik != null)
            {
                var token = GenerisiToken(korisnik);
                response = Ok(new
                {
                    token
                });
                
            }
            return response;
        }

        public User ProvjeraKorisnika(User userLogin)
        {
            var user = listaKorisnika.SingleOrDefault(korisnik => korisnik.Username == userLogin.Username && korisnik.Password == userLogin.Password);

            return user;
        }

        public string GenerisiToken(User korisnik)
        {
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var kredencijali = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);

            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, korisnik.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("role", korisnik.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claim,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: kredencijali
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
