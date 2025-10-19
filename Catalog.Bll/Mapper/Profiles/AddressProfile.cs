using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Bll.DTOs.Address;
using Catalog.Dal.Entities;

namespace Catalog.Bll.Mapper.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile() {
            CreateMap<AddressCreateDto, Address>();
            CreateMap<AddressUpdateDto, Address>();
            CreateMap<AddressDto, Address>();
            CreateMap<Address, AddressDto>();
        }
    }
}
