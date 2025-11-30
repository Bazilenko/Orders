using Dal.DTOs.Order;
using Microsoft.AspNetCore.Mvc;
using Orders.Bll.DTOs.Order;
using Orders.Bll.DTOs.OrderDish;
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

        [HttpGet("Get-receipt")]
        public async Task<ActionResult<OrderReceiptDto>> GetOrderReceipt(int orderId)
        {
            var order = await _orderService.GetOrderWithDishesAsync(orderId);
            return Ok(order);

        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create([FromBody] OrderCreateDto dto)
        {
            var created = await _orderService.CreateAsync(dto);
            return Ok(created);
        }

        [HttpPut("addDish")]
        public async Task<ActionResult<OrderDishDto>> AddDishToOrder([FromBody] OrderDishCreateDto dto, CancellationToken ct)
        {
            var entity =  await _orderService.AddDishToOrder(dto);

            return Ok(entity);
        }


        
    }
}
