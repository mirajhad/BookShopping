using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Shared.Entities
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string AuthorBio { get; set; }
        public string? AuthorImage { get; set; }
        public List<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();


    }
}
