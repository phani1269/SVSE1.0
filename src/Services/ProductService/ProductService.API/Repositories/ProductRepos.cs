using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductService.API.DataLayer.Contexts;
using ProductService.API.DataLayer.DTOs;
using ProductService.API.DataLayer.Models;

namespace ProductService.API.Repositories
{
    
    public class ProductRepos : IProductRepos
    {
        private readonly ProductDbContext _context;
        private readonly ILogger<ProductRepos> _logger;
        protected ResponseModel _response;
        private readonly IMapper _mapper;

        public ProductRepos(ProductDbContext context, ILogger<ProductRepos> logger,IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._response = new ResponseModel();
            _mapper = mapper;
        }

        public async Task<ResponseModel> AddCategory(CategoriesModel category)
        {
            try
            {
                category.CreatedAt = DateTime.Now;
                category.ModifiedAt = DateTime.Now;
                var categoryModel = await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Record is saved into Database with Id:{categoryModel.Entity.CategoryId}");
                _response.Result = categoryModel.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        public async Task<ResponseModel> AddProduct(AddProductDTO product)
        {
            try
            {
                
                var newProduct = _mapper.Map<Products>(product);
                if (product.ItemCodes.Any())
                {
                    newProduct.Quantity = product.ItemCodes.Count;
                }
                else
                {
                    newProduct.Quantity = 0;
                }

                newProduct.CreatedAt = DateTime.Now;
                newProduct.ModifiedAt = DateTime.Now;
                var productModel = await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Record is saved into Database with Id:{productModel.Entity.ProductId}");

                if (product.ItemCodes.Any())
                {
                    foreach (var item in product.ItemCodes)
                    {
                        var productItem = new ProductItemsModel
                        {
                            ItemCode = item,
                            ProductId = productModel.Entity.ProductId
                        };
                       await _context.ProductItems.AddAsync(productItem);
                       await _context.SaveChangesAsync();
                    }
                }
                var itemsList = await _context.ProductItems.Where(i => i.ProductId == productModel.Entity.ProductId).ToListAsync();

                var result = _mapper.Map<GetProductDTO>(productModel.Entity);
                result.ProductItems = _mapper.Map<List<ProductItemDTO>>(itemsList);

                _response.Result = result;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> DeleteCategory(int id)
        {
            try
            {
                var categoryModel = await _context.Categories.FirstOrDefaultAsync(x=>x.CategoryId== id);
                if (categoryModel == null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = $"No Category is present with  Id:{id}";
                    return _response;
                }
                _context.Categories.Remove(categoryModel);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"record with id:{id} is deleted from categories");
                _response.DisplayMessage = $"Deleted the Record with Id:{id}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> DeleteProduct(int id)
        {
            try
            {
                var productModel = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
                if (productModel==null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = $"No Product is present with  Id:{id}";
                    return _response;
                }
                _context.Products.Remove(productModel);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"record with id:{id} is deleted from Products");
                _response.DisplayMessage = $"Deleted the Record with Id:{id}";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetAllCategories()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                if (categories==null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Categories are empty";
                    return _response;
                }
                _logger.LogInformation("All Categories from database are retrieved");
                _response.Result = categories;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetAllProducts()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                if (products==null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Products are empty";
                    return _response;
                }
                _logger.LogInformation("All Products from database are retrieved");
                _response.Result = products;

                /*foreach (var item in products)
                {
                    var newProduct = _mapper.Map<GetProductDTO>(item);
                    var productItems = await _context.ProductItems.Where(x => x.ProductId == item.ProductId).ToListAsync();
                    newProduct.ProductItems = _mapper.Map<List<GetProductItemsDTO>>(productItems);
                    getProducts.Add(newProduct);
                }
                _response.Result = getProducts;*/
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetCategoriesById(int id)
        {
            try
            {
                var categoryModel = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
                if (categoryModel==null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = $"No Category is Present with Id:{id}";
                    return _response;
                }
                _logger.LogInformation($"Category with id:{id} is retrieved");
                _response.Result = categoryModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetProductById(int id)
        {
            try
            {
                var productModel = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
                if (productModel==null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = $"No Product is Present with Id:{id}";
                    return _response;
                }
               // var categoryModel = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == productModel.CategoryId);
               // var productItems = await _context.ProductItems.Where(x => x.ProductId == productModel.ProductId).ToListAsync();
               /* var newProduct = new GetProductDTO
                {
                    CapacityRating = productModel.CapacityRating,
                    CategoryId = productModel.CategoryId,
                    CategoryName = categoryModel.CategoryName,
                    CostPrice = productModel.CostPrice,
                    ProductCode = productModel.ProductCode,
                    ProductId = productModel.ProductId,
                    ProductName = productModel.ProductName,
                    RetailPrice = productModel.RetailPrice,
                    SubCategory = categoryModel.SubCategory,
                    Warranty = productModel.Warranty,
                 //   ProductItems = _mapper.Map<List<GetProductItemsDTO>>(productItems)
            };*/
                _logger.LogInformation($"Product with id:{id} is retrieved");

                _response.Result = productModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetSelectedProduct(string category, string subcategory)
        {
            try
            {
                var productList = new List<GetProductDTO>();
                var categoryModel = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == category && x.SubCategory == subcategory);
                if (categoryModel==null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = $" {category} and {subcategory} are not present";
                    return _response;

                }
                var products = await _context.Products.Where(x=>x.CategoryId==categoryModel.CategoryId).ToListAsync();

                /*var products = await (from p in _context.Products
                                      join c in _context.Categories on p.Categories.CategoryId equals c.CategoryId
                                      where c.SubCategory == subcategory && c.CategoryName == category
                                      select new GetProductDTO
                                      {
                                          SubCategory = c.SubCategory,
                                          CategoryName = c.CategoryName,
                                          CategoryId = c.CategoryId,
                                          CapacityRating = p.CapacityRating,
                                          CostPrice = p.CostPrice,
                                          ProductCode = p.ProductCode,
                                          ProductId = p.ProductId,
                                          ProductName = p.ProductName,
                                          RetailPrice = p.RetailPrice,
                                          Warranty = p.Warranty
                                      }).ToListAsync();*/

                if (!products.Any())
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = $"No products are there under {category} and {subcategory}";
                    return _response;

                }

                _logger.LogInformation($"Products with {category} and {subcategory} are retrieved ");
                _response.Result = products;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        public async Task<ResponseModel> UpdateCategory(CategoriesModel category)
        {
            try
            {
                var categoryModel = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == category.CategoryId);
                if (categoryModel == null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = $"No Category is present with Id:{category.CategoryId}";
                    return _response;
                }
                var newCategory = new CategoriesModel
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    SubCategory = category.SubCategory,
                    ModifiedBy= category.ModifiedBy,
                    ModifiedAt = DateTime.Now
                };
                _context.Categories.Update(newCategory);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Category with Id:{category.CategoryId} is updated");
                _response.DisplayMessage = $"Category with Id:{category.CategoryId} is updated";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> UpdateProduct(Products product)
        {
            try
            {
                var productModel = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == product.ProductId);
                if (productModel==null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = $"No Product is present with Id:{product.ProductId}";
                    return _response;
                }
                var newProduct = new Products
                {
                    ProductId = product.ProductId,
                    CapacityRating = product.CapacityRating,
                    CostPrice = product.CostPrice,
                    ProductCode = product.ProductCode,
                    ProductName = product.ProductName,
                    RetailPrice = product.RetailPrice,
                    Warranty = product.Warranty,
                    ModifiedAt = DateTime.Now,
                    ModifiedBy = product.ModifiedBy
                };
                _context.Products.Update(newProduct);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Product with Id:{product.ProductId} is updated");
                _response.DisplayMessage = $"Product with Id:{product.ProductId} is updated";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> AddProductItems(List<AddProductItemsDTO> productItems, int productId)
        {
            try
            {
                var productExists = _context.Products.Where(x => x.ProductId == productId).Any();
                if (productExists)
                {
                    foreach (var item in productItems)
                    {
                        var newItem = new ProductItemsModel
                        {
                            ProductId = productId,
                            ItemCode = item.ItemCode
                        };
                        await _context.ProductItems.AddAsync(newItem);
                        await _context.SaveChangesAsync();
                    }
                    var itemsList = await _context.ProductItems.Where(x => x.ProductId == productId).ToListAsync();

                    var product = await _context.Products.Where(x => x.ProductId == productId).ToListAsync();
                    foreach (var item in product)
                    {
                        item.Quantity = itemsList.Count;
                    }
                    await _context.SaveChangesAsync();
                    _response.Result = true; 
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = $"No Product is present with Id:{productId}";
                    return _response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetProductItemsById(int productId)
        {
            try
            {
                var productItems = await _context.ProductItems.Where(x => x.ProductId == productId).ToListAsync();
                _response.Result = productItems;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetProductByProductCode(string productCode)
        {
            try
            {
                var productResponse = await _context.Products.FirstOrDefaultAsync(x => x.ProductCode == productCode);
                _response.Result = productResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetProductItemsByItemCode(string itemCode)
        {
            try
            {
                _response.Result = await _context.ProductItems.FirstOrDefaultAsync(x => x.ItemCode == itemCode);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
