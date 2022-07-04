using AutoMapper;
using ShoppingCart.DTOs;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Helpers
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper()
        {
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
            CreateMap<CorpSale, CorpSalesDto>().ReverseMap();
        }

    }
}
