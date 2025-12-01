using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Delivery.Application.Commands.CourierCommands.Commands;
using Delivery.Application.Interfaces.Commands;
using Delivery.Domain.Entities;
using Delivery.Domain.Interfaces.Repositories;
using Delivery.Domain.Interfaces.Services;

namespace Delivery.Application.Commands.CourierCommands.Handlers
{
    public class CreateCourierCommandHandler : ICommandHandler<CreateCourierCommand, string>
    {
        private readonly ICourierService _service;

        public CreateCourierCommandHandler(ICourierService service)
        {
            _service = service;
        }

        public async Task<string> Handle(CreateCourierCommand request, CancellationToken ct)
        {
            var courier = await _service.CreateCourierAsync(request.Name, request.Email, request.PhoneNumber, ct);

            return courier.Id;
        }
    }
}
