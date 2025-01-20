using API.FurnitureStore.Data;
using API.FurnitureStore.Models;
using API.FurnitureStore.Models.Dtos.Product;
using API.FurnitureStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FurnitureStore.Services
{
    public class ProductsService : IProductsService
    {
        private readonly FugnitureStoreDbContext _context;

        public ProductsService(FugnitureStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryId(int categoryId)
        {
            var products = await _context.Products.Where(x => x.ProductCategoryId == categoryId).ToListAsync();
         
            return products;
        }

        public async Task<OperationResult> CreateProduct(CreateProductDto product)
        {
            try
            {
                var response = new OperationResult();

                var productCategoryExists = await _context.ProductCategories.FindAsync(product.ProductCategoryId);

                if (productCategoryExists == null)
                {
                    response.Success = false;
                    response.Message = "ProductCategory Not Found.";

                    return response;
                }

                var newProduct = new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    ProductCategoryId = product.ProductCategoryId,
                };

                _context.Products.Add(newProduct);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    response.Success = true;
                    response.Message = "Product successfully created.";
                    response.ResourceId = newProduct.Id;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Product could not be created.";
                }

                return response;
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "An unexpected error occurred during process." };
            }
        }

        public async Task<OperationResult> EditProduct(EditProductDto product)
        {
            try
            {
                var response = new OperationResult();

                var existingProduct = await _context.Products.FindAsync(product.Id);

                if (existingProduct == null)
                {
                    response.Success = false;
                    response.Message = "Product Not Found.";

                    return response;
                }

                var productCategoryExists = await _context.ProductCategories.FindAsync(product.ProductCategoryId);

                if (productCategoryExists == null)
                {
                    response.Success = false;
                    response.Message = "ProductCategory Not Found.";

                    return response;
                }

                existingProduct.Name = product.Name ?? existingProduct.Name;
                existingProduct.Price = product.Price != 0m ? product.Price : existingProduct.Price;
                existingProduct.ProductCategoryId = productCategoryExists.Id;

                _context.Products.Update(existingProduct);
                var result = await _context.SaveChangesAsync();

                if(result > 0)
                {
                    response.Success = true;
                    response.Message = "Product successfully updated.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Product could not be updated.";
                }

                return response;
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "An unexpected error occurred during process." };
            }
        }
        public async Task<OperationResult> DeleteProduct(int id)
        {
            try
            {
                var response = new OperationResult();
                var productExists = _context.Products.Find(id);

                if (productExists == null)
                {
                    response.Success = false;
                    response.Message = "Product Not Found.";

                    return response;
                }

                _context.Products.Remove(productExists);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    response.Success = true;
                    response.Message = "Product succesfully deleted.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Product could not be deleted.";
                }

                return response;
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "An unexpected error occurred during process." };
            }
        }
    }
}
