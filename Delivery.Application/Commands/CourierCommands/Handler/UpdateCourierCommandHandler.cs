using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Commands.CourierCommands.Commands;
using Delivery.Application.Interfaces.Commands;
using Delivery.Domain.Entities;
using Delivery.Domain.Exceptions;
using Delivery.Domain.Interfaces.Repositories;
using Delivery.Domain.Interfaces.Services;
using MediatR;

namespace Delivery.Application.Commands.CourierCommands.Handlers
{
    public class UpdateCourierCommandHandler : ICommandHandler<UpdateCourierCommand>
    {
        private readonly ICourierService _service;

        public UpdateCourierCommandHandler(ICourierService service)
        {
            _service = service;
        }

        public async Task<Unit> Handle(UpdateCourierCommand request, CancellationToken ct)
        {
            await _service.UpdateCourierAsync(request.Id, request.Name, request.Email, request.PhoneNumber);

            return Unit.Value;
            
        }
    }
}
