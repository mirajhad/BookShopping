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
    public class AuthorRepo : IAuthorRepo
    {
        private IHttpService _httpService;
        private string url = "api/authors";
        public AuthorRepo(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task DeleteAuthor(int id)
        {
            var response = await _httpService.Delete($"{url}/{id}");
            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
        }

        public async Task<Pagination<List<Author>>> GetAllAuthors(PageInfoDTO pageInfoDTO)
        {
        return  await _httpService.GetPagination<List<Author>>(url, pageInfoDTO);
           
        }

        public async Task<Author> GetAuthorById(int id)
        {
           var response = await  _httpService.Get<Author>($"{url}/{id}");
            if(!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
            return response.ServerResponse;
        }

        public async Task PostAuthor(Author author)
        {
            var response = await _httpService.Post(url, author);
            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
        }

        public async Task<List<Author>> SearchAuthor(string authorName)
        {
            var response = await _httpService.Get<List<Author>>($"{url}/search/{authorName}");
            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
            return response.ServerResponse;
        }

        public async Task UpdateAuthor(Author author)
        {
            var response = await _httpService.Put(url,author);
            if (!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
        }
    }
}
