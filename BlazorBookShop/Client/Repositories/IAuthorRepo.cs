using BlazorBookShop.Client.Helpers;
using BlazorBookShop.Shared.DTOs;
using BlazorBookShop.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Repositories
{
    public interface IAuthorRepo
    {
        Task PostAuthor(Author author);
        Task<Pagination<List<Author>>> GetAllAuthors(PageInfoDTO pageInfoDTO);
        Task<Author> GetAuthorById(int id);
        Task<List<Author>> SearchAuthor(string authorName);
        Task UpdateAuthor(Author author);
        Task DeleteAuthor(int id);







    }
}
