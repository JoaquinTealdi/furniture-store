using API.FurnitureStore.Data;
using API.FurnitureStore.Models;
using API.FurnitureStore.Models.Dtos.ProductCategory;
using API.FurnitureStore.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.FurnitureStore.Services
{
    public class ProductCategoriesService : IProductCategoriesService
    {
        private readonly FugnitureStoreDbContext _context;

        public ProductCategoriesService(FugnitureStoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductCategory>> GetProductCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory?> GetProductCategoryById(int id)
        {
            return await _context.ProductCategories.FindAsync(id);
        }

        public async Task<OperationResult> CreateProductCategory(CreateProductCategoryDto productCategory)
        {
            try
            {
                var response = new OperationResult();

                var newProductCategory = new ProductCategory
                {
                    Name = productCategory.Name
                };

                _context.ProductCategories.Add(newProductCategory);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    response.Success = true;
                    response.Message = "ProductCategory successfully created.";
                    response.ResourceId = newProductCategory.Id;
                }
                else
                {
                    response.Success = false;
                    response.Message = "ProductCategory could not be created.";

                }

                return response;
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "An unexpected error occurred during process." };
            }
        }
        public async Task<OperationResult> EditProductCategory(EditProductCategoryDto productCategory)
        {
            try
            {
                var response = new OperationResult();

                var productCategoryExists = _context.ProductCategories.Find(productCategory.Id);

                if (productCategoryExists == null)
                {
                    response.Success = false;
                    response.Message = "Product Category Not Found.";

                    return response;
                }

                productCategoryExists.Name = productCategory.Name ?? productCategoryExists.Name;

                _context.ProductCategories.Update(productCategoryExists);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    response.Success = true;
                    response.Message = "ProductCategory successfully updated.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "ProductCategory could not be updated.";
                }

                return response;
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "An unexpected error occurred during process." };
            }
        }

        public async Task<OperationResult> DeleteProductCategory(int id)
        {
            try
            {
                var response = new OperationResult();

                var productCategoryExists = _context.ProductCategories.Find(id);

                if (productCategoryExists == null)
                {
                    response.Success = false;
                    response.Message = "Product Category Not Found.";

                    return response;
                }

                _context.ProductCategories.Remove(productCategoryExists);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    response.Success = true;
                    response.Message = "Product Category succesfully deleted.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Product Category could not be deleted.";
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
