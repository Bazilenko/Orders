using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Interfaces.Commands;

namespace Delivery.Application.Commands.CourierCommands.Commands
{
    public record CreateCourierCommand(string Name, string Email, string PhoneNumber) : ICommand<string>;
    
}
