using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using Delivery.Domain.Enums;
using Delivery.Domain.ValueObjects;

namespace Delivery.Domain.Interfaces.Services
{

        public interface IDeliveryService
        {
            Task<Entities.Delivery> CreateDeliveryAsync(int orderId, GeoCoordinate pickup, GeoCoordinate dropoff, CancellationToken cancellationToken = default);
            Task<Entities.Delivery?> GetDeliveryByIdAsync(string id, CancellationToken cancellationToken = default);
            Task<Entities.Delivery?> GetDeliveryByOrderIdAsync(int orderId, CancellationToken cancellationToken = default);
            Task<IEnumerable<Entities.Delivery>> GetAllDeliveriesAsync(CancellationToken cancellationToken = default);
            Task<Entities.Delivery> UpdateDeliveryAsync(string id, GeoCoordinate? pickup, GeoCoordinate? dropoff, CancellationToken cancellationToken = default);
            Task DeleteDeliveryAsync(string id, CancellationToken cancellationToken = default);

            Task<Entities.Delivery> AssignCourierToDeliveryAsync(string deliveryId, string courierId, CancellationToken cancellationToken = default);
            Task<Entities.Delivery> AssignDeliveryWindowAsync(string deliveryId, DeliveryWindow window, CancellationToken cancellationToken = default);
         
            Task<Entities.Delivery> CalculateDeliveryCostAsync(string deliveryId, decimal baseRatePerKm, CancellationToken cancellationToken = default);
            Task<Entities.Delivery> MarkDeliveryAsDeliveredAsync(string deliveryId, CancellationToken cancellationToken = default);
            Task<Entities.Delivery> UpdateDeliveryStatusAsync(string deliveryId, DeliveryStatus newStatus, CancellationToken cancellationToken = default);

            
            Task<IEnumerable<Entities.Delivery>> GetDeliveriesByStatusAsync(DeliveryStatus status, CancellationToken cancellationToken = default);
            Task<IEnumerable<Entities.Delivery>> GetDeliveriesByCourierAsync(string courierId, CancellationToken cancellationToken = default);
            Task<IEnumerable<Entities.Delivery>> GetDeliveriesInTimeRangeAsync(DateTime start, DateTime end, CancellationToken cancellationToken = default);
            Task<decimal> GetTotalDeliveryCostAsync(int orderId, CancellationToken cancellationToken = default);

            // Business Logic - Optimization
            
            Task<IEnumerable<Entities.Delivery>> GetDeliveriesNearLocationAsync(GeoCoordinate location, double radiusKm, CancellationToken cancellationToken = default);
        }
}
