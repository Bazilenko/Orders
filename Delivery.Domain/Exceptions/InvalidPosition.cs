using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Exceptions
{
    public class InvalidPosition : DomainException
    {
        public InvalidPosition() : 
            base("Latitude must be between -90 and 90 while Longitude must be between -180 and 180", "InvalidCoordinates")
        {
        }
    }
}
