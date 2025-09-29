using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repository.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<int> AddRangeAsync(IEnumerable<T> items);
        Task ReplaceAsync(T t);
        Task<int> AddAsync(T t);

    }
}
