using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Exceptions
{
    public class DifferentCurrencyException : DomainException
    {
        public DifferentCurrencyException() : base("Cannot add money with different currency", "DifferentCurrency")
        {
        }
    }
}
