using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Bll.DTOs.Category;
using Catalog.Dal.Entities;

namespace Catalog.Bll.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAll();
        Task<CategoryDto> GetById(int id);
        Task Update(CategoryUpdateDto dto);
        Task<CategoryDto> Create(CategoryCreateDto dto);
        Task<bool> Delete(int id);

    }
}
