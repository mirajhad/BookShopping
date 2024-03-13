using BlazorBookShop.Client.Helpers;
using BlazorBookShop.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private IHttpService _httpService;
        private string url = "api/categories";
        public CategoryRepo(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task CreateCategory(Category category)
        {
           var response =  await _httpService.Post(url, category);
            if(!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
        }

        public async Task DeleteCategory(int id)
        {
            var responseFromService = await _httpService.Delete($"{url}/{id}");
            if (!responseFromService.IsSuccess)
            {
                throw new ApplicationException(await responseFromService.GetBodyPart());
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var response = await _httpService.Get<List<Category>>(url);
            if(!response.IsSuccess) {
                throw new ApplicationException(await response.GetBodyPart());
}
            return response.ServerResponse;
        }

        public async Task<Category> GetCategory(int id)
        {
            var responseFromService = await _httpService.Get<Category>($"{url}/{id}");
            if(!responseFromService.IsSuccess)
            {
                throw new ApplicationException(await responseFromService.GetBodyPart());
            }
            return responseFromService.ServerResponse;


        }

        public async Task UpdateCategory(Category category)
        {
           var response = await _httpService.Put(url, category);
            if(!response.IsSuccess)
            {
                throw new ApplicationException(await response.GetBodyPart());
            }
        }
    }
}
