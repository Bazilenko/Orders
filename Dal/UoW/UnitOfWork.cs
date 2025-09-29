using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Repository;

namespace Dal.UoW
{
    public class UnitOfWork
    {
        CustomerRepository _customerRepository;
        readonly IDbTransaction _dbTransaction;
        
        public UnitOfWork(CustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }
    }
}
