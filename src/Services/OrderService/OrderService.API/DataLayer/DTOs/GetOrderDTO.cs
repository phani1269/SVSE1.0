﻿using OrderService.API.DataLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace OrderService.API.DataLayer.DTOs
{
    public class GetOrderDTO : EntityBase
    {
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal PayableAmount { get; set; }
        public bool PaymentConfirmation { get; set; } 
        public List<GetItemsDTO> OrderdItems { get; set; }
    }
}
