using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Common;
using Delivery.Domain.Enums;
using Delivery.Domain.Exceptions;
using Delivery.Domain.Value_Objects;
using Delivery.Domain.ValueObjects;

namespace Delivery.Domain.Entities
{
    public class Delivery : BaseEntity
    {
        public int OrderId { get; private set; }
        public Courier Courier { get; private set; }
        public GeoCoordinate PickUpLocation { get; private set; }
        public GeoCoordinate DropOffLocation { get; private set; }
        public DeliveryWindow TimeWindow { get; private set; }
        public Money? DeliveryCost { get; private set; }
        public DeliveryStatus Status { get; private set; }
        private Delivery() { }

        public Delivery(int orderId, GeoCoordinate pickup, GeoCoordinate dropoff)
        {
            if (int.IsNegative(orderId))
                throw new DomainException("OrderId cannot be negative.", "InvalidValue");

            OrderId = orderId;
            PickUpLocation = pickup;
            DropOffLocation = dropoff;
            Status = DeliveryStatus.Pending;
        }

        public void AssignWindow(DeliveryWindow window)
        {
            TimeWindow = window;
            Touch();
        }

        public void AssignCourier(Courier? courier)
        {
            if (this.Courier != null)
                throw new DomainException("Courier already assigned.", "CourierException");

            Courier = courier ?? throw new DomainException("Courier cannot be null.", "CourierException");
            Touch();
        }

        public void CalculateCost(decimal baseRatePerKm)
        {
            double distance = PickUpLocation.DistanceTo(DropOffLocation);
            DeliveryCost = new Money((decimal)distance * baseRatePerKm, "UAH");
            Touch();
        }
        public void MarkDelivered()
        {
            if (Status == DeliveryStatus.Delivered)
                throw new DomainException("Already delivered.", "DeliveryException");

            Status = DeliveryStatus.Delivered;
            Touch();
        }
    }
}
