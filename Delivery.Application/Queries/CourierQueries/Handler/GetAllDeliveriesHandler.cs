using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Interfaces.Queries;
using Delivery.Application.Queries.CourierQueries.Query;
using Delivery.Domain.Interfaces.Services;

namespace Delivery.Application.Queries.CourierQueries.Handler
{
    public class GetAllDeliveriesHandler : IQueryHandler<GetAllDeliveriesQuery, List<Domain.Entities.Delivery>>
    {
        public IDeliveryService _service;

        public GetAllDeliveriesHandler(IDeliveryService service)
        {
            _service = service;
        }

        public async Task<List<Domain.Entities.Delivery>> Handle(GetAllDeliveriesQuery request, CancellationToken cancellationToken)
        {
            var deliveries = await _service.GetAllDeliveriesAsync(cancellationToken);
            return deliveries.ToList();
        }
    }
}
