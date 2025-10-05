using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Bll.DTOs.Payment;
using Orders.Dal.DTOs.Customer;

namespace Orders.Bll.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentDto> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<PaymentDto>> GetAllAsync(CancellationToken ct = default);
        Task<PaymentDto> CreateAsync(PaymentCreateDto paymentCreateDto, CancellationToken ct = default);
        Task<PaymentDto> UpdateAsync(int id, PaymentUpdateDto paymentUpdateDto, CancellationToken ct = default);
        Task<PaymentDto> DeleteAsync(int id, CancellationToken ct = default);
    }
}
