using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal.Entities;
using Orders.Bll.DTOs.OrderDish;

namespace Orders.Bll.Mapper.Profiles
{
    public class OrderDishProfile : Profile
    {
        public OrderDishProfile() {
            CreateMap<OrderDish, OrderDishDto>();
            CreateMap<OrderDishDto, OrderDish>();
        }
    }
}
