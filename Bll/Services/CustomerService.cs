using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal.Entities;
using Dal.UoW.Interfaces;
using Orders.Bll.Exception;
using Orders.Bll.Services.Interfaces;
using Orders.Dal.DTOs.Customer;

namespace Orders.Bll.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper ;
        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper) {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerDto> CreateAsync(CustomerCreateDto dto, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ValidationException("Username cannot be empty.");
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ValidationException("Email cannot be empty.");
            if (!dto.Email.Contains("@"))
                throw new ValidationException("Invalid email format.");

            var existingByEmail = await _unitOfWork._customerRepository!.GetByEmailAsync(dto.Email, ct);
            if (existingByEmail != null)
                throw new ValidationException($"Customer with email {dto.Email} already exists.");

            var entity = _mapper.Map<Customer>(dto);
            var id = await _unitOfWork._customerRepository.AddAsync(entity);
            entity.Id = id;

            _unitOfWork.Commit();

            return _mapper.Map<CustomerDto>(entity);
        }

        public async Task DeleteAsync(int id, CancellationToken ct = default)
        {
            var customer = await _unitOfWork._customerRepository!.GetAsync(id);
            if (customer == null)
                throw new NotFoundException($"Customer with id {id} not found.");

            await _unitOfWork._customerRepository.DeleteAsync(customer.Id);
            _unitOfWork.Commit();

        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync(CancellationToken ct = default)
        {
            var customers = await _unitOfWork._customerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            var customer = await _unitOfWork._customerRepository.GetByEmailAsync(email, ct);
            if (customer == null)
                throw new NotFoundException($"Customer with id {email} was not found");
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var customer = await _unitOfWork._customerRepository.GetAsync(id);
            if (customer == null)
            {
                throw new NotFoundException($"Customer with id {id} not found.");
            }
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> UpdateAsync(int id, CustomerUpdateDto dto, CancellationToken ct = default)
        {
            var customer = await _unitOfWork._customerRepository.GetAsync(id);
            if (customer == null)
                throw new NotFoundException($"Customer with id {id} not found");

            if(!string.IsNullOrEmpty(dto.Name) && dto.Name != customer.Name)
            {
                var existing = await _unitOfWork._customerRepository.GetByNameAsync(dto.Name, ct);
                if (existing != null && existing.Id != id)
                    throw new ValidationException($"Username {dto.Name} is already taken.");

                customer.Name = dto.Name;
            }

            if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != customer.Email)
            {
                var existing = await _unitOfWork._customerRepository.GetByEmailAsync(dto.Email, ct);
                if (existing != null && existing.Id != id)
                    throw new ValidationException($"Email {dto.Email} is already taken.");

                customer.Email = dto.Email;
            }
            await _unitOfWork._customerRepository.ReplaceAsync(customer);
             _unitOfWork.Commit();

            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
