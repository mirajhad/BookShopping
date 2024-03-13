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
    public class BookRepository : IBookRepository
    {
        private IHttpService _httpService;
        private string url = "api/books";
        public BookRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<int> CreateBook(Book book)
        {
          var response =  await   _httpService.PostData<Book,int>(url, book);
            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
            return response.ServerResponse;
        }

        public async Task DeleteBook(int id)
        {
         var response = await _httpService.Delete($"{url}/{id}");
            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
        }

        public async Task<List<Book>> GetAllBooks()
        {
            var response = await _httpService.Get<List<Book>>(url);
            if(!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
            return response.ServerResponse;
        }

        public async Task<DetailsBookDTO> GetBookById(int id)
        {
            var response = await _httpService.Get<DetailsBookDTO>($"{url}/{id}");
            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
            return response.ServerResponse;
        }
    }
}
