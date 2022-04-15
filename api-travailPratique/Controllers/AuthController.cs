using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
//using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace api_travailPratique.Controllers
{
    [Produces("application/json")]
    [Route("/api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IConfiguration _config;
        Models.ApiDbContext db = new Models.ApiDbContext();
        public AuthController(IConfiguration config)
        {
            _config = config;
            db = new Models.ApiDbContext();

        }

        string GenerateJSONWebToken(Models.User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<Models.User> AuthenticateUser(Models.Login login)
        {
            Models.User user = null;
            var ValidClient = db.Clients.Any(x => x.UserName.Equals(login.UserName));
            var ValidVendeur = db.Vendeurs.Any(x => x.UserName.Equals(login.UserName));
            if (ValidClient)
            {
                return db.Clients.FirstOrDefault(user =>
            user.UserName.Equals(login.UserName)
            && user.Password == login.Password);
            }
            else if(ValidVendeur) {
                return db.Vendeurs.FirstOrDefault(user =>
               user.UserName.Equals(login.UserName)
               && user.Password == login.Password);
            }
            else
            {
                return user;
            }
            }


        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] Models.Login data)
        {
            IActionResult response = Unauthorized();
            var user = await AuthenticateUser(data);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { Token = tokenString, Message = "Success" });
            }
            return response;
        }
        [HttpPost(nameof(Register))]
        public IActionResult Register([FromBody] Models.Register register)
        {
            if (register.Role == "Client")
            {
                db.Clients.Add(new Models.Client
                {
                    UserName = register.UserName,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Password = register.Password,
                    Profil = register.Role,
                    Solde = 5000 });
                db.SaveChanges();
                return Ok(db.Clients);
            }
            else if (register.Role == "Vendeur")
            {
                db.Vendeurs.Add(new Models.Vendeur
                {
                    UserName = register.UserName,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Password = register.Password,
                    Profil = register.Role,
                    Solde = 0
                });
                db.SaveChanges();
                return Ok(db.Vendeurs);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
