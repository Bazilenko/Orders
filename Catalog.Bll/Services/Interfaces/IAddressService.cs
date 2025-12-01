using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.Bll.DTOs.Address;

namespace Catalog.Bll.Services.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressDto>> GetAll();
        Task<AddressDto> GetById(int id);
        Task Update(AddressUpdateDto dto);
        Task<AddressDto> Create(AddressCreateDto dto);
    }
}
