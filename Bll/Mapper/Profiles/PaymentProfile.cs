using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dal.Entities;
using Orders.Bll.DTOs.Payment;

namespace Orders.Bll.Mapper.Profiles
{
    public class PaymentProfile: Profile
    {
        public PaymentProfile() {
            CreateMap<Payment, PaymentDto>();
            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<PaymentUpdateDto, Payment>();
        }
    }
}
