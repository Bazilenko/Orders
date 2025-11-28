using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Commands.CourierCommands.Validator;
using FluentValidation;

namespace Delivery.Application.Queries.CourierQueries.Query.Validator
{
    public class FindCourierByPhoneNumberQueryValidator : BaseValidator<FindCourierByPhoneNumberQuery>
    {
        public FindCourierByPhoneNumberQueryValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Must(BeValidPhoneNumber).WithMessage("Invalid phone number format");
        }
    }
}
