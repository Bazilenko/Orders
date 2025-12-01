using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Interfaces.Commands;

namespace Delivery.Application.Commands.CourierCommands.Command
{
    public record DeleteCourierCommand(string Id) : ICommand
    {

    }
}
