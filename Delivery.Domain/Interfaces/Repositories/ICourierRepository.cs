using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Domain.Entities;

namespace Delivery.Domain.Interfaces.Repositories
{
    public interface ICourierRepository : IRepository<Courier>
    {
        Task<Courier?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken ct = default);
    }
}
