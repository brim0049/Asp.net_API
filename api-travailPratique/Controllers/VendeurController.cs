using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        [HttpPut]
        [Route("info")]
        public IActionResult UpdateVendeur([FromForm] Models.UserForm userForm)
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Vendeur vendeur = db.Vendeurs.SingleOrDefault(x => x.UserName == username);

                vendeur.UserName = userForm.UserName;
                vendeur.FirstName = userForm.FirstName;
                vendeur.LastName = userForm.LastName;
                vendeur.Password = pw.HashPassword(userForm.UserName, userForm.Password);

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

                List<Models.Produit> produits = db.Produits.Where(x => x.VendeurId == vendeur.Id).ToList();
                return Ok(produits);
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

                db.Produits.Add(new Models.Produit()
                {
                    NomProduit = produitForm.NomProduit,
                    Quantite = produitForm.Quantite,
                    Price = produitForm.Price,
                    VendeurId = vendeur.Id,
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
        [HttpPut]
        [Route("products/{productId}")]
        public IActionResult UpdateProduct(int productId, [FromForm] Models.ProduitForm produitForm)
        {
            try
            {
                Models.Produit produit = db.Produits.Find(productId);

                if (produit != null)
                {
                    produit.NomProduit = produitForm.NomProduit;
                    produit.Quantite = produitForm.Quantite;
                    produit.Price = produitForm.Price;

                    db.SaveChanges();

                    return Ok(produit);
                }
                else
                    return StatusCode((int)StatusCodes.Status404NotFound, new { message = "L'utilisateur ou Le produit n'existe pas !" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("products/{productId}")]
        public IActionResult RemoveProduct(int productId)
        {
            try
            {
                Models.Produit produit = db.Produits.Find(productId);

                if (produit != null)
                {
                    db.Produits.Remove(produit);
                    db.SaveChanges();
                    return Ok();
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
        public IActionResult GetStatVendeur()
        {
            try
            {
                string username = HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                Models.Vendeur vendeur = db.Vendeurs.SingleOrDefault(x => x.UserName == username);

                Models.StatVendeur stats = db.StatVendeurs.SingleOrDefault(x => x.VendeurId == vendeur.Id);
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
