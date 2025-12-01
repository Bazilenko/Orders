using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Bll.DTOs.Restaurant;
using Catalog.Dal.Entities;

namespace Catalog.Bll.Mapper.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile() {
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<RestaurantCreateDto, Restaurant>();
            CreateMap<RestaurantUpdateDto, Restaurant>();
            CreateMap<RestaurantDto, Restaurant>();
        }
    }
}
