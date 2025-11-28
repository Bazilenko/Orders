using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Interfaces.Commands;
using Delivery.Domain.ValueObjects;
using MediatR;

namespace Delivery.Application.Commands.DeliveryCommands.Command
{
    public record AssignDeliveryWindowCommand(
        string DeliveryId,
        DeliveryWindow Window) : ICommand<Domain.Entities.Delivery>;
}
