using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Exceptions
{
    public class DeliveryNotFoundException : DomainException
    {
        public DeliveryNotFoundException(string id) : base($"Delivery with id {id} not found!", "NotFound")
        {
        }
    }
}
