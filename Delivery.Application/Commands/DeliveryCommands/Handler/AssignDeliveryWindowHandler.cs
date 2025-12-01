using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Commands.DeliveryCommands.Command;
using Delivery.Application.Interfaces.Commands;
using Delivery.Domain.Interfaces.Services;

namespace Delivery.Application.Commands.DeliveryCommands.Handler
{
    public class AssignDeliveryWindowHandler : ICommandHandler<AssignDeliveryWindowCommand, Domain.Entities.Delivery>
    {
        private readonly IDeliveryService _service;

        public AssignDeliveryWindowHandler(IDeliveryService service)
        {
            _service = service;
        }

        public async Task<Domain.Entities.Delivery> Handle(AssignDeliveryWindowCommand req, CancellationToken ct = default)
        {
            var delivery = await _service.AssignDeliveryWindowAsync(req.DeliveryId, req.Window);
            return delivery;
        }

    }
}
