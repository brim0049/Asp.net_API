using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_travailPratique.Controllers
{
    [Route("api/[product]")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        Models.ApiDbContext db = new Models.ApiDbContext();
        public ProduitController()
        {
            db = new Models.ApiDbContext();
        }
        [HttpGet("{product_id}", Name = "GetProduct")]
        public IActionResult GetProduct(int product_id)
        {
            try
            {
                List<Models.Client> clients = db.Clients.ToList();
                Models.Client client = clients[product_id];
                return Ok(client);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
