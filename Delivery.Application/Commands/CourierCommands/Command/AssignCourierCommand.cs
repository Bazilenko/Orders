using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ICommand = Delivery.Application.Interfaces.Commands.ICommand;

namespace Delivery.Application.Commands.CourierCommands.Commands
{
        public record AssignCourierCommand(string DeliveryId, string CourierId) : ICommand;
    
}
