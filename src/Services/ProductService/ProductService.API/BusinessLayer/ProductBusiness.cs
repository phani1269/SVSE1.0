using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductService.API.DataLayer.DTOs;
using ProductService.API.DataLayer.Models;
using ProductService.API.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;

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
                var newProductModel = _mapper.Map<Products>(product);
                _response = await _productRepos.AddProduct(newProductModel);
                _response.Result = _mapper.Map<GetProductDTO>(_response.Result);
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
                _response.Result = _mapper.Map<IEnumerable<GetProductDTO>>(_response.Result);

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

        public async Task<ResponseModel> UpdateCategory(AddCategoryDTO category)
        {
            try
            {
               _response = await _productRepos.UpdateCategory(_mapper.Map<CategoriesModel>(category));
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

        public async Task<ResponseModel> UpdateProduct(AddProductDTO product)
        {
            try
            {
                _response = await _productRepos.UpdateProduct(_mapper.Map<Products>(product));

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
