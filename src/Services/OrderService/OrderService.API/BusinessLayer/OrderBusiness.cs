﻿using AutoMapper;
using Azure;
using OrderService.API.DataLayer.DTOs;
using OrderService.API.DataLayer.Models;
using OrderService.API.Repository;

namespace OrderService.API.BusinessLayer
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderBusiness> _logger;
        protected ResponseModel _response;

        public OrderBusiness(IOrderRepository repository, IMapper mapper, ILogger<OrderBusiness> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            this._response = new ResponseModel();
        }

        public async Task<ResponseModel> AddOrderedItems(List<AddItemsDTO> addItems)
        {
            try
            {
                var mappedItems =  _mapper.Map<List<OrderedItems>>(addItems);
                _response = await _repository.AddOrderedItems(mappedItems);
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

        public async Task<ResponseModel> CreateOrder(AddOrderDTO addOrder)
        {
            try
            {
                var mappedOrder = _mapper.Map<Order>(addOrder);
                _response = await _repository.CreateOrder(mappedOrder);
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

        public async Task<ResponseModel> DeleteOrder(int id)
        {
            try
            {
                _response = await _repository.DeleteOrder(id);
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

        public Task<ResponseModel> GetOrdersbyItemCode(string itemcode)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> UpdateOrder(AddOrderDTO updateOrder)
        {
            try
            {
                var mappedOrder = _mapper.Map<Order>(updateOrder);
                _response = await _repository.UpdateOrder(mappedOrder);
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

        public Task<ResponseModel> UpdateOrderedItems(AddItemsDTO updateItems)
        {
            throw new NotImplementedException();
        }
    }
}
