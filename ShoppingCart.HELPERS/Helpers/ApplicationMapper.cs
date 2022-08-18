using AutoMapper;
using ShoppingCart.DTO.DTOs;
using ShoppingCart.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.HELPERS.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<CorpSale, CorpSalesDto>().ReverseMap();
        }
    }
}
