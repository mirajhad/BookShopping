using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Shared.DTOs
{
    public class DtoToSend
    {
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }

    }
}
