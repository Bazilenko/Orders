using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Delivery.Domain.Common;
using Delivery.Domain.Exceptions;

namespace Delivery.Domain.Entities
{
    public class Courier : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public Courier(string name ,string email, string phoneNumber) {
            //if (string.IsNullOrWhiteSpace(email) ||
            //    !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            //    throw new DomainException("Invalid email", "InvalidEmail");

            //if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+?\d{10,15}$"))
            //    throw new DomainException("Invalid phone number", "InvalidNumber");
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;

        }
    }
}
