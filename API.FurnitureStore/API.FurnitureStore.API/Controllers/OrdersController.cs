using API.FurnitureStore.Data;
using API.FurnitureStore.Models;
using API.FurnitureStore.Models.Dtos.Order;
using API.FurnitureStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.WebSockets;

namespace API.FurnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _service;
        public OrdersController(IOrdersService service)
        {
            _service = service; 
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _service.GetOrders();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _service.GetOrderById(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto order)
        {
                
            var result = await _service.CreateOrder(order);

            if (!result.Success) { 
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.ResourceId }, null);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(OrderDto order)
        {
            if (order.Id < 0)
            {   
                return BadRequest("Invalid id");
            }

            var result = await _service.EditOrder(order);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid id");
            }

            var result = await _service.DeleteOrder(id);

            if(!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
