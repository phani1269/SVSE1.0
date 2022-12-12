using OrderService.API.DataLayer.DTOs;
using OrderService.API.DataLayer.Models;

namespace OrderService.API.BusinessLayer
{
    public interface IOrderBusiness
    {
        Task<ResponseModel> CreateOrder(AddOrderDTO addOrder);
        Task<ResponseModel> AddOrderedItems(List<AddItemsDTO> addItems);
        Task<ResponseModel> UpdateOrder(AddOrderDTO updateOrder);
        Task<ResponseModel> UpdateOrderedItems(AddItemsDTO updateItems);
        Task<ResponseModel> DeleteOrder(int id);
        Task<ResponseModel> GetOrdersbyItemCode(string itemcode);
        Task<ResponseModel> GetAllOrders();
        Task<ResponseModel> GetOrdersbyDate(string date);
        Task<ResponseModel> GetOrdersByCustomer(string customerName);
    }
}
