using Delivery.Application.Commands.CourierCommands.Command;
using Delivery.Application.Commands.CourierCommands.Commands;
using Delivery.Application.Queries.CourierQueries.Query;
using Delivery.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Api.Controllers
{
    public class CourierController : BaseController
    {
        public CourierController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourierByIdQuery(string id, CancellationToken ct)
        {
            var res = await _mediator.Send(new GetCourierByIdQuery { courierId = id }, ct);

            if (res == null)
                throw new CourierNotFoundException(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult>AddCourier([FromBody]CreateCourierCommand cmd, CancellationToken ct)
        {
            var res = await _mediator.Send(cmd, ct);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourier([FromBody] UpdateCourierCommand cmd, CancellationToken ct)
        {
            var res = await _mediator.Send(cmd, ct);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourier([FromBody] DeleteCourierCommand cmd, CancellationToken ct)
        {
            await _mediator.Send(cmd, ct);
            return NoContent();
        }

        [HttpGet("by/{phoneNumber}")]
        public async Task<IActionResult> GetByPhoneNumber(string phoneNumber, CancellationToken ct)
        {
            var res = await _mediator.Send(new FindCourierByPhoneNumberQuery { PhoneNumber = phoneNumber}, ct);
            return Ok(res);
        }
    }
}
