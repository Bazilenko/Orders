using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public string? Code { get; }
        public DomainException(string message, string? code) : base(message)
        {
            Code = code;
        }
    }
}
