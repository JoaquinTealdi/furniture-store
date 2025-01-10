using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FurnitureStore.Models.Dtos.Product
{
    public class EditProductDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int ProductCategoryId { get; set; }
    }
}
