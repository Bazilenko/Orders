using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICommand = Delivery.Application.Interfaces.Commands.ICommand;

namespace Delivery.Application.Commands.DeliveryCommands.Command
{
    public record CalculateCostCommand(string DeliveryId, decimal BaseRatePerKm) : ICommand;
}
