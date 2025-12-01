using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Exceptions
{
    public class InvalidMoneyException : DomainException
    {
        public InvalidMoneyException(decimal amount)
            : base($"Amount: {amount} cannot be negative", "InvalidMoneyException")
        {

        }
    }
}
