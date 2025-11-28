using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using Delivery.Domain.Enums;
using Delivery.Domain.ValueObjects;

namespace Delivery.Domain.Interfaces.Repositories
{
    public interface IDeliveryRepository : IRepository<Entities.Delivery>
    {
        Task<Entities.Delivery?> GetByOrderIdAsync(int orderId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Entities.Delivery>> GetByCourierIdAsync(string courierId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Entities.Delivery>> GetByStatusAsync(DeliveryStatus status, CancellationToken cancellationToken = default);
        Task<IEnumerable<Entities.Delivery>> GetByCourierAndStatusAsync(string courierId, DeliveryStatus status, CancellationToken cancellationToken = default);
        Task<IEnumerable<Entities.Delivery>> GetByTimeRangeAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default);
        Task<int> GetActiveDeliveryCountByCourierAsync(string courierId, CancellationToken cancellationToken = default);
    }
}
