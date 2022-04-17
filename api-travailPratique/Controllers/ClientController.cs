using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_travailPratique.Controllers
{
    [Produces("application/json")]
    [Route("api/info")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        Models.ApiDbContext db = new Models.ApiDbContext();
        public ClientController()
        {
            db = new Models.ApiDbContext();
        }
        [Authorize]
        [HttpGet(Name = "GetClients")]
        public IActionResult GetClients()
        {
            List<Models.Client> clients = db.Clients.ToList();

            return Ok(clients);
        }
       
        [HttpPatch("{client_id}")]
        public IActionResult CreateTask(int client_id, [FromBody] Models.Client client)
        {
            List<Models.Client> clients = db.Clients.ToList();
            Models.Client client_to_update = clients[client_id];
            client_to_update.FirstName = client.FirstName ?? client_to_update.FirstName;
            client_to_update.LastName = client.LastName ?? client_to_update.LastName;
            client_to_update.Password = client.Password ?? client_to_update.Password;
            client_to_update.Solde = client.Solde ?? client_to_update.Solde;
            return Ok(client_to_update);
        }
    }

}
