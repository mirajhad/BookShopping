using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Shared.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BookDescription { get; set; }
        public string BookISBN { get; set; }
        public string BookImage { get; set; }
        public List<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        public List<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

    }
}
