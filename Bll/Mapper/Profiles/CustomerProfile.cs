using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Orders.Dal.DTOs.Customer;
using Dal.Entities;

namespace Orders.Bll.Mapper.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile() {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();
        }
    }
}
