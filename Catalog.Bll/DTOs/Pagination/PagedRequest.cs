using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Bll.DTOs.Pagination
{
    public class PagedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortColumn { get; set; } = "Id"; // Назва поля, як у класі Entity
        public string SortOrder { get; set; } = "asc"; // "asc" або "desc"
    }
}
