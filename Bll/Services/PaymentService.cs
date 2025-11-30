using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal.Entities;
using Dal.UoW.Interfaces;
using Orders.Bll.DTOs.Payment;
using Orders.Bll.Services.Interfaces;
using Orders.Bll.Exception;

namespace Orders.Bll.Services
{
    public class PaymentService : IPaymentService

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaymentDto> CreateAsync(PaymentCreateDto dto, CancellationToken ct = default)
        {
            var entity  = _mapper.Map<Payment>(dto);
            var Id = await _unitOfWork._paymentRepository.AddAsync(entity);
            entity.Id = Id;
            _unitOfWork.Commit();
            return _mapper.Map<PaymentDto>(entity);
        }

        public async Task<PaymentDto> DeleteAsync(int id, CancellationToken ct = default)
        {
            var payment = await _unitOfWork._paymentRepository.GetAsync(id);
            if (payment == null)
                throw new NotFoundException($"Payment with Id {id} not found! ");
            await _unitOfWork._paymentRepository.DeleteAsync(id);
            _unitOfWork.Commit();
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<PaymentDto?> GetByOrderId(int orderId, CancellationToken ct = default)
        {
            
            var payment = await _unitOfWork._paymentRepository.GetByOrderIdAsync(orderId);
            if (payment == null)
                throw new NotFoundException($"Payment with order id {orderId} not found! ");
            return _mapper.Map<PaymentDto>(payment);
        } 

        public async Task<IEnumerable<PaymentDto>> GetAllAsync(CancellationToken ct = default)
        {
            var payments = await _unitOfWork._paymentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentDto>>(payments);
        }

        public async Task<PaymentDto> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var payment = await _unitOfWork._paymentRepository.GetAsync(id);
            return _mapper.Map<PaymentDto>(payment);
        }

        public async Task<PaymentDto> UpdateAsync(int id, PaymentUpdateDto paymentUpdateDto, CancellationToken ct = default)
        {
            var payment = await _unitOfWork._paymentRepository.GetAsync(id);
            if (payment == null)
                throw new NotFoundException($"Payment with Id {id} not found!");
            payment.Status = paymentUpdateDto.Status;
            await _unitOfWork._paymentRepository.ReplaceAsync(payment);
            return _mapper.Map<PaymentDto>(payment);
        }
    }
}
