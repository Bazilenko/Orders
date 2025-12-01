using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Delivery.Application.Interfaces.Commands;
using MediatR;

namespace Delivery.Application.Commands.DeliveryCommands.Command
{
    public record AssignCourierToDeliveryCommand(
        string DeliveryId,
        string CourierId) : ICommand<Domain.Entities.Delivery>;
}
