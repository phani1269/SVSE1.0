﻿using AutoMapper;
using ProductService.API.DataLayer.DTOs;
using ProductService.API.DataLayer.Models;

namespace ProductService.API.MappingProfile
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<AddProductDTO, Products>().ReverseMap();
            CreateMap<AddCategoryDTO,CategoriesModel>().ReverseMap();
            CreateMap<GetCategoryDTO, CategoriesModel>().ReverseMap();
            CreateMap<GetProductDTO, Products>().ReverseMap();
        }
    }
}