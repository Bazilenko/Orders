using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Commands.DeliveryCommands.Command;
using Delivery.Application.Interfaces.Commands;
using Delivery.Domain.Interfaces.Services;
using MediatR;

namespace Delivery.Application.Commands.DeliveryCommands.Handler
{
    public class AssignCourierToDeliveryCommandHandler : ICommandHandler<AssignCourierToDeliveryCommand, Domain.Entities.Delivery>
    {
        private readonly IDeliveryService _service;
        public AssignCourierToDeliveryCommandHandler(IDeliveryService service) { _service = service; }

        public async Task<Domain.Entities.Delivery> Handle(AssignCourierToDeliveryCommand request, CancellationToken ct)
        {
            return await _service.AssignCourierToDeliveryAsync(request.DeliveryId, request.CourierId, ct);
        }
    }
}
