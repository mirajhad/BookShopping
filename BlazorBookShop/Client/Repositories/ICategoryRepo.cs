using BlazorBookShop.Client.Pages.Categories;
using BlazorBookShop.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBookShop.Client.Repositories
{
    public interface ICategoryRepo
    {
        Task CreateCategory(Category category);
        Task<Category> GetCategory(int id);
        Task<List<Category>> GetAllCategories();
        Task UpdateCategory(Category category); 
        Task DeleteCategory(int id);
    }
}
