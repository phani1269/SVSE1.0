using ProductService.API.DataLayer.Models;

namespace ProductService.API.Repositories
{
    public interface IProductRepos
    {
        Task<ResponseModel> GetAllProducts();
        Task<ResponseModel> GetAllCategories();
        Task<ResponseModel> GetCategoriesById(int id);
        Task<ResponseModel> GetSelectedProduct(string category, string subcategory);
        Task<ResponseModel> GetProductById(int id);
        Task<ResponseModel> AddProduct(Products product);
        Task<ResponseModel> AddCategory(CategoriesModel category);
        Task<ResponseModel> UpdateProduct(Products product);
        Task<ResponseModel> UpdateCategory(CategoriesModel category);
        Task<ResponseModel> DeleteProduct(int id);
        Task<ResponseModel> DeleteCategory(int id);

    }
}
