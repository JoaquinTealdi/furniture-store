using API.FurnitureStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.FurnitureStore.Models.Dtos.ProductCategory;

namespace API.FurnitureStore.Services.Interfaces
{
    public interface IProductCategoriesService
    {
        Task<IEnumerable<ProductCategory>> GetProductCategories();
        Task<ProductCategory?> GetProductCategoryById(int id);
        Task<OperationResult> CreateProductCategory(CreateProductCategoryDto productCategory);
        Task<OperationResult> EditProductCategory(EditProductCategoryDto productCategory);
        Task<OperationResult> DeleteProductCategory(int id);
    }
}
