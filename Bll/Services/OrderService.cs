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
using Orders.Bll.DTOs.OrderDish;
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

        public async Task<OrderReceiptDto> GetOrderWithDishesAsync(int orderId)
        {
            var order = await _untiOfWork._orderRepository.GetWithItemsByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException($"There is no Order with Id: {orderId}");
            return _mapper.Map<OrderReceiptDto>(order);
        }

        public async Task<OrderDishDto> AddDishToOrder(OrderDishCreateDto dto)
        {
            var order = await _untiOfWork._orderRepository.GetAsync(dto.OrderId);
            if (order == null)
                throw new NotFoundException($"Order with id {dto.OrderId} not found!");
            var entity = _mapper.Map<OrderDish>(dto);
            int id = await _untiOfWork._orderDishRepository.AddAsync(entity);
            entity.Id = id;
            var newTotalCost = await CalculateOrderCost(dto.OrderId);
            await UpdateOrderCost(order, newTotalCost);
            return _mapper.Map<OrderDishDto>(entity);
        }

        public async Task UpdateOrderCost(Order order, decimal cost)
        {
            
            order.TotalAmount = cost;
            await _untiOfWork._orderRepository.ReplaceAsync(order);
             _untiOfWork.Commit();
        }

        public async Task<OrderDto> GetById(int id)
        {
            var order = await _untiOfWork._orderRepository.GetAsync(id);
            if (order == null)
                throw new NotFoundException($"Order with id {id} not found!");
            return _mapper.Map<OrderDto>(order);
        }
        


        public async Task<decimal> CalculateOrderCost(int orderId)
        {
            var dishes = await _untiOfWork._orderDishRepository.GetByOrderIdAsync(orderId);
            decimal totalPrice = 0;
            if(dishes != null)
            {
                foreach (var dish in dishes)
                {
                    totalPrice += dish.Quantity * dish.PriceAtTimeOfOrder;
                }
            }
            return totalPrice;

        }
    }
}
