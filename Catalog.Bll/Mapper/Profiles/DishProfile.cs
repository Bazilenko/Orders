using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Bll.DTOs.Dish;
using Catalog.Dal.Entities;

namespace Catalog.Bll.Mapper.Profiles
{
    public class DishProfile : Profile
    {
        public DishProfile() {
            CreateMap<DishCreateDto, Dish>();
            CreateMap<DishUpdateDto, Dish>();
            CreateMap<DishDto,  Dish>();
            CreateMap<Dish, DishDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        }

    }
}
