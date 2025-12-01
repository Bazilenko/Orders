using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Commands.CourierCommands.Command;
using Delivery.Application.Interfaces.Commands;
using Delivery.Domain.Interfaces.Services;
using MediatR;

namespace Delivery.Application.Commands.CourierCommands.Handler
{
    public class DeleteCourierCommandHandler : ICommandHandler<DeleteCourierCommand>
    {
        private readonly ICourierService _courierService;

        public DeleteCourierCommandHandler(ICourierService courierService)
        {
            _courierService = courierService;
        }

        public async Task<Unit> Handle(DeleteCourierCommand cmd, CancellationToken ct)
        {
            var courier = await _courierService.GetCourierByIdAsync(cmd.Id);

            await _courierService.DeleteCourierAsync(courier.Id,ct);
            return Unit.Value;
        }
    }
}
