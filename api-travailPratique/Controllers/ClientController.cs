using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace api_travailPratique.Controllers
{
    [Produces("application/json")]
    [Route("/api/Client/")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        Models.ApiDbContext db = new Models.ApiDbContext();
        PasswordHasher<string> pw = new PasswordHasher<string>();
        public ClientController()
        {
            db = new Models.ApiDbContext();
        }

        [Authorize]
        [HttpGet]
        [Route("info")]
        public IActionResult GetClient()
        {
            string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            Models.Client client = db.Clients.SingleOrDefault(x => x.UserName == username);

            return Ok(client);
        }

        [Authorize]
        [HttpPut]
        [Route("info")]
        public IActionResult UpdateClient([FromForm] Models.UserForm userForm)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client client = db.Clients.SingleOrDefault(x => x.UserName == username);

                client.UserName = userForm.UserName;
                client.FirstName = userForm.FirstName;
                client.LastName = userForm.LastName;
                client.Password = pw.HashPassword(userForm.UserName, userForm.Password);

                db.SaveChanges();

                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("products/{productId}")]
        public IActionResult GetProducts(int productId)
        {
            try
            {
                Models.Produit produit = db.Produits.Find(productId);

                if (produit != null)
                {
                    return Ok(produit);
                }
                else
                    return StatusCode((int)StatusCodes.Status404NotFound, new { message = "Le produit n'existe pas !" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("stats")]
        public IActionResult GetStatClient()
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client client = db.Clients.SingleOrDefault(x => x.UserName == username);

                Models.StatClient stats = db.StatClients.SingleOrDefault(x => x.ClientId == client.Id);
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
