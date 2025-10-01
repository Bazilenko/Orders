using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal.DTOs.Order;
using Dal.Entities;
using Dal.UoW.Interfaces;
using Orders.Bll.DTOs.Order;
using Orders.Bll.Exception;
using Orders.Bll.Services.Interfaces;

namespace Orders.Bll.Services
{
    public class OrderService : IOrderService
    {
        protected IUnitOfWork _untiOfWork;
        protected IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _untiOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto?>> GetAllAsync()
        {
            var orders = await _untiOfWork._orderRepository.GetAllAsync();
            if (orders == null)
                throw new NotFoundException("List of orders is empty!");
            return _mapper.Map<IEnumerable<OrderDto?>>(orders);
        }

        public async Task<decimal?> GetIncomeByDateAsync(DateTime fromDate, DateTime toDate)
        {
            var result = await _untiOfWork._orderRepository.GetIncomeByDate(fromDate, toDate);
            if (result == 0)
                throw new NotFoundException($"There is not income for this dates {fromDate} - {toDate}");
            return result;
        }

        public async Task<OrderDto> CreateAsync(OrderCreateDto dto)
        {
            var entity = _mapper.Map<Order>(dto);
            int id = await _untiOfWork._orderRepository.AddAsync(entity);
            entity.Id = id;

            _untiOfWork.Commit();
            return _mapper.Map<OrderDto>(entity);
        }
    }
}
