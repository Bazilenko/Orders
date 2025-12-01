using Microsoft.AspNetCore.Mvc;
using Orders.Bll.DTOs.Payment;
using Orders.Bll.Services.Interfaces;

namespace Orders.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
       private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService) 
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDto>>> GetAll() {
            var payments = await _paymentService.GetAllAsync();
            return Ok(payments);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDto>> Create([FromBody]PaymentCreateDto dto)
        {
            var created = await _paymentService.CreateAsync(dto);
            return Ok(created);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentDto>> Update(int id, [FromBody]PaymentUpdateDto dto)
        {
            var updated = await _paymentService.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDto>> Delete(int id)
        {
            var deleted = await _paymentService.DeleteAsync(id);
            return Ok(deleted);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<PaymentDto?>> ByOrderId(int orderId, CancellationToken ct)
        {
            var payment = await _paymentService.GetByOrderId(orderId);
            return Ok(payment);
        }


    }
}
