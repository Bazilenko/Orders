using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using Delivery.Domain.Enums;

namespace Delivery.Domain.Interfaces.Services
{
    public interface ICourierService
    {
        Task<Courier> CreateCourierAsync(string name, string email, string phoneNumber, CancellationToken cancellationToken = default);
        Task<Courier?> GetCourierByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Courier>> GetAllCouriersAsync(CancellationToken cancellationToken = default);
        Task<Courier> UpdateCourierAsync(string id, string? name, string? email, string? phoneNumber, CancellationToken cancellationToken = default);
        Task DeleteCourierAsync(string id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Courier>> GetAvailableCouriersAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Entities.Delivery>> GetCourierDeliveriesAsync(string courierId, DeliveryStatus status, CancellationToken cancellationToken = default);
        Task<int> GetActiveCourierDeliveryCountAsync(string courierId, CancellationToken cancellationToken = default);
        Task<Courier?> FindCourierByPhoneAsync(string phoneNumber, CancellationToken cancellationToken = default);
    }
}
