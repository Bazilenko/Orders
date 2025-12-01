using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using Delivery.Domain.Enums;
using Delivery.Domain.Interfaces.Repositories;
using Delivery.Domain.Interfaces.Services;
using Delivery.Domain.ValueObjects;
using Delivery.Domain.Exceptions;

namespace Delivery.Application.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly ICourierRepository _courierRepository;

        public DeliveryService(IDeliveryRepository deliveryRepository, ICourierRepository courierRepository)
        {
            _deliveryRepository = deliveryRepository;
            _courierRepository = courierRepository;
        }

        public async Task<Domain.Entities.Delivery> AssignCourierToDeliveryAsync(string deliveryId, string courierId, CancellationToken ct = default)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(deliveryId, ct);
            var courier = await _courierRepository.GetByIdAsync(courierId, ct);

            if (delivery is null)
                throw new DeliveryNotFoundException(deliveryId);
            if (courier is null)
                throw new CourierNotFoundException(courierId);

            delivery.AssignCourier(courier);
            await _deliveryRepository.UpdateAsync(delivery, ct);

            return delivery;
        }

        public async Task<Domain.Entities.Delivery> AssignDeliveryWindowAsync(string deliveryId, DeliveryWindow window, CancellationToken ct = default)
        {
            Domain.Entities.Delivery delivery = await _deliveryRepository.GetByIdAsync(deliveryId, ct);

            if (delivery is null)
                throw new DomainException($"Delivery with id {deliveryId} not found!", "NotFound");
            delivery.AssignWindow(window);
            await _deliveryRepository.UpdateAsync(delivery, ct);
            return delivery;
        }

        public async Task<Domain.Entities.Delivery> CalculateDeliveryCostAsync(string deliveryId, decimal baseRatePerKm, CancellationToken ct = default)
        {
            Domain.Entities.Delivery delivery = await _deliveryRepository.GetByIdAsync(deliveryId, ct);

            if (delivery is not null)
            {
                delivery.CalculateCost(baseRatePerKm);
                await _deliveryRepository.UpdateAsync(delivery, ct);
                return delivery;
            }
            throw new DeliveryNotFoundException(deliveryId);
        }

        public async Task<Domain.Entities.Delivery> CreateDeliveryAsync(int orderId, GeoCoordinate pickup, GeoCoordinate dropoff, CancellationToken ct = default)
        {
            var existingDelivery = await _deliveryRepository.GetByOrderIdAsync(orderId, ct);
            if (existingDelivery is null) {
                var delivery = new Domain.Entities.Delivery(orderId, pickup, dropoff);
               return await _deliveryRepository.AddAsync(delivery, ct);
            }
            throw new DomainException($"Delivery already exists for {orderId} ", "DuplicateOrder");
        }

        public async Task DeleteDeliveryAsync(string id, CancellationToken ct = default)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);

            if (delivery is null)
                throw new DeliveryNotFoundException(id);

            await _deliveryRepository.DeleteAsync(id);
        }

        public Task<Courier?> FindOptimalCourierForDeliveryAsync(string deliveryId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Domain.Entities.Delivery>> GetAllDeliveriesAsync(CancellationToken ct = default)
        {
            return await _deliveryRepository.GetAllAsync(ct);
        }

        public async Task<IEnumerable<Domain.Entities.Delivery>> GetDeliveriesByCourierAsync(string courierId, CancellationToken ct = default)
        {
            var delivery = await _deliveryRepository.GetByCourierIdAsync(courierId, ct);

            if (delivery is null)
                throw new DomainException($"Delivery with id {courierId} not found!", "NotFound");

            return delivery;
        }

        public async Task<IEnumerable<Domain.Entities.Delivery>> GetDeliveriesByStatusAsync(DeliveryStatus status, CancellationToken ct = default)
        {
            return await _deliveryRepository.GetByStatusAsync(status);
        }

        public async Task<IEnumerable<Domain.Entities.Delivery>> GetDeliveriesInTimeRangeAsync(DateTime start, DateTime end, CancellationToken ct = default)
        {
            return await _deliveryRepository.GetByTimeRangeAsync(start, end, ct);
        }

        public async Task<IEnumerable<Domain.Entities.Delivery>> GetDeliveriesNearLocationAsync(GeoCoordinate location, double radiusKm, CancellationToken ct = default)
        {
            var allDeliveries = await _deliveryRepository.GetAllAsync(ct);

            return allDeliveries.Where(d => d.PickUpLocation.DistanceTo(location) <= radiusKm ||
            d.DropOffLocation.DistanceTo(location) <= radiusKm);

        }

        public async Task<Domain.Entities.Delivery?> GetDeliveryByIdAsync(string id, CancellationToken ct = default)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id, ct);

            if (delivery is null)
                throw new DeliveryNotFoundException(id);

            return await _deliveryRepository.GetByIdAsync(id, ct);
        }

        public async Task<Domain.Entities.Delivery?> GetDeliveryByOrderIdAsync(int orderId, CancellationToken ct = default)
        {
            var delivery = await _deliveryRepository.GetByOrderIdAsync(orderId, ct);

            if (delivery is null)
                throw new DomainException($"Delivery with order id {orderId} not found!", "Not Found");

            return delivery;
        }

        public async Task<decimal> GetTotalDeliveryCostAsync(int orderId, CancellationToken ct = default)
        {
            var delivery = await _deliveryRepository.GetByOrderIdAsync(orderId, ct);

            if (delivery is null)
                throw new DomainException($"Delivery with order id {orderId} not found!", "Not Found");

            return delivery.DeliveryCost.Amount;
        }

        public async Task<Domain.Entities.Delivery> MarkDeliveryAsDeliveredAsync(string deliveryId, CancellationToken ct = default)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(deliveryId, ct);

            if (delivery is null)
                throw new DeliveryNotFoundException(deliveryId);
                
            delivery.MarkDelivered();

            await _deliveryRepository.UpdateAsync(delivery, ct);

            return delivery;
        }

        public async Task<Domain.Entities.Delivery> UpdateDeliveryAsync(string id, GeoCoordinate? pickup, GeoCoordinate? dropoff, CancellationToken ct = default)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id, ct);

            if (delivery is null)
                throw new DeliveryNotFoundException(id);

            typeof(Domain.Entities.Delivery).GetProperty(nameof(Domain.Entities.Delivery.PickUpLocation))?.SetValue(delivery, pickup);
            typeof(Domain.Entities.Delivery).GetProperty(nameof(Domain.Entities.Delivery.DropOffLocation))?.SetValue(delivery, dropoff);
            await _deliveryRepository.UpdateAsync(delivery, ct);

            return delivery;
        }

        public async Task<Domain.Entities.Delivery> UpdateDeliveryStatusAsync(string deliveryId, DeliveryStatus newStatus, CancellationToken ct = default)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(deliveryId, ct);

            if (delivery is null)
                throw new DeliveryNotFoundException(deliveryId);

            typeof(Domain.Entities.Delivery).GetProperty(nameof(DeliveryStatus))?.SetValue(delivery, newStatus);
            await _deliveryRepository.UpdateAsync(delivery, ct);

            return delivery;
        }
    }
}
