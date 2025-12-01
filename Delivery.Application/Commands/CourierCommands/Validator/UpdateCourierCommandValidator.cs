using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Commands.CourierCommands.Commands;
using Delivery.Domain.Interfaces.Repositories;
using FluentValidation;

namespace Delivery.Application.Commands.CourierCommands.Validator
{
    public class UpdateCourierCommandValidator : BaseValidator<UpdateCourierCommand>
    {
        private readonly ICourierRepository _courierRepository;

        public UpdateCourierCommandValidator(ICourierRepository courierRepository)
        {
            _courierRepository = courierRepository;
            RuleFor(x => x.Id)
            .Must(BeValidId).WithMessage("Invalid courier ID");

            RuleFor(x => x.Name)
                .MinimumLength(2).WithMessage("Name must be at least 2 characters")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters")
                .When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Email)
                .Must(BeValidEmail).WithMessage("Invalid email format")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));

            RuleFor(x => x.PhoneNumber)
                .Must(BeValidPhoneNumber).WithMessage("Invalid phone number format")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));

        }
       
    }
}
