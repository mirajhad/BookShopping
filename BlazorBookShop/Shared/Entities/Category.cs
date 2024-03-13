using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Shared.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Field Should be Filled")]
        public  string Name { get; set; }
        public List<BookCategory> BookCategories { get; set; } = new List<BookCategory>();

    }
}
