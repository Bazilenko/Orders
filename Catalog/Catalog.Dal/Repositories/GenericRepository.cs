using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Dal.Context;
using Catalog.Dal.Entities;
using Catalog.Dal.Repositories.Interfaces;
using Catalog.Dal.Specifications.Interfaces;
using Catalog.Dal.Specifications;

namespace Catalog.Dal.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly MyDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(MyDbContext dbContext) {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        
        public async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken ct = default)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _dbSet.FindAsync(id);
            
        }

        public async Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T?> GetEntityWithSpecification(ISpecification<T> specification)
        {
            var query = SpecificationEvaluator<T>.GetQuery(_dbSet, specification);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ListAsync(ISpecification<T> specification)
        {
            var query = SpecificationEvaluator<T>.GetQuery(_dbSet, specification);
            return await query.ToListAsync();
        }
    }
}
