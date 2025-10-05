using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal.DTOs.Order;
using Dal.Entities;
using Orders.Bll.DTOs.Order;

namespace Orders.Bll.Mapper.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderUpdateDto, Order>();
            CreateMap<OrderDto, OrderReceiptDto>();
            CreateMap<Order, OrderReceiptDto>();
        }


    }
}
