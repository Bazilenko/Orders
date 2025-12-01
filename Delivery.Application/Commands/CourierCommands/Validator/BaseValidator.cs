using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Delivery.Application.Commands.CourierCommands.Validator
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        protected bool BeValidId(string id)
        {
            return id != string.Empty;
        }

        protected bool BeValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        protected bool BeValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrWhiteSpace(phoneNumber) &&
                   System.Text.RegularExpressions.Regex.IsMatch(phoneNumber,    @"^\+?\d{10,15}$");
        }
    }
}
