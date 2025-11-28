using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Interfaces.Queries;
using Delivery.Application.Queries.CourierQueries.Query;
using Delivery.Domain.Entities;
using Delivery.Domain.Interfaces.Services;

namespace Delivery.Application.Queries.CourierQueries.Handler
{
    public class GetCourierByIdQueryHandler : IQueryHandler<GetCourierByIdQuery, Courier?>
    {
        private readonly ICourierService _service;

        public GetCourierByIdQueryHandler(ICourierService service)
        {
            _service = service;
        }

        public async Task<Courier?> Handle(GetCourierByIdQuery req, CancellationToken ct)
        {
            return await _service.GetCourierByIdAsync(req.courierId);
        }
    }
}
