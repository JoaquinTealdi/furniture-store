using API.FurnitureStore.Models.Dtos.Product;
using API.FurnitureStore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.FurnitureStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _service;
        private readonly IProductCategoriesService _productCategoriesService;

        public ProductsController(IProductsService service, IProductCategoriesService productCategoriesService)
        {
            _service = service;
            _productCategoriesService = productCategoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var products = await _service.GetProducts();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("GetByCategoryId/{productCategoryId}")]
        public async Task<IActionResult> GetByCategoryId(int productCategoryId)
        {
            var categoryExists = await _productCategoriesService.GetProductCategoryById(productCategoryId);

            if (categoryExists == null)
            {
                return NotFound("Product Category does not exists");
            }

            var productsByCategory = await _service.GetProductByCategoryId(productCategoryId);

            return Ok(productsByCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.CreateProduct(product);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = result.ResourceId }, null);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.EditProduct(product);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }


            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteProduct(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
