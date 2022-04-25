using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace api_travailPratique.Controllers
{
    [Produces("application/json")]
    [Route("/api/Vendeur/")]
    [ApiController]
    public class VendeurController : ControllerBase
    {
        Models.ApiDbContext db = new Models.ApiDbContext();
        PasswordHasher<string> pw = new PasswordHasher<string>();

        public VendeurController()
        {
            db = new Models.ApiDbContext();
        }

        [Authorize]
        [HttpGet]
        [Route("info")]
        public IActionResult GetVendeur()
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Vendeur vendeur = db.Vendeurs.SingleOrDefault(x => x.UserName == username);
                return Ok(vendeur);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch]
        [Route("info")]
        public IActionResult UpdateVendeur([FromForm] Models.UserForm userForm)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Vendeur vendeur = db.Vendeurs.SingleOrDefault(x => x.UserName == username);

                vendeur.FirstName = userForm.FirstName is null ?  vendeur.FirstName : userForm.FirstName;
                vendeur.LastName = userForm.LastName is null ? vendeur.LastName : userForm.LastName;
                vendeur.Password = pw.HashPassword(vendeur.UserName, userForm.Password);
                vendeur.Solde = userForm.Solde is null ? vendeur.Solde : userForm.Solde;

                db.SaveChanges();

                return Ok(vendeur);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("products")]
        public IActionResult GetProducts()
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Vendeur vendeur = db.Vendeurs.SingleOrDefault(x => x.UserName == username);

                Models.Vendeur vendeurProduit = db.Vendeurs.Include(User => User.Produits).Where(user => user.Id == vendeur.Id).First();
                List<Models.Produit> produits = vendeurProduit.Produits.ToList();
                return Ok(vendeurProduit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("products")]
        public IActionResult CreateProduct([FromForm] Models.ProduitForm produitForm)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Vendeur vendeur = db.Vendeurs.SingleOrDefault(x => x.UserName == username);

                vendeur.Produits.Add(new Models.Produit()
                {
                    NomProduit = produitForm.NomProduit,
                    Quantite = produitForm.Quantite,
                    Price = produitForm.Price,
                });
                db.SaveChanges();

                return StatusCode((int)StatusCodes.Status201Created, new { message = "Le produit a été créé !" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("products/{productId}")]
        public IActionResult GetProduct(int productId)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Vendeur vendeurLogged = db.Vendeurs.SingleOrDefault(x => x.UserName == username);
                Models.Vendeur vendeur = db.Vendeurs.Include(User => User.Produits).Where(user => user.Id == vendeurLogged.Id).First();
                Models.Produit produit = vendeur.Produits.Where(s => s.Id == productId).FirstOrDefault();

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
        [HttpPatch]
        [Route("products/{productId}")]
        public IActionResult UpdateProduct(int productId, [FromForm] Models.ProduitForm produitForm)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Vendeur vendeurLogged = db.Vendeurs.SingleOrDefault(x => x.UserName == username);
                Models.Vendeur vendeur = db.Vendeurs.Include(User => User.Produits).Where(user => user.Id == vendeurLogged.Id).First();
                Models.Produit produit = vendeur.Produits.Where(p => p.Id == productId).FirstOrDefault();
                produit.NomProduit = produitForm.NomProduit is null? produit.NomProduit: produitForm.NomProduit;
                produit.Price = produitForm.Price.ToString() is null? produit.Price: produitForm.Price;
                produit.Quantite = produitForm.Quantite.ToString() is null? produit.Quantite: produitForm.Quantite;
                db.SaveChanges();
                return Ok(vendeur.Produits.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("products/{produitId}")]
        public IActionResult RemoveProduct(int produitId)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Vendeur vendeurLogged = db.Vendeurs.SingleOrDefault(x => x.UserName == username);
                Models.Vendeur vendeur = db.Vendeurs.Include(User => User.Produits).Where(user => user.Id == vendeurLogged.Id).First();
                vendeur.Produits.Remove(db.Produits.Where(p => p.Id == produitId).FirstOrDefault());
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("stats")]
        public IActionResult GetStatVendeur()
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Vendeur vendeur = db.Vendeurs.SingleOrDefault(x => x.UserName == username);
                Models.Vendeur vendeurFacture = db.Vendeurs.Include(User => User.Factures).Where(user => user.Id == vendeur.Id).First();
                List<Models.Facture> factures = vendeurFacture.Factures.ToList();
                decimal TotalFacture = vendeurFacture.Factures.Sum(produit => produit.Total);
                Models.Vendeur vendeurProduit = db.Vendeurs.Include(User => User.Produits).Where(user => user.Id == vendeur.Id).First();
                int produits = vendeurProduit.Produits.Count();
                return StatusCode((int)StatusCodes.Status200OK, new { message = String.Format("Le vendeur {0} {1} a gagné {2:0.00} et a {3} produits !", vendeur.FirstName, vendeur.LastName, TotalFacture, produits) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
