using API.FurnitureStore.Data;
using API.FurnitureStore.Models;
using API.FurnitureStore.Models.Dtos.Order;
using API.FurnitureStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FurnitureStore.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly FugnitureStoreDbContext _context;

        public OrdersService(FugnitureStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders.Include(x => x.OrderDetails).ToListAsync();
        }
        public async Task<Order?> GetOrderById(int id)
        {
            return await _context.Orders.Include(x => x.OrderDetails)
                            .Where(x => x.Id == id).FirstOrDefaultAsync();
        }


        public async Task<OperationResult> CreateOrder(CreateOrderDto order)
        {
            var response = new OperationResult();
            var client = await _context.Clients.FindAsync(order.ClientId);

            if (order.OrderDetails == null)
            {
                response.Success = false;
                response.Message = "The order must have at least one order detail";
                return response;
            }

            if (client == null)
            {
                response.Success = false;
                response.Message = "Client does not exists";
                return response;

            }

            var newOrder = new Order
            {
                ClientId = client.Id,
                OrderDate = order.OrderDate,
                DeliveryDate = order.DeliveryDate,
                OrderNumber = order.OrderNumber,
            };

            _context.Orders.AddAsync(newOrder);
            _context.OrderDetails.AddRangeAsync(order.OrderDetails);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                response.Success = true;
                response.Message = "Order successfully created.";
                response.ResourceId = newOrder.Id;
            }
            else
            {
                response.Success = false;
                response.Message = "Order could not be created.";
            }

            return response;
        }

        public async Task<OperationResult> EditOrder(EditOrderDto order)
        {
            var response = new OperationResult();
            var existingOrder = await _context.Orders.FindAsync(order.Id);
            var client = await _context.Clients.FindAsync(order.ClientId);

            if (existingOrder == null)
            {
                response.Success = false;
                response.Message = "Order does not exists.";
                return response;
            }

            if (order.OrderDetails == null)
            {
                response.Success = false;
                response.Message = "The order must have at least one order detail.";
                return response;
            }

            if (client == null)
            {
                response.Success = false;
                response.Message = "Client does not exists.";
                return response;

            }

            existingOrder.OrderNumber = order.OrderNumber;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.DeliveryDate = order.DeliveryDate;
            existingOrder.ClientId = order.ClientId;

            _context.OrderDetails.RemoveRange(existingOrder.OrderDetails);

            _context.Orders.Update(existingOrder);

            _context.OrderDetails.AddRangeAsync(order.OrderDetails);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                response.Success = true;
                response.Message = "Order successfully updated.";
                response.ResourceId = existingOrder.Id;
            }
            else
            {
                response.Success = false;
                response.Message = "Order could not be updated.";
            }

            return response;
        }

        public async Task<OperationResult> DeleteOrder(int id)
        {
            var response = new OperationResult();
            var orderExists = await _context.Orders.FindAsync(id);

            if (orderExists == null)
            {
                response.Success = false;
                response.Message = "Order does not exists.";
                return response;
            }

            _context.OrderDetails.RemoveRange(orderExists.OrderDetails);
            _context.Orders.Remove(orderExists);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                response.Success = true;
                response.Message = "Order successfully deleted.";
            }
            else
            {
                response.Success = false;
                response.Message = "Order could not be deleted.";
            }

            return response;
        }


    }
}
