using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Bll.Exception
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException(string message) : base(message) {
        }

    }
}
