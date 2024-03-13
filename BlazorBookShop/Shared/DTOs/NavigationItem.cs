using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Shared.DTOs
{
    public class NavigationItem
    {
        public string Text { get; set; }
        public int PageNumber { get; set; }
        public bool Enabled { get; set; } = true;
        public bool IsActive { get; set; } = false;
        public NavigationItem(int pageNumber) : this(pageNumber, true)
        {

        }
        public NavigationItem(int pageNumber, bool enabled) : this(pageNumber.ToString(), pageNumber, enabled)
        {

        }
        public NavigationItem(string text, int page, bool enable)
        {
            Text = text;
            PageNumber = page;
            Enabled = enable;
        }

    }
}
