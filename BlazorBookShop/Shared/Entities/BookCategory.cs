using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Shared.Entities
{
    public class BookCategory
    {
        public int BookId { get; set; }
        
        public Book? Book { get; set; }
        public int CategoryId { get; set; }
       
        public Category? Category { get; set; }

    }
}
