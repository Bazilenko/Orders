using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Bll.DTOs.Contact;
using Catalog.Bll.Services.Interfaces;
using Catalog.Dal.Entities;
using Catalog.Dal.UOW.Interfaces;

namespace Catalog.Bll.Services
{
    public class ContactService : IContactService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ContactDto> Create(ContactCreateDto dto)
        {
            var entity = _mapper.Map<Contact>(dto);
            await _unitOfWork.Contacts.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ContactDto>(entity);
        }

        public async Task<IEnumerable<ContactDto>> GetAll()
        {
            var categories = await _unitOfWork.Contacts.GetAllAsync();
            return _mapper.Map<IEnumerable<ContactDto>>(categories);
        }

        public async Task<ContactDto> GetById(int id)
        {
            var entity = await _unitOfWork.Contacts.GetByIdAsync(id);
            return _mapper.Map<ContactDto>(entity);
        }

        public async Task Update(ContactUpdateDto dto)
        {
            var entity = _mapper.Map<Contact>(dto);
            await _unitOfWork.Contacts.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
