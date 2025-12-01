using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Bll.DTOs.Category;
using Catalog.Bll.Services.Interfaces;
using Catalog.Dal.Entities;
using Catalog.Dal.UOW.Interfaces;

namespace Catalog.Bll.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> Create(CategoryCreateDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            await _unitOfWork.Categories.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _unitOfWork.Categories.GetByIdAsync(id);
            if (entity == null)
                return false;
            await _unitOfWork.Categories.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var entity = await _unitOfWork.Categories.GetByIdAsync(id);
            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task Update(CategoryUpdateDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            _unitOfWork.Categories.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
