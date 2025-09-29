using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Dal.DTOs.Customer
{
    public class CustomerUpdateDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
