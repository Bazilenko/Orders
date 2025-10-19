using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Bll.DTOs.Dish;
using Catalog.Dal.Entities;
using Catalog.Dal.UOW.Interfaces;

namespace Catalog.Bll.Services.Interfaces
{
    public interface IDishService
    {
        Task<IEnumerable<DishDto?>> GetAllDishesAsync();
        Task<DishDto?> GetDishByIdAsync(int id);
        Task<DishDto> AddDish(DishCreateDto dish);
        Task UpdateDish(DishUpdateDto dto);
    }
}
