using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.DTOs.Order;
using Orders.Bll.DTOs.Order;

namespace Orders.Bll.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto?>> GetAllAsync();
        Task<decimal?> GetIncomeByDateAsync(DateTime fromDate, DateTime toDate);
        Task<OrderDto> CreateAsync(OrderCreateDto dto);

    }
}
