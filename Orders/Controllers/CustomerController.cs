using Microsoft.AspNetCore.Mvc;
using Orders.Bll.Services.Interfaces;
using Orders.Dal.DTOs.Customer;
using Orders.Dal;

namespace Orders.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll(CancellationToken ct)
        {
            var customers = await _customerService.GetAllAsync(ct);
            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Create([FromBody] CustomerCreateDto dto, CancellationToken ct)
        {
            var created = await _customerService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetByEmail), new { email = created.Email }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerDto>> Update(int id, [FromBody] CustomerUpdateDto dto, CancellationToken ct)
        {
            var updated = await _customerService.UpdateAsync(id, dto, ct);
            return NoContent();
        }

        [HttpGet("by-email")]
        public async Task<ActionResult<CustomerDto>> GetByEmail([FromQuery] string email, CancellationToken ct)
        {
            var customers = await _customerService.GetByEmailAsync(email, ct);
            return Ok(customers);
        }
    }
}
