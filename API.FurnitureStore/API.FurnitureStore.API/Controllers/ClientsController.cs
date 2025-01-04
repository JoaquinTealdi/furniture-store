using API.FurnitureStore.Data;
using API.FurnitureStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FurnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly FugnitureStoreDbContext _context;

        public ClientsController(FugnitureStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("Get")]
        public async Task<IEnumerable<Client>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Create", client.Id, client);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> EditClient(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = _context.Clients.FirstOrDefault(x => x.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
