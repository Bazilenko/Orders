using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;
using Delivery.Domain.Enums;
using Delivery.Domain.Interfaces.Repositories;
using Delivery.Domain.Interfaces.Services;
using Delivery.Domain.Exceptions;

namespace Delivery.Application.Services
{
    public class CourierService : ICourierService
    {
        private readonly ICourierRepository _courierRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public CourierService(ICourierRepository courierRepository, IDeliveryRepository deliveryRepository)
        {
            _courierRepository = courierRepository;
            _deliveryRepository = deliveryRepository;
        }

        public async Task<Courier> CreateCourierAsync(string name, string email, string phoneNumber, CancellationToken ct = default)
        {
            var courier = new Courier(name, email, phoneNumber);

            await _courierRepository.AddAsync(courier, ct);

            return courier;
        }

        public async Task DeleteCourierAsync(string id, CancellationToken ct = default)
        {
            var courier = await _courierRepository.GetByIdAsync(id, ct);

            if (courier is null)
                throw new CourierNotFoundException(id);

            var activeDeliveries = await _deliveryRepository.GetByCourierAndStatusAsync(id, DeliveryStatus.Pending, ct);

            if (activeDeliveries.Any())
                throw new DomainException("Cannot delete courier with active deliveries!", "ActiveDeliveries");

            await _courierRepository.DeleteAsync(id, ct);
        }

        public async Task<Courier?> FindCourierByPhoneAsync(string phoneNumber, CancellationToken ct = default)
        {
            var courier = await _courierRepository.GetByPhoneNumberAsync(phoneNumber, ct);

            if (courier is null)
                throw new DomainException($"Courier with phone number {phoneNumber} not found!", "NotFound");

            return courier;
        }

        public async Task<int> GetActiveCourierDeliveryCountAsync(string courierId, CancellationToken ct = default)
        {
            var courier = await _courierRepository.GetByIdAsync(courierId, ct);

            if (courier is null)
                throw new CourierNotFoundException(courierId);

            var count = await _deliveryRepository.GetActiveDeliveryCountByCourierAsync(courierId, ct);

            return count;
        }

        public async Task<IEnumerable<Courier>> GetAllCouriersAsync(CancellationToken ct = default)
        {
            return await _courierRepository.GetAllAsync(ct);
        }

        public async Task<IEnumerable<Courier>> GetAvailableCouriersAsync(CancellationToken ct = default)
        {
            const int MAX_ACTIVE_DELIVERY_COUNT = 5;

            var couriers = await _courierRepository.GetAllAsync(ct);
            var availableCouriers = new List<Courier>();

            foreach(var courier in couriers)
            {
                var activeDelivryCount = await _deliveryRepository.GetActiveDeliveryCountByCourierAsync(courier.Id, ct);

                if (MAX_ACTIVE_DELIVERY_COUNT > activeDelivryCount)
                    availableCouriers.Add(courier);
            }

            return availableCouriers;
        }

        public async Task<Courier?> GetCourierByIdAsync(string id, CancellationToken ct = default)
        {
            var courier = await _courierRepository.GetByIdAsync(id, ct);

            if (courier is null)
                throw new CourierNotFoundException(id);

            return courier;
        }

        public async Task<IEnumerable<Domain.Entities.Delivery>> GetCourierDeliveriesAsync(string courierId, DeliveryStatus status, CancellationToken ct = default)
        {
            var courier = _courierRepository.GetByIdAsync(courierId, ct);

            if (courier is null)
                throw new CourierNotFoundException(courierId);

            if (status != null)
                await _deliveryRepository.GetByCourierAndStatusAsync(courierId, status, ct);
            return await _deliveryRepository.GetByCourierIdAsync(courierId, ct);
        }

        public async Task<Courier> UpdateCourierAsync(string id, string? name, string? email, string? phoneNumber, CancellationToken ct = default)
        {
            var courier = await _courierRepository.GetByIdAsync(id, ct);

            if (courier is null)
                throw new CourierNotFoundException(id);

            typeof(Courier).GetProperty(nameof(Courier.Name))?.SetValue(courier, name);
            typeof(Courier).GetProperty(nameof(Courier.Email))?.SetValue(courier, email);
            typeof(Courier).GetProperty(nameof(Courier.PhoneNumber))?.SetValue(courier, email);

            await _courierRepository.UpdateAsync(courier, ct);

            return courier;
        }
    }
}
