using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FurnitureStore.Models.Dtos.Order
{
    public class EditOrderDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OrderNumber { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        public List<OrderDetailRequest> OrderDetails { get; set; }
    }
}
