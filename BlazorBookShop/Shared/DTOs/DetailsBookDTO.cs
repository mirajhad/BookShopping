using BlazorBookShop.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Shared.DTOs
{
    public class DetailsBookDTO
    {
        public Book Book { get; set; }
        public List<Category> Categories { get; set; }
        public List<Author> Authors { get; set; }

    }
}
