using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal.UoW.Interfaces;
using Orders.Dal.DTOs.Customer;

namespace Orders.Bll.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IEnumerable<CustomerDto>> GetAllAsync(CancellationToken ct = default);
        Task<CustomerDto> GetByEmailAsync(string email, CancellationToken ct = default);
        Task<CustomerDto> CreateAsync(CustomerCreateDto customerCreateDto, CancellationToken ct = default);
        Task<CustomerDto> UpdateAsync(int id, CustomerUpdateDto customerUpdateDto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);

    }
}
