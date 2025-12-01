using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Bll.DTOs.Dish;
using Catalog.Bll.Services.Interfaces;
using Catalog.Dal.Entities;
using Catalog.Dal.UOW.Interfaces;

namespace Catalog.Bll.Services
{
    public class DishService : IDishService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DishService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DishDto> AddDish(DishCreateDto dto)
        {
            var dish = _mapper.Map<Dish>(dto);
            await _unitOfWork.Dishes.AddAsync(dish);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<DishDto>(dish);
        }

        public async Task<IEnumerable<DishDto?>> GetAllDishesAsync()
        {
            var dishes = await _unitOfWork.Dishes.GetAllAsync();
            return _mapper.Map<IEnumerable<DishDto>>(dishes);
        }

        public async Task<DishDto?> GetDishByIdAsync(int id)
        {
            var dish = await _unitOfWork.Dishes.GetByIdAsync(id);
            return _mapper.Map<DishDto>(dish);
        }

        public async Task UpdateDish(DishUpdateDto dto)
        {
            var entity = _mapper.Map<Dish>(dto);
            await _unitOfWork.Dishes.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
