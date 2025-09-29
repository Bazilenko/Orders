using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Repository.Interfaces;

namespace Dal.UoW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository _customerRepository { get; }
        IOrderRepository _orderRepository { get; }
        IOrderDishRepository _orderDishRepository { get; }
        IPaymentRepository _paymentRepository { get; }
        void Commit();
        void Dispose();
    }
}
