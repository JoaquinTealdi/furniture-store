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

        public async Task<IEnumerable<OrderDto>> GetOrders()
        {
            return _context.Orders.Include(x => x.OrderDetails)
                                    .Select(x => new OrderDto
                                    {
                                        Id = x.Id,
                                        OrderDate = x.OrderDate,
                                        DeliveryDate = x.DeliveryDate,
                                        ClientId = x.ClientId,
                                        OrderNumber = x.OrderNumber,
                                        OrderDetailsRequest = x.OrderDetails.Select(od =>
                                            new OrderDetailRequest
                                            {
                                                ProductId = od.Product.Id,
                                                Quantity = od.Quantity
                                            }).ToList()
                                    }).ToList();
                
        }
        public async Task<OrderDto?> GetOrderById(int id)
        {
            return await _context.Orders.Include(x => x.OrderDetails)
                            .Where(x => x.Id == id).Select(
                             x=> new OrderDto
                             {
                                 Id = x.Id,
                                 OrderDate = x.OrderDate,
                                 DeliveryDate = x.DeliveryDate,
                                 ClientId = x.ClientId,
                                 OrderNumber = x.OrderNumber,
                                 OrderDetailsRequest = x.OrderDetails.Select(od =>
                                     new OrderDetailRequest
                                     {
                                         ProductId = od.Product.Id,
                                         Quantity = od.Quantity
                                     }).ToList()
                             }).FirstOrDefaultAsync();
        }


        public async Task<OperationResult> CreateOrder(CreateOrderDto order)
        {
            try
            {
                var response = new OperationResult();

                if (order == null)
                {
                    response.Success = false;
                    response.Message = "The order can not be null.";
                    return response;
                }

                if (order.OrderDetailsRequest == null || !order.OrderDetailsRequest.Any())
                {
                    response.Success = false;
                    response.Message = "The order must have at least one order detail.";
                    return response;
                }

                var client = await _context.Clients.FindAsync(order.ClientId);

                if (client == null)
                {
                    response.Success = false;
                    response.Message = "Client does not exists.";
                    return response;
                }

                var newOrder = new Order
                {
                    OrderNumber = order.OrderNumber,
                    ClientId = order.ClientId,
                    OrderDate = order.OrderDate,
                    DeliveryDate = order.DeliveryDate
                };

                foreach (var item in order.OrderDetailsRequest)
                {

                    var product = await _context.Products.FindAsync(item.ProductId);

                    if (product == null)
                    {
                        response.Success = false;
                        response.Message = "Product does not exists.";
                        return response;
                    }

                    var newOrderDetail = new OrderDetail
                    {
                        Product = product,
                        Quantity = item.Quantity,
                    };

                    newOrder.OrderDetails.Add(newOrderDetail);
                }

                await _context.Orders.AddAsync(newOrder);
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
            catch (Exception ex)
            {
                var response = new OperationResult();
                response.Success = false;
                response.Message = $"Error: {ex.Message}";

                return response;
            }

        }

        public async Task<OperationResult> EditOrder(OrderDto order)
        {
            try
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

                if (order.OrderDetailsRequest == null)
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

                existingOrder.OrderDetails.Clear();
                var odToDelete = await _context.OrderDetails.Where(x=> x.OrderId == existingOrder.Id).ToListAsync();
                _context.OrderDetails.RemoveRange(odToDelete);


                existingOrder.OrderNumber = order.OrderNumber;
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.DeliveryDate = order.DeliveryDate;
                existingOrder.ClientId = order.ClientId;


                foreach (var item in order.OrderDetailsRequest)
                {

                    var product = await _context.Products.FindAsync(item.ProductId);

                    if (product == null)
                    {
                        response.Success = false;
                        response.Message = "Product does not exists.";
                        return response;
                    }

                    var newOrderDetail = new OrderDetail
                    {
                        Product = product,
                        Quantity = item.Quantity,
                    };

                    existingOrder.OrderDetails.Add(newOrderDetail);
                }

                _context.Orders.Update(existingOrder);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    response.Success = true;
                    response.Message = "Order successfully updated.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Order could not be updated.";
                }

                return response;
            }
            catch (Exception ex)
            {
                var response = new OperationResult();
                response.Success = false;
                response.Message = $"Error: {ex.Message}";

                return response;
            }
        }

        public async Task<OperationResult> DeleteOrder(int id)
        {
            var response = new OperationResult();
            var existingOrder = await _context.Orders.Include(x=> x.OrderDetails).FirstOrDefaultAsync(x=> x.Id == id);

            if (existingOrder == null)
            {
                response.Success = false;
                response.Message = "Order does not exists.";
                return response;
            }

            //_context.OrderDetails.RemoveRange(existingOrder.OrderDetails);
            _context.Orders.Remove(existingOrder);

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
