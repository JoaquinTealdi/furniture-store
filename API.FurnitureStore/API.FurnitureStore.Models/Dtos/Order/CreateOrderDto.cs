using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FurnitureStore.Models.Dtos.Order
{
    public class CreateOrderDto
    {
        public int OrderNumber { get; set; }
        public int ClientId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<OrderDetailRequest> OrderDetailsRequest { get; set; }
    }


    public class OrderDetailRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }


}
