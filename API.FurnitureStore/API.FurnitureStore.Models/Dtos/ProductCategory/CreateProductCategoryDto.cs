using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FurnitureStore.Models.Dtos.ProductCategory
{
    public class CreateProductCategoryDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
