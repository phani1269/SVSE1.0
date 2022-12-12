using AutoMapper;
using ProductService.API.DataLayer.DTOs;
using ProductService.API.DataLayer.Models;
using ProductService.API.Repositories;

namespace ProductService.API.BusinessLayer
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepos _productRepos;
        private readonly IMapper _mapper;
        protected ResponseModel _response;
        private readonly ILogger<ProductBusiness> _logger;

        public ProductBusiness(IProductRepos productRepos, IMapper mapper,ILogger<ProductBusiness> logger)
        {
            _productRepos = productRepos ?? throw new ArgumentNullException(nameof(productRepos));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._response= new ResponseModel();
            _logger = logger;
        }

        public async Task<ResponseModel> AddCategory(AddCategoryDTO category)
        {
            try
            {
                var categoryModel = _mapper.Map<CategoriesModel>(category);
                _response = await _productRepos.AddCategory(categoryModel);
                _response.Result = _mapper.Map<GetCategoryDTO>(_response.Result);
                _logger.LogInformation("Mapping is done");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> AddProduct(AddProductDTO product)
        {
            try
            {
              //  var newProductModel = _mapper.Map<Products>(product);
                _response = await _productRepos.AddProduct(product);
               // _response.Result = _mapper.Map<GetProductDTO>(_response.Result);
               // _logger.LogInformation("Mapping is done");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> DeleteCategory(int id)
        {
            try
            {
                _response = await _productRepos.DeleteCategory(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> DeleteProduct(int id)
        {
            try
            {
                _response = await _productRepos.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetAllCategories()
        {
            try
            {
                _response = await _productRepos.GetAllCategories();
                _response.Result = _mapper.Map<IEnumerable<GetCategoryDTO>>(_response.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        public async Task<ResponseModel> GetAllProducts()
        {
            try
            {
                _response = await _productRepos.GetAllProducts();

                var result = _mapper.Map<List<GetProductDTO>>(_response.Result);
                foreach (var item in result)
                {
                    var prodItems = GetProductItemsById(item.ProductId);
                    item.ProductItems = _mapper.Map<List<ProductItemDTO>>(prodItems.Result);
                }
                _response.Result = result;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetCategoriesById(int id)
        {
            try
            {
                _response = await _productRepos.GetCategoriesById(id);
                _response.Result = _mapper.Map<GetCategoryDTO>(_response.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        public async Task<ResponseModel> GetProductById(int id)
        {
            try
            {
                _response = await _productRepos.GetProductById(id);
                var newProduct = _mapper.Map<GetProductDTO>(_response.Result);
                var categories =  await GetCategoriesById(newProduct.CategoryId);
                var categoryModel = _mapper.Map<GetCategoryDTO>(categories.Result);
                newProduct.CategoryName = categoryModel.CategoryName;
                newProduct.SubCategory = categoryModel.SubCategory;

                var prodItems = GetProductItemsById(id);
                var productItems = _mapper.Map<List<ProductItemDTO>>(prodItems.Result);

                newProduct.ProductItems = productItems;

                _response.Result = newProduct;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetSelectedProduct(string category, string subcategory)
        {
            try
            {
                _response = await _productRepos.GetSelectedProduct(category, subcategory);

                var productsResponse = _mapper.Map<List<Products>>(_response.Result);

                var result = new GetProductsByCatandSubDTO();
                var productList = new ProductsList();

                foreach (var item in productsResponse)
                {
                    var categoryies = await GetCategoriesById(item.CategoryId);
                    var categoryModel = _mapper.Map<GetCategoryDTO>(categoryies.Result);

                    result.CategoryId = categoryModel.CategoryId;
                    result.CategoryName = categoryModel.CategoryName;
                    result.SubCategory = categoryModel.SubCategory;

                    productList.ProductId = item.ProductId;
                    productList.ProductName = item.ProductName;
                    productList.ProductCode = item.ProductCode;
                    productList.CapacityRating = item.CapacityRating;
                    productList.CostPrice = item.CostPrice;
                    productList.RetailPrice = item.RetailPrice;
                    productList.Warranty = item.Warranty;

                    var newitems = await GetProductItemsById(item.ProductId);
                    var productItemsModel = _mapper.Map<List<ProductItemDTO>>(newitems.Result);

                    productList.ProductItems = productItemsModel;


                    result.ProductsList.Add(productList);
                }
                _response.Result = result;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> UpdateCategory(UpdateCategoryDTO category,int id)
        {
            try
            {
               _response = await _productRepos.UpdateCategory(_mapper.Map<CategoriesModel>(category),id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> UpdateProduct(UpdateProductDTO product,int id)
        {
            try
            {
                _response = await _productRepos.UpdateProduct(_mapper.Map<Products>(product),id);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> AddProductItems(List<AddProductItemsDTO> productItems, int productId)
        {
            try
            {
                _response = await _productRepos.AddProductItems(productItems, productId);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetProductItemsById(int productId)
        {
            try
            {
                _response = await _productRepos.GetProductItemsById(productId);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> GetProductByProductCode(string productCode)
        {
            try
            {
                _response =  await _productRepos.GetProductByProductCode(productCode);
                var product = _mapper.Map<GetProductDTO>(_response.Result);

                var categories = await GetCategoriesById(product.CategoryId);
                var catgeoryModel = _mapper.Map<GetCategoryDTO>(categories.Result);
                product.CategoryName = catgeoryModel.CategoryName;
                product.SubCategory = catgeoryModel.SubCategory;

                var productresposne = await GetProductItemsById(product.ProductId);
                var productModel = _mapper.Map<List<ProductItemDTO>>(productresposne.Result);

                product.ProductItems = productModel;

                _response.Result = product;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
 
        }

        public async Task<ResponseModel> GetProductItemsByItemCode(string itemCode)
        {
            try
            {
                var productItemresponse = await _productRepos.GetProductItemsByItemCode(itemCode);
                var productItem = _mapper.Map<GetProductItemDTO>(productItemresponse.Result);

                var productresponse = await _productRepos.GetProductById(productItem.ProductId);
                var existProduct = _mapper.Map<ProductDTO>(productresponse.Result);

                ItemDTO itemDTO = new ItemDTO
                {
                    product = existProduct,
                    ProductItem = productItem
                };
                _response.Result = itemDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        public async Task<ResponseModel> DeleteProductItems(List<AddProductItemsDTO> itemsList)
        {
            try
            {
                _response = await _productRepos.DeleteProductItems(itemsList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught : {ex.Message}");
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error";
                _response.Result = null;
                if (_response.ErrorMessages != null)
                {
                    _response.ErrorMessages.Add(ex.ToString());
                }
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
