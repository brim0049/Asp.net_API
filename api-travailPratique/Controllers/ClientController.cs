using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;
using System.Text;

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
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client client = db.Clients.SingleOrDefault(x => x.UserName.Equals(username));
                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("info")]
        public IActionResult UpdateClient([FromForm] Models.UserForm userForm)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client client = db.Clients.SingleOrDefault(x => x.UserName == username);
                client.FirstName = userForm.FirstName is null? client.FirstName : userForm.FirstName;
                client.LastName = userForm.LastName is null ? client.LastName : userForm.LastName;
                client.Password = pw.HashPassword(client.UserName, userForm.Password);
                client.Solde = userForm.Solde is null ? client.Solde : userForm.Solde;
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
        [Route("products/{keyword}")]
        public IActionResult GetProductsBykeyword(string keyword)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client clientLogin = db.Clients.SingleOrDefault(x => x.UserName == username);
                Models.Client client = db.Clients.Include(User => User.Produits).Where(user => user.Id == clientLogin.Id).First();
                List<Models.Produit> produits = client.Produits.Where(s => s.NomProduit.Contains(keyword)).ToList();
                return Ok(produits);
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
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client clientLogin = db.Clients.SingleOrDefault(x => x.UserName == username);
                Models.Client client = db.Clients.Include(User => User.Produits).Where(user => user.Id == clientLogin.Id).First();
                Models.Produit produit = client.Produits.Where(s => s.Id == productId).FirstOrDefault();
                if (produit != null)
                {
                    return Ok(produit);

                }
                else
                {
                    return StatusCode((int)StatusCodes.Status404NotFound, new { message = "Le produit n'existe pas !" });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
//cart
        [Authorize]
        [HttpGet]
        [Route("cart")]
        public IActionResult GetClientCart()
        {
            try {

                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client client_logged = db.Clients.SingleOrDefault(x => x.UserName.Equals(username));
                Models.Client client = db.Clients.Include(User => User.Produits).Where(user => user.Id == client_logged.Id).First();
                List<Models.Produit> produits = client.Produits.ToList();
                return Ok(produits);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        [Route("cart")]
        public IActionResult AcheterProduct([FromForm] int productId)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client clientLogin = db.Clients.SingleOrDefault(x => x.UserName == username);
                Models.Client client = db.Clients.Include(User => User.Produits).Where(user => user.Id == clientLogin.Id).First();
                client.Produits.Add(db.Produits.SingleOrDefault(p => p.Id == productId));

                db.SaveChanges();

                return StatusCode((int)StatusCodes.Status201Created, new { message = "Le produit a été ajouté !" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
            [HttpDelete]
            [Route("cart/{produitId}")]
            public IActionResult RemoveFromCart(int produitId)
            {
                try
                {
                    string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                    Models.Client clientLogin = db.Clients.SingleOrDefault(x => x.UserName == username);
                    Models.Client client = db.Clients.Include(User => User.Produits).Where(user => user.Id == clientLogin.Id).First();
                    client.Produits.Remove(db.Produits.Where(p => p.Id == produitId).FirstOrDefault());
                    db.SaveChanges();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        [Authorize]
        [HttpPatch]
        [Route("cart/{produitId}")]
        public IActionResult UpdateCart(int produitId, [FromForm] int quantite)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client clientLogin = db.Clients.SingleOrDefault(x => x.UserName == username);
                Models.Client client = db.Clients.Include(User => User.Produits).Where(user => user.Id == clientLogin.Id).First();
                client.Produits.Where(p => p.Id == produitId).FirstOrDefault().Quantite=quantite;
                db.SaveChanges();
                return Ok(client.Produits.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
 //pay
        [Authorize]
        [HttpGet]
        [Route("pay")]
        public IActionResult GetPay()
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client client = db.Clients.SingleOrDefault(x => x.UserName == username);
                Models.Client client_pro = db.Clients.Include(User => User.Produits).Where(user => user.Id == client.Id).First();
                decimal somme = client_pro.Produits.Sum(produit => produit.Price * produit.Quantite);
                Models.Facture _facture = new Models.Facture() { };
                _facture.Total = somme;
                Models.Client clientFacture = db.Clients.Include(User => User.Factures).Where(user => user.Id == client.Id).First();
                clientFacture.Factures.Add(_facture);
                db.SaveChanges();
                int id = clientFacture.Id;

                return StatusCode((int)StatusCodes.Status201Created, new { message = "Le paiement a été effectué !", id });
            }
            catch (Exception ex)
            {
                return BadRequest( ex.InnerException.Message);
            }
        }
//invoices
        [Authorize]
        [HttpGet]
        [Route("invoices")]
        public IActionResult GetFactures()
        {
            try
            {

                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client client_logged = db.Clients.SingleOrDefault(x => x.UserName.Equals(username));
                Models.Client client = db.Clients.Include(User => User.Factures).Where(user => user.Id == client_logged.Id).First();
                List<Models.Facture> factures = client.Factures.ToList();
                return Ok(factures);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("invoices/{invoice_id}")]
        public IActionResult GetFacture(int invoice_id)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client clientLogin = db.Clients.SingleOrDefault(x => x.UserName == username);
                Models.Client client = db.Clients.Include(User => User.Factures).Where(user => user.Id == clientLogin.Id).First();
                Models.Facture facture = client.Factures.Where(s => s.Id == invoice_id).FirstOrDefault();
               
                if (facture != null)
                {
                    return Ok(facture);

                }
                else
                {
                    return StatusCode((int)StatusCodes.Status404NotFound, new { message = "La facture n'existe pas !" });

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //stat
        [Authorize]
        [HttpGet]
        [Route("stats")]
        public IActionResult GetStatClient()
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Client client = db.Clients.SingleOrDefault(x => x.UserName == username);
                Models.Client clientFacture = db.Clients.Include(User => User.Factures).Where(user => user.Id == client.Id).First();
                List<Models.Facture> factures = clientFacture.Factures.ToList();
                decimal TotalFacture = clientFacture.Factures.Sum(produit => produit.Total);
                Models.Client clientProduit = db.Clients.Include(User => User.Produits).Where(user => user.Id == client.Id).First();
                int produits = clientProduit.Produits.Count();
                return StatusCode((int)StatusCodes.Status200OK, new { message = string.Format("Le client {0} {1} a depensé {2:0.00} sur {3} produits !", client.FirstName , client.LastName, TotalFacture, produits) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
 