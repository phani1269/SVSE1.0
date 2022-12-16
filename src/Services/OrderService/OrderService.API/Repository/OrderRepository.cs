﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderService.API.DataLayer.Contexts;
using OrderService.API.DataLayer.DTOs;
using OrderService.API.DataLayer.Models;
using System.Linq;

namespace OrderService.API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;
        private readonly ILogger<OrderRepository> _logger;
        protected ResponseModel _response;
        private readonly IMapper _mapper;

        public OrderRepository(OrderDbContext context, ILogger<OrderRepository> logger,IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            this._response = new ResponseModel();
        }

        public async Task<ResponseModel> AddOrderedItems(List<OrderedItems> addItems)
        {
            decimal total = 0;
            int orderId = 0;
            try
            {
                foreach (var item in addItems)
                {
                    orderId = item.OrderId;
                    bool orderExists = await _context.orders.AnyAsync(o => o.OrderId == orderId);
                    if (!orderExists)
                    {
                        _response.IsSuccess = false;
                        _response.DisplayMessage = $"Order with OrderId:{orderId} is not present";
                        return _response;
                    }

                    var newOrder = await _context.orderedItems.AddAsync(item);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Order items are added with itemId:{newOrder.Entity.ItemsId} for orderId:{orderId}");
                    total += item.TotalAmount;
                }

                var orders = await _context.orders.Where(x => x.OrderId == orderId).ToListAsync();

                foreach (var item in orders)
                {
                    item.PayableAmount = total;
                    item.PaymentConfirmation = true;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"payable amount is updated for orderId:{orderId}");
                }

                _response.IsSuccess = true;
                _response.DisplayMessage = "Items are inserted into database";
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

        public async Task<ResponseModel> CreateOrder(Order addOrder)
        {
            try
            {
                await _context.orders.AddAsync(addOrder);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Order has created with orderId {addOrder.OrderId}");

                _response.IsSuccess = true;
                _response.DisplayMessage = $"Order has created with orderId:{addOrder.OrderId}";
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

        public async Task<ResponseModel> DeleteOrder(int id)
        {
            try
            {
                bool orderExists = await _context.orders.AnyAsync(o => o.OrderId == id);
                if (!orderExists)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = $"Order with OrderId:{id} is not present";
                    return _response;
                }
                var orderList = await _context.orderedItems.Where(x => x.OrderId == id).ToListAsync();
                _context.orderedItems.RemoveRange(orderList);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"OrderItems with OrderId :{id} are removed");

                var order = await _context.orders.FirstOrDefaultAsync(x => x.OrderId == id);
                _context.orders.Remove(order);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Order with orderId {id} has removed");

                _response.IsSuccess = true;
                _response.DisplayMessage = $"Order with orderId {id} has removed";

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

        public Task<ResponseModel> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> GetOrdersByCustomer(string customerName)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> GetOrdersbyDate(string date)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> GetOrdersbyItemCode(string itemcode)
        {
            try
            {
                var existItems = await _context.orderedItems.Where(x => x.Itemcode == itemcode).ToListAsync();

                _response.Result = existItems;
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

        public async Task<ResponseModel> UpdateOrder(Order updateOrder)
        {
            try
            {
                var ExistOrder = await _context.orders.FirstOrDefaultAsync(o => o.OrderId == updateOrder.OrderId);

                if (ExistOrder == null)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = $"Order with OrderId:{updateOrder.OrderId} is not present";
                    return _response;
                }

                ExistOrder.Address = updateOrder.Address;
                ExistOrder.PhoneNumber = updateOrder.PhoneNumber;
                ExistOrder.GSTNumber = updateOrder.GSTNumber;
                ExistOrder.VehicleNumber = updateOrder.VehicleNumber;
                ExistOrder.CustomerName = updateOrder.CustomerName;
                ExistOrder.PaymentConfirmation = updateOrder.PaymentConfirmation;
                ExistOrder.ModifiedAt = DateTime.Now;
                ExistOrder.ModifiedBy = updateOrder.ModifiedBy;
                ExistOrder.PayableAmount = updateOrder.PayableAmount;

                await _context.SaveChangesAsync();
                _response.IsSuccess = true;
                _response.DisplayMessage = $"Order with Id:{updateOrder.OrderId} is updated";
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

        public async Task<ResponseModel> UpdateOrderedItems(OrderedItems updateItems)
        {
            try
            {
                var orderExist = await _context.orders.AnyAsync(x => x.OrderId == updateItems.OrderId);
                if (!orderExist)
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = $"Order with OrderId:{updateItems.OrderId} is not present";
                    return _response;
                }

                var ExistOrderItem = await _context.orderedItems.FirstOrDefaultAsync(x => x.ItemsId == updateItems.ItemsId);

                ExistOrderItem.OrderedQuantity = updateItems.OrderedQuantity;
                ExistOrderItem.SGST = updateItems.SGST;
                ExistOrderItem.CGST = updateItems.CGST;
                ExistOrderItem.ItemAmount = updateItems.ItemAmount;
                ExistOrderItem.TotalAmount = updateItems.TotalAmount;
                ExistOrderItem.Itemcode = updateItems.Itemcode;
                ExistOrderItem.ModifiedBy = updateItems.ModifiedBy;
                ExistOrderItem.ModifiedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                _response.IsSuccess = true;
                _response.DisplayMessage = $"Item with Id:{updateItems.ItemsId} is updated";


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

        public async Task<ResponseModel> GetOrderByOrderId(int id)
        {
            try
            {
                _response.Result = await _context.orders.FirstOrDefaultAsync(x => x.OrderId == id);

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
