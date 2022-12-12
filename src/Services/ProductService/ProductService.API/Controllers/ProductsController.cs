using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.API.BusinessLayer;
using ProductService.API.DataLayer.DTOs;

namespace ProductService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductBusiness _productBusiness;
        public ProductsController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness ?? throw new ArgumentNullException(nameof(productBusiness));
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddCategory(AddCategoryDTO addCategory)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.AddCategory(addCategory);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddProduct(AddProductDTO addProduct)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.AddProduct(addProduct);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddProductItems(List<AddProductItemsDTO> productItems, int productId)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.AddProductItems(productItems,productId);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.DeleteCategory(id);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.DeleteProduct(id);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllCategories()
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.GetAllCategories();
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllProducts()
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.GetAllProducts();
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetCategoriesById(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.GetCategoriesById(id);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.GetProductById(id);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetSelectedProduct(string category,string subCategory)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.GetSelectedProduct(category, subCategory);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UpdateCategory(AddCategoryDTO addCategory)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.UpdateCategory(addCategory);
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<IActionResult> UpdateProduct(AddProductDTO addProduct)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.UpdateProduct(addProduct);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetProductItemsByProductId(int productId)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.GetProductItemsById(productId);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetProductByProductCode(string productCode)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.GetProductByProductCode(productCode);
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetProductItemsByItemCode(string itemCode)
        {
            if (ModelState.IsValid)
            {
                var result = await _productBusiness.GetProductItemsByItemCode(itemCode);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
