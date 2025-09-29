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
        public ICustomerRepository _customerRepository { get; }
        public IOrderDishRepository _orderDishRepository { get; }
        public IOrderRepository _orderRepository { get; }
        public IPaymentRepository _paymentRepository { get; }


        void Commit();
        void Dispose();
    }
}
