using API.FurnitureStore.Data;
using API.FurnitureStore.Models;
using API.FurnitureStore.Models.Dtos;
using API.FurnitureStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.FurnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientsService _service;

        public ClientsController(IClientsService service)
        {
            _service = service;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetClients();
            return Ok(result);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _service.GetClientById(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateClientDto client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateClient(client);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Created(result.Message, null);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(EditClientDto client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.EditClient(client);

            if (!result.Success)
            {
                return BadRequest($"{result.Message}");
            }

            return NoContent();
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteClient(id);

            if (!result.Success)
            {
                return BadRequest($"{result.Message}");
            }

            return Ok($"{result.Message}");
        }
    }
}
