using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Dal.Repository;
using Orders.Dal.Repository.Interfaces;
using Orders.Dal.UoW.Interfaces;

namespace Orders.Dal.UoW
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public ICustomerRepository _customerRepository { get; }
        public IOrderDishRepository _orderDishRepository { get; }
        public IOrderRepository _orderRepository { get; }
        public IPaymentRepository _paymentRepository { get; }

        readonly IDbTransaction _dbTransaction;

        public UnitOfWork(ICustomerRepository customerRepository, IOrderDishRepository orderDishRepository, IOrderRepository orderRepository, IPaymentRepository paymentRepository, IDbTransaction dbTransaction) {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _orderDishRepository = orderDishRepository;
            _dbTransaction = dbTransaction;
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
                // By adding this we can have muliple transactions as part of a single request
                //_dbTransaction.Connection.BeginTransaction();
            }
            catch (Exception ex)
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            //Close the SQL Connection and dispose the objects
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }

    }
}
