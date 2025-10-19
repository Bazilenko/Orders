using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Bll.DTOs.Category;
using Catalog.Dal.Entities;

namespace Catalog.Bll.Mapper.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();   
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
