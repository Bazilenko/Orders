using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Commands.DeliveryCommands.Command;
using Delivery.Application.Interfaces.Commands;
using Delivery.Domain.Interfaces.Repositories;
using Delivery.Domain.Interfaces.Services;

namespace Delivery.Application.Commands.DeliveryCommands.Handler
{
    
        public class CreateDeliveryCommandHandler : ICommandHandler<CreateDeliveryCommand, string>
        {
            private readonly IDeliveryService _service;

            public CreateDeliveryCommandHandler(IDeliveryService service)
            {
                _service = service;
            }

            public async Task<string> Handle(CreateDeliveryCommand command, CancellationToken cancellationToken)
            {
                
                var delivery = await _service.CreateDeliveryAsync(command.OrderId, command.Pickup, command.Dropoff);

                return delivery.Id;
            }
        }
}
