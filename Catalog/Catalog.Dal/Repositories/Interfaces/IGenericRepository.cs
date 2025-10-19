using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Entities;

namespace Catalog.Dal.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity, CancellationToken ct = default);
        Task UpdateAsync(T entity, CancellationToken ct = default);
        Task DeleteAsync(T entity, CancellationToken ct = default);
    }
}
