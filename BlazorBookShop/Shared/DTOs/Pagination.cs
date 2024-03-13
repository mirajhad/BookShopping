using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Shared.DTOs
{
    public class Pagination<T>
    {
        public T Response { get; set; }
        public int TotalPages { get; set; }

    }
}
