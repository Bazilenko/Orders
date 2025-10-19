using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.Bll.DTOs.Contact;
using Catalog.Dal.Entities;

namespace Catalog.Bll.Mapper.Profiles
{
    public class ContactProfile : Profile
    {
        public ContactProfile() {
            CreateMap<ContactCreateDto, Contact>();
            CreateMap<ContactUpdateDto, Contact>();
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactDto, Contact>();
        }
    }
}
