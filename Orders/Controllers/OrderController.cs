using Dal.DTOs.Order;
using Microsoft.AspNetCore.Mvc;
using Orders.Bll.DTOs.Order;
using Orders.Bll.Services;
using Orders.Bll.Services.Interfaces;

namespace Orders.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll() {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("Get-income-by-date")]
        public async Task<ActionResult<decimal>> GetIncome(DateTime dateFrom, DateTime dateTo)
        {
            var income = await _orderService.GetIncomeByDateAsync(dateFrom, dateTo);
            return Ok(income);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] OrderCreateDto dto)
        {
            var created = await _orderService.CreateAsync(dto);
            return Ok(created);
        }
        
    }
}
