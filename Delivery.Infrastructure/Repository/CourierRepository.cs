using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using Delivery.Domain.Interfaces.Repositories;
using Delivery.Infrastructure.Mongo;
using MongoDB.Driver;

namespace Delivery.Infrastructure.Repository
{
    public class CourierRepository : MongoRepository<Courier>, ICourierRepository
    {
        public CourierRepository(MongoDbContext context, IClientSessionHandle? session = null) : base(context, session)
        {
        }

        public async Task<Courier?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken ct = default)
        {
            var filter = Builders<Courier>.Filter.Eq(c => c.PhoneNumber, phoneNumber);
            return await _collection.Find(filter).FirstOrDefaultAsync(ct);
        }
    }
}
