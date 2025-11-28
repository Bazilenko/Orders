using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Interfaces.Queries;
using Delivery.Application.Queries.CourierQueries.Query;
using Delivery.Domain.Entities;
using Delivery.Domain.Interfaces.Repositories;
using Delivery.Domain.Interfaces.Services;

namespace Delivery.Application.Queries.CourierQueries.Handler
{
    public class FindCourierByPhoneNumberHandler : IQueryHandler<FindCourierByPhoneNumberQuery, Courier?>
    {
        private readonly ICourierService _service;

        public FindCourierByPhoneNumberHandler(ICourierService service)
        {
            _service = service;
        }

        public async Task<Courier?> Handle(FindCourierByPhoneNumberQuery req, CancellationToken ct)
        {
            return await _service.FindCourierByPhoneAsync(req.PhoneNumber, ct);

        }
    }
}
