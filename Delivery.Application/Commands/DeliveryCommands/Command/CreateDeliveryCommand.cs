using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Application.Interfaces.Commands;
using Delivery.Domain.ValueObjects;

namespace Delivery.Application.Commands.DeliveryCommands.Command
{
    public record CreateDeliveryCommand( int OrderId, GeoCoordinate Pickup, GeoCoordinate Dropoff
) : ICommand<string>;
    }

