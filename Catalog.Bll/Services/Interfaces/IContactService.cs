using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Bll.DTOs.Contact;

namespace Catalog.Bll.Services.Interfaces
{
    public interface IContactService 
    {
        Task<IEnumerable<ContactDto>> GetAll();
        Task<ContactDto> GetById(int id);
        Task Update(ContactUpdateDto dto);
        Task<ContactDto> Create(ContactCreateDto dto);
    }
}
