using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Interfaces.Queries;
using Delivery.Domain.Entities;

namespace Delivery.Application.Queries.CourierQueries.Query
{
    public class FindCourierByPhoneNumberQuery : IQuery<Courier?>
    {
        public string PhoneNumber { get; init; } = default!;
    }
}
