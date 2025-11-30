using Delivery.Application.Commands.DeliveryCommands.Command;
using Delivery.Application.Queries.CourierQueries.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers
{
    public class DeliveryController : BaseController
    {
        public DeliveryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateDelivery([FromBody] CreateDeliveryCommand cmd, CancellationToken ct)
        {
            var res = await _mediator.Send(cmd, ct);
            return Ok(res);
        }
        [HttpPut]
        public async Task<IActionResult> AssignCourierToDelivery([FromBody] AssignCourierToDeliveryCommand cmd, CancellationToken ct)
        {
            await _mediator.Send(cmd, cancellationToken: ct);
            return NoContent();

        }

        [HttpPut("assign-window")]
        public async Task<IActionResult> AssignWindowToDelivery([FromBody] AssignDeliveryWindowCommand cmd, CancellationToken ct)
        {
            var res = await _mediator.Send(cmd, cancellationToken: ct);
            return Ok(res);
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllDeliveries(CancellationToken ct)
        {
            GetAllDeliveriesQuery cmd = new GetAllDeliveriesQuery();
            var res = await _mediator.Send(cmd, ct);
            return Ok(res);
        }

    }
}
