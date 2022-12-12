using ProductService.API.DataLayer.DTOs;
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
        Task<ResponseModel> GetProductItemsById(int productId);
        Task<ResponseModel> GetProductByProductCode(string productCode);
        Task<ResponseModel> GetProductItemsByItemCode(string itemCode);
        Task<ResponseModel> AddProduct(AddProductDTO product);
        Task<ResponseModel> AddProductItems(List<AddProductItemsDTO> productItems,int productId);
        Task<ResponseModel> AddCategory(CategoriesModel category);
        Task<ResponseModel> UpdateProduct(Products product, int id  );
        Task<ResponseModel> UpdateCategory(CategoriesModel category, int id );
        Task<ResponseModel> DeleteProduct(int id);
        Task<ResponseModel> DeleteCategory(int id);
        Task<ResponseModel> DeleteProductItems(List<AddProductItemsDTO> itemsList);

  


    }
}
