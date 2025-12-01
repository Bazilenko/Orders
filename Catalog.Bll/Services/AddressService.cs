using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Bll.DTOs.Address;
using Catalog.Bll.Services.Interfaces;
using Catalog.Dal.Entities;
using Catalog.Dal.UOW.Interfaces;

namespace Catalog.Bll.Services
{
    public class AddressService : IAddressService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AddressDto> Create(AddressCreateDto dto)
        {
            var entity = _mapper.Map<Address>(dto);
            await _unitOfWork.Addresses.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AddressDto>(entity);
        }

        public async Task<IEnumerable<AddressDto>> GetAll()
        {
            var categories = await _unitOfWork.Addresses.GetAllAsync();
            return _mapper.Map<IEnumerable<AddressDto>>(categories);
        }

        public async Task<AddressDto> GetById(int id)
        {
            var entity = await _unitOfWork.Addresses.GetByIdAsync(id);
            return _mapper.Map<AddressDto>(entity);
        }

        public async Task Update(AddressUpdateDto dto)
        {
            var entity = _mapper.Map<Address>(dto);
            await _unitOfWork.Addresses.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

