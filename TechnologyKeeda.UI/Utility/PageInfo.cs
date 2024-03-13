using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnologyKeeda.UI.Utility
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }//3
        public int TotalItems { get; set; } //17

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);// 17/3 =  5.666 =6

        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;



    }
}
