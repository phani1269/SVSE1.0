using OrderService.API.DataLayer.DTOs;
using OrderService.API.DataLayer.Models;

namespace OrderService.API.Repository
{
    public interface IOrderRepository
    {
        // Add Order
        Task<ResponseModel> CreateOrder(Order addOrder);
        // Add ordered Items for particular order and update the payable amount in order
        Task<ResponseModel> AddOrderedItems(List<OrderedItems> addItems);
        // update order
        Task<ResponseModel> UpdateOrder(Order updateOrder);
        // update orderedItems and if price update change the payable amount in order
        Task<ResponseModel> UpdateOrderedItems(OrderedItems updateItems);
        // Delete order  and ordered items
        Task<ResponseModel> DeleteOrder(int id);
        // get order by itemcode
        Task<ResponseModel> GetOrdersbyItemCode(string itemcode);
        // get all orders with ordered items
        Task<ResponseModel> GetAllOrders();
        // get orders by date 
        Task<ResponseModel> GetOrdersbyDate(string date);
        // get orders by customer name
        Task<ResponseModel> GetOrdersByCustomer(string customerName);




    }
}
