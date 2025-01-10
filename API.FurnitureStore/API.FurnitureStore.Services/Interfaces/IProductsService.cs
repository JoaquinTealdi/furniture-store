using API.FurnitureStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.FurnitureStore.Models.Dtos.Product;

namespace API.FurnitureStore.Services.Interfaces
{
    public interface IProductsService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product?> GetProductById(int id);
        Task<OperationResult> CreateProduct(CreateProductDto product);
        Task<OperationResult> EditProduct(EditProductDto product);
        Task<OperationResult> DeleteProduct(int id);
    }
}
