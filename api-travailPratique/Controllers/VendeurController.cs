using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api_travailPratique.Controllers
{
    [Produces("application/json")]
    [Route("/api/Vendeur/")]
    [ApiController]
    public class VendeurController : ControllerBase
    {
        Models.ApiDbContext db = new Models.ApiDbContext();

        public VendeurController()
        {
            db = new Models.ApiDbContext();
        }

        [Authorize]
        [HttpGet]
        [Route("info")]
        public IActionResult GetVendeurs()
        {
            try
            {
                List<Models.Vendeur> vendeurs = db.Vendeurs.ToList();
                return Ok(vendeurs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPut]
        [Route("info/{vendeurId}")]
        public IActionResult UpdateVendeur(int vendeurId, [FromForm] Models.UserForm userForm)
        {
            try
            {
                Models.Vendeur vendeur = db.Vendeurs.Find(vendeurId);
              

                if (vendeur != null)
                {
                    vendeur.UserName = userForm.UserName;
                    vendeur.FirstName = userForm.FirstName;
                    vendeur.LastName = userForm.LastName;
                    vendeur.Password = userForm.Password;

                    db.SaveChanges();

                    return Ok(vendeur);
                }
                else
                    return StatusCode((int)StatusCodes.Status404NotFound, new { message = "L'utilisateur n'existe pas !" });
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
                List<Models.Produit> produits = db.Produits.ToList();
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
                db.Produits.Add(new Models.Produit()
                {
                    NomProduit = produitForm.NomProduit,
                    Quantite = produitForm.Quantite,
                    Price = produitForm.Price,
                    VendeurId = 1,
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
    }
}
