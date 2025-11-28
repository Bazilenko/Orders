using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Enums;
using Delivery.Domain.Interfaces.Repositories;
using Delivery.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Delivery.Infrastructure.Repository
{
    public class DeliveryRepository : MongoRepository<Domain.Entities.Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(MongoDbContext context, IClientSessionHandle? session = null) : base(context, session)
        {
        }

        public async Task<int> GetActiveDeliveryCountByCourierAsync(string courierId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.Delivery>.Filter.And(
            Builders<Domain.Entities.Delivery>.Filter.Eq(d => d.Id, courierId),
            Builders<Domain.Entities.Delivery>.Filter.Nin(d => d.Status, new[] { DeliveryStatus.Delivered, DeliveryStatus.Failed })
        );
            var count = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
            return (int)count;
        }

        public async Task<IEnumerable<Domain.Entities.Delivery>> GetByCourierAndStatusAsync(string courierId, DeliveryStatus status, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.Delivery>.Filter.And(
            Builders<Domain.Entities.Delivery>.Filter.Eq(d => d.Courier.Id, courierId),
            Builders<Domain.Entities.Delivery>.Filter.Eq(d => d.Status, status)
        );

            return await _collection.Find(filter).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Domain.Entities.Delivery>> GetByCourierIdAsync(string courierId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.Delivery>.Filter.Eq(d => d.Courier.Id, courierId);
            return await _collection.Find(filter).ToListAsync(cancellationToken);
        }

        public async Task<Domain.Entities.Delivery?> GetByOrderIdAsync(int orderId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.Delivery>.Filter.Eq(d => d.OrderId, orderId);
            return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Domain.Entities.Delivery>> GetByStatusAsync(DeliveryStatus status, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.Delivery>.Filter.Eq(d => d.Status, status);
            return await _collection.Find(filter).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Domain.Entities.Delivery>> GetByTimeRangeAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default)
        {
            var filter = Builders<Domain.Entities.Delivery>.Filter.And(
            Builders<Domain.Entities.Delivery>.Filter.Gte(d => d.CreatedAt, start),
            Builders<Domain.Entities.Delivery>.Filter.Lt(d => d.CreatedAt, end)
        );

            return await _collection.Find(filter).ToListAsync(cancellationToken);
        }
    }
}
