using BlazorBookShop.Shared.DTOs;
using BlazorBookShop.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Repositories
{
    public interface IBookRepository
    {
        Task DeleteBook(int id);
        Task<int> CreateBook(Book book);
        Task<List<Book>> GetAllBooks();
        Task<DetailsBookDTO> GetBookById(int id);


    }
}
