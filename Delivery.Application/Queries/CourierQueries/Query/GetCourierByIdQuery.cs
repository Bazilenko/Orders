using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Interfaces.Queries;
using Delivery.Domain.Entities;

namespace Delivery.Application.Queries.CourierQueries.Query
{
    public class GetCourierByIdQuery : IQuery<Courier?>
    {
        public string courierId { get; init; } = default!;

    }
}
