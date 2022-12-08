using ProductService.API.DataLayer.DTOs;
using ProductService.API.DataLayer.Models;

namespace ProductService.API.BusinessLayer
{
    public interface IProductBusiness
    {
        Task<ResponseModel> GetAllProducts();
        Task<ResponseModel>  GetAllCategories();
        Task<ResponseModel> GetCategoriesById(int id);
        Task<ResponseModel> GetSelectedProduct(string category, string subcategory);
        Task<ResponseModel> GetProductById(int id);
        Task<ResponseModel> AddProduct(AddProductDTO product);
        Task<ResponseModel> AddCategory(AddCategoryDTO category);
        Task<ResponseModel> UpdateProduct(AddProductDTO product);
        Task<ResponseModel> UpdateCategory(AddCategoryDTO category);
        Task<ResponseModel> DeleteProduct(int id);
        Task<ResponseModel> DeleteCategory(int id);
    }
}
