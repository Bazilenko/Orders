using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Entities;

namespace Dal.Repository.Interfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<Payment> GetByOrderIdAsync(int orderId);
        Task<Payment> GetByPaymentMethodAsync(string paymentMethod);
    }
}
