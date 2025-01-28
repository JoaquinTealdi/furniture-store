using API.FurnitureStore.Models.Dtos.Product;
using API.FurnitureStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.FurnitureStore.Models.Dtos.Order;

namespace API.FurnitureStore.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<IEnumerable<OrderDto>> GetOrders();
        Task<OrderDto?> GetOrderById(int id);
        Task<OperationResult> CreateOrder(CreateOrderDto order);
        Task<OperationResult> EditOrder(OrderDto order);
        Task<OperationResult> DeleteOrder(int id);
    }
}
