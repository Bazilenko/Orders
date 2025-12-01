using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Exceptions
{
    public class CourierNotFoundException : DomainException
    {
        public CourierNotFoundException(string id) : base($"Courier with id {id} not found!", "NotFound")
        {
        }
    }
}
