using AutoMapper;
using OrderService.API.DataLayer.DTOs;
using OrderService.API.DataLayer.Models;

namespace OrderService.API.MappingProfile
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<AddOrderDTO, Order>().ReverseMap();
            CreateMap<AddItemsDTO,OrderedItems>().ReverseMap();
            CreateMap<GetOrderDTO, Order>().ReverseMap();
            CreateMap<GetItemsDTO, OrderedItems>().ReverseMap();
        }
    }
}
