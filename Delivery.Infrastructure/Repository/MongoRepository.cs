using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Delivery.Domain.Common;
using Delivery.Domain.Interfaces.Repositories;
using Delivery.Infrastructure.Mongo;
using MongoDB.Driver;
using MongoDB.Bson;
namespace Delivery.Infrastructure.Repository
{
    public class MongoRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> _collection;
        protected readonly IClientSessionHandle? _session;

        public MongoRepository(MongoDbContext context , IClientSessionHandle? session = null)
        {
            _collection = typeof(T).Name switch
            {
                "Courier" => (IMongoCollection<T>)context.Couriers,
                "Delivery" => (IMongoCollection<T>)context.Deliveries,
                _ => context.Database.GetCollection<T>(typeof(T).Name + "s")
            };
            _session = session;
        }

        public async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            if (_session != null)
                await _collection.InsertOneAsync(_session, entity, cancellationToken: ct);
            else
                await _collection.InsertOneAsync(entity, cancellationToken: ct);
            return entity;
        }

        public async Task DeleteAsync(string id, CancellationToken ct = default)
        {
            await _collection.DeleteOneAsync(e => e.Id == id, ct);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id, CancellationToken ct = default)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync(ct);
        }

        public async Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity, cancellationToken: ct);
        }
    }
}
